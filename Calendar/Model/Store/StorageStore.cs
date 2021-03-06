﻿using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Calendar.Model.Store
{
    public class StorageStore : IStore
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(StorageStore));
        private IStorage storage;
        private Person person;

        public StorageStore(IStorage storage) {
            this.storage = storage;

            var args = Environment.GetCommandLineArgs();

            if (args.Length > 1) {
                var userID = args[1];
                person = storage.GetPersonByUserID(userID);
                if (person != null)
                {
                    log.Info(string.Format("Signed in as {0} {1}.", person.FirstName, person.LastName));
                }
            }
        }

        public void AddAppointment(Appointment appointment)
        {
            appointment = storage.CreateAppointment(appointment.Title, appointment.StartTime, appointment.EndTime);
            if (person != null && person.PersonId != Guid.Empty) {
                storage.CreateAttendance(appointment, person);
            }
        }

        public void EditAppointment(Appointment old, Appointment changed)
        {
            storage.UpdateAppointment(changed);

        }

        public void DeleteAppointment(Appointment appointment)
        {
            storage.DeleteAppointment(appointment);
        }

        public List<Day> GetDays(DateTime dateTime)
        {
            List<Day> list = new List<Day>();
            DateTime monday = dateTime.Date.AddDays(DayOfWeek.Monday - dateTime.DayOfWeek);

            var dict = LoadAppointments();

            for (int i = 0; i < 28; i++) {
                var date = monday.AddDays(i);
                Day day = new Day(date);
                List<Appointment> appointments;
                foreach (Appointment appointment in dict.TryGetValue(date.Date, out appointments) ? appointments : new List<Appointment>())
                {
                    day.Appointments.Add(appointment);
                }
                list.Add(day);
            }

            return list;
        }
        private Dictionary<DateTime, List<Appointment>> LoadAppointments() {
            Dictionary<DateTime, List<Appointment>> dict = new Dictionary<DateTime, List<Appointment>>();
            List<Appointment> list = storage.GetAppointments(person);

            foreach (var a in list.OrderBy(a => a.StartTime))
            {
                var date = a.StartTime.Date;
                List<Appointment> appointments;
                dict.TryGetValue(date, out appointments);
                if (appointments == null) {
                    appointments = new List<Appointment>();
                }
                appointments.Add(a);
                dict[date] = appointments;
            }
            return dict;
        }

        public List<Day> GetDaysWithNow()
        {
            return GetDays(DateTime.Now);
        }
    }
}
