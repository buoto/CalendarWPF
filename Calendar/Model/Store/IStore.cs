﻿using System;
using System.Collections.Generic;

namespace Calendar.Model
{
    public interface IStore
    {
        List<Day> GetDaysWithNow();
        List<Day> GetDays(DateTime dateTime);
        void AddAppointment(Appointment appointment);
        void EditAppointment(Appointment old, Appointment changed);
        void DeleteAppointment(Appointment appointment);
    }
}
