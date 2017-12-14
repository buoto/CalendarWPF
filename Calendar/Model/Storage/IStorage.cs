using System;
using System.Collections.Generic;

namespace Calendar.Model
{
    public interface IStorage
    {
        List<Person> GetPersons();
        Person GetPerson(Guid ID);
        Person GetPersonByUserID(String userID);
        void CreatePerson(string firstName, string lastName, string userId);


        List<Appointment> GetAppointments(Person person);
        Appointment CreateAppointment(string title, DateTime startTime, DateTime endTime);
        void UpdateAppointment(Appointment st);
        void DeleteAppointment(Appointment st);

        Attendance CreateAttendance(Appointment appointment, Person person);
    }
}
