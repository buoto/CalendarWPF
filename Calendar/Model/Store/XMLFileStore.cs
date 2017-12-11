using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Calendar.Model
{
    class XMLFileStore : IStore
    {
        private static readonly string FILENAME = @"../../CalendarData.xml";
        private Dictionary<DateTime, List<Appointment>> appointmentsDict = new Dictionary<DateTime, List<Appointment>>();

        public XMLFileStore()
        {
            LoadState();
        }

        private void LoadState()
        {
            List<Appointment> list;
            XmlSerializer xs = new XmlSerializer(typeof(List<Appointment>));
            using (var sr = new StreamReader(FILENAME))
            {
                list = (List<Appointment>)xs.Deserialize(sr);
            }

            foreach (var a in list.OrderBy(a => a.StartTime))
            {
                AddAppointment(a);
            }
        }

        public void AddAppointment(Appointment appointment)
        {
            var date = appointment.StartTime.Date;
            List<Appointment> appointments;
            appointmentsDict.TryGetValue(date, out appointments);
            if (appointments == null) {
                appointments = new List<Appointment>();
            }
            appointments.Add(appointment);
            appointmentsDict[date] = appointments;

            SaveState();
        }

        public void DeleteAppointment(Appointment appointment)
        {
            var date = appointment.StartTime.Date;
            List<Appointment> appointments;
            appointmentsDict.TryGetValue(date, out appointments);
            if (appointments == null) {
                appointments = new List<Appointment>();
            }
            appointments.Remove(appointment);
            appointmentsDict[date] = appointments;
            SaveState();
        }

        private void SaveState()
        {
            var list = new List<Appointment>();
            foreach (var aps in appointmentsDict.ToArray()) {
                list.AddRange(aps.Value);
            }
            XmlSerializer xs = new XmlSerializer(typeof(List<Appointment>));
            using (TextWriter tw = new StreamWriter(FILENAME)) {
                xs.Serialize(tw, list);
            } 
        }

        public List<Day> GetDays(DateTime dateTime)
        {
            List<Day> list = new List<Day>();
            DateTime monday = dateTime.Date.AddDays(DayOfWeek.Monday - dateTime.DayOfWeek);

            for (int i = 0; i < 28; i++) {
                var date = monday.AddDays(i);
                Day day = new Day(date);
                List<Appointment> appointments;
                foreach (Appointment appointment in appointmentsDict.TryGetValue(date.Date, out appointments) ? appointments : new List<Appointment>())
                {
                    day.Appointments.Add(appointment);
                }
                list.Add(day);
            }

            return list;
        }

        public List<Day> GetDaysWithNow()
        {
            return GetDays(DateTime.Now);
        }

        public void EditAppointment(Appointment old, Appointment changed)
        {
            DeleteAppointment(old);
            AddAppointment(changed);
        }
    }
}
