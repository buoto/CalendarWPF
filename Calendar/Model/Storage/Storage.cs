using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Calendar.Model
{
    class Storage : IStorage
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(Storage));

        public List<Appointment> GetAppointments(Person person)
        {
            using (var db = new StorageContext())
            {
                if (person == null)
                    return db.Appointments.ToList();
                var q = from a in db.Appointments
                       join att in db.Attendances on a.AppointmentId equals att.Appointment.AppointmentId
                       where att.Person.PersonId == person.PersonId
                       select a;
                return q.ToList();
            }
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

        public Person GetPersonByUserID(string userID)
        {
            using (var db = new StorageContext()) {
                try
                {
                    return db.Persons.Where(p => p.UserID == userID).First();
                }
                catch (InvalidOperationException)
                {
                    return new Person();
                }
            }

        }

        public void CreatePerson(string firstName, string lastName, string userId)
        {
            using (var db = new StorageContext()) {
                var person = new Person
                {
                    PersonId = Guid.NewGuid(),
                    FirstName = firstName,
                    LastName = lastName,
                    UserID = userId,
                };
                db.Persons.Add(person);
                db.SaveChanges();
            }
        }

        public Appointment CreateAppointment(string title, DateTime startTime, DateTime endTime)
        {
            using (var db = new StorageContext())
            {

                var appointment = new Appointment
                {
                    AppointmentId = Guid.NewGuid(),
                    Title = title,
                    StartTime = startTime,
                    EndTime = endTime
                };
                db.Appointments.Add(appointment);

                db.SaveChanges();
                return appointment;
            }
        }


        public void UpdateAppointment(Appointment st)
        {
            using (var db = new StorageContext())
            {
                var original = db.Appointments.Find(st.AppointmentId);
                if (original != null)
                {
                    db.Entry(original).OriginalValues["RowVersion"] = st.RowVersion;
                    original.Title = st.Title;
                    original.StartTime = st.StartTime;
                    original.EndTime = st.EndTime;
                    try
                    {
                        db.SaveChanges();
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        string message = string.Format("Update of appointment \"{0}\" failed due to concurrent write!", st.Title);
                        log.Error(message, e);
                        throw new ConcurrentUpdateException(message);
                    }
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
                    original.Attendances.ToList().ForEach(attendance => db.Attendances.Remove(attendance));

                    db.Appointments.Remove(original);
                    db.SaveChanges();
                }
            }
        }

        public Attendance CreateAttendance(Appointment appointment, Person person)
        {
            using (var db = new StorageContext())
            {
                var attendance = new Attendance
                {
                    AttendanceId = Guid.NewGuid(),
                    Accepted = true,
                    PersonId = person.PersonId,
                    AppointmentId = appointment.AppointmentId,
                };
                db.Attendances.Add(attendance);

                db.SaveChanges();
                return attendance;
            }
        }
    }
}
