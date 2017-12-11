using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    class Storage : IStorage
    {
        public List<Appointment> getAppointments()
        {
            using (var db = new StorageContext())
                return db.Appointments.ToList();
        }

        public List<Person> getPersons()
        {
            using (var db = new StorageContext())
                return db.Persons.ToList();
        }


        public void CreateAppointment(string title, DateTime startTime, DateTime endTime)
        {
            using (var db = new StorageContext())
            {
                var Appointment = new Appointment
                {
                    Title = title,
                    StartTime = startTime,
                    EndTime = endTime
                };
                db.Appointments.Add(Appointment);
                db.SaveChanges();
            }
        }

        public void UpdateAppointment(Appointment st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(st.AppointmentId);
                if (original != null)
                {
                    original.Title = st.Title;
                    original.StartTime = st.StartTime;
                    original.EndTime = st.EndTime;
                    original.Attendances = st.Attendances;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteAppointment(Appointment st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(st.AppointmentId);
                if (original != null)
                {
                    db.Appointments.Remove(original);
                    db.SaveChanges();
                }
            }
        }


    }
}
