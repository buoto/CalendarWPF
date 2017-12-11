﻿using System;
using System.Collections.Generic;

namespace Calendar.Model
{
    public interface IStorage
    {
        List<Person> GetPersons();
        Person GetPerson(Guid ID);
        void CreatePerson(string firstName, string lastName);


        List<Appointment> GetAppointments(Person person);
        Appointment CreateAppointment(string title, DateTime startTime, DateTime endTime);
        void UpdateAppointment(Appointment st);
        void DeleteAppointment(Appointment st);

        Attendance CreateAttendance(Appointment appointment, Person person);
    }
}
