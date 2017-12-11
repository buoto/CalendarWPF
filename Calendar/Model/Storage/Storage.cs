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
            {
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

        public void CreatePerson(string firstName, string lastName)
        {
            using (var db = new StorageContext()) {
                var person = new Person
                {
                    PersonId = Guid.NewGuid(),
                    FirstName = firstName,
                    LastName = lastName
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
                    original.Title = st.Title;
                    original.StartTime = st.StartTime;
                    original.EndTime = st.EndTime;
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
