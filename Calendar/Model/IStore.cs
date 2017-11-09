﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public interface IStore
    {
        List<Day> GetDaysWithNow();
        List<Day> GetDays(DateTime dateTime);
        void AddAppointment(Appointment appointment);
        void DeleteAppointment(Appointment appointment);
    }
}
