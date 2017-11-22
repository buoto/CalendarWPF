using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Calendar.Model
{
    class XMLFileStore : IStore
    {
        private static readonly string FILENAME = @"../../CalendarData.xml";
        private Dictionary<DateTime, List<Event>> appointmentsDict = new Dictionary<DateTime, List<Event>>();

        public XMLFileStore()
        {
            LoadState();
        }

        private void LoadState()
        {
            List<Event> list;
            XmlSerializer xs = new XmlSerializer(typeof(List<Event>));
            using (var sr = new StreamReader(FILENAME))
            {
                list = (List<Event>)xs.Deserialize(sr);
            }

            foreach (var a in list.OrderBy(a => a.Start))
            {
                AddAppointment(a);
            }
        }

        public void AddAppointment(Event appointment)
        {
            var date = appointment.Start.Date;
            List<Event> appointments;
            appointmentsDict.TryGetValue(date, out appointments);
            if (appointments == null) {
                appointments = new List<Event>();
            }
            appointments.Add(appointment);
            appointmentsDict[date] = appointments;

            SaveState();
        }

        public void DeleteAppointment(Event appointment)
        {
            var date = appointment.Start.Date;
            List<Event> appointments;
            appointmentsDict.TryGetValue(date, out appointments);
            if (appointments == null) {
                appointments = new List<Event>();
            }
            appointments.Remove(appointment);
            appointmentsDict[date] = appointments;
            SaveState();
        }

        private void SaveState()
        {
            var list = new List<Event>();
            foreach (var aps in appointmentsDict.ToArray()) {
                list.AddRange(aps.Value);
            }
            XmlSerializer xs = new XmlSerializer(typeof(List<Event>));
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
                List<Event> appointments;
                foreach (Event appointment in appointmentsDict.TryGetValue(date.Date, out appointments) ? appointments : new List<Event>())
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
    }
}
