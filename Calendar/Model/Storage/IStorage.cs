using System;
using System.Collections.Generic;

namespace Calendar.Model
{
    public interface IStorage
    {
        List<Person> GetPersons();
        List<Appointment> GetAppointments(Person person);
        Person GetPerson(Guid ID);

        void CreateAppointment(string title, DateTime startTime, DateTime endTime, Person person);
        void UpdateAppointment(Appointment st);
        void DeleteAppointment(Appointment st);
    }
}
