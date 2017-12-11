using System;
using System.Collections.Generic;
using System.Linq;

namespace Calendar.Model
{
    class Storage : IStorage
    {
        public List<Appointment> GetAppointments(Person person)
        {
            using (var db = new StorageContext())
                return db.Appointments.ToList();
        }

        public List<Person> GetPersons()
        {
            using (var db = new StorageContext())
                return db.Persons.ToList();
        }

        public Person GetPerson(Guid ID)
        {
            using (var db = new StorageContext())
                return db.Persons.Find(ID);
        }


        public void CreateAppointment(string title, DateTime startTime, DateTime endTime, Person person)
        {
            using (var db = new StorageContext())
            {
                var appointment = new Appointment
                {
                    Title = title,
                    StartTime = startTime,
                    EndTime = endTime
                };
                db.Appointments.Add(appointment);
                db.Attendances.Add(new Attendance { Appointment = appointment, Person = person, Accepted = true });
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
                    original.Attendances.ForEach(attendance => db.Attendances.Remove(attendance));

                    db.Appointments.Remove(original);
                    db.SaveChanges();
                }
            }
        }


    }
}
