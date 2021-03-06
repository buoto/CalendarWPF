﻿using Calendar.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Calendar.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MainWindowViewModel));
        private IStore store;

        public List<Appointment> Appointments { get; private set; }

        private String styleValue;
        private String fontStyle = "Consolas";

        public RelayCommand PrevCommand { get; set; }
        public RelayCommand NextCommand { get; set; }
        private ObservableCollection<Day> days;
        public ObservableCollection<Day> Days {
            get { return days; }
            set {
                days = value;

                OnPropertyChanged("Days");
                OnPropertyChanged("FirstWeek");
                OnPropertyChanged("SecondWeek");
                OnPropertyChanged("ThirdWeek");
                OnPropertyChanged("FourthWeek");
            }
        }

        public string FirstWeek { get { return String.Format("W{0:00}\n{1}", Days[0].DateTime.DayOfYear/7, Days[0].DateTime.Year); }}
        public string SecondWeek { get { return String.Format("W{0:00}\n{1}", Days[7].DateTime.DayOfYear/7, Days[7].DateTime.Year); }}
        public string ThirdWeek { get { return String.Format("W{0:00}\n{1}", Days[14].DateTime.DayOfYear/7, Days[14].DateTime.Year); }}
        public string FourthWeek { get { return String.Format("W{0:00}\n{1}", Days[21].DateTime.DayOfYear/7, Days[21].DateTime.Year); }}

        public string StyleValue { get { return styleValue; } set { styleValue = value; OnPropertyChanged("StyleValue"); } }

        public string FontStyle { get { return fontStyle; } set { fontStyle = value; OnPropertyChanged("FontStyle"); } }

        public MainWindowViewModel(IStore store)
        {
            this.store = store;
            
            PrevCommand = new RelayCommand(o =>
                {
                    Days = new ObservableCollection<Day>(store.GetDays(Days[0].DateTime.AddDays(-7)));
                }
            );

            NextCommand = new RelayCommand(o =>
                {
                    Days = new ObservableCollection<Day>(store.GetDays(Days[0].DateTime.AddDays(7)));
                }
            );

            Days = new ObservableCollection<Day>(store.GetDaysWithNow());
        }

        public void EditAppointment(Appointment old, Appointment appointment) {
            foreach (var day in Days) {
                if (day.DateTime.Year == old.StartTime.Year && day.DateTime.Month == old.StartTime.Month && day.DateTime.Day == old.StartTime.Day) {
                    try
                    {
                        store.EditAppointment(old, appointment);
                        if (day.Appointments.Remove(old))
                        {
                            day.AddAppointment(appointment);
                            break;
                        }
                    }
                    catch (ConcurrentUpdateException e)
                    {
                        Days = new ObservableCollection<Day>(store.GetDays(Days[0].DateTime));
                        throw e;
                    }
                    
                }
            }
        }

        public void DeleteAppointment(Appointment appointment) { 
            foreach (var day in Days) {
                if (day.DateTime.Year == appointment.StartTime.Year && day.DateTime.Month == appointment.StartTime.Month && day.DateTime.Day == appointment.StartTime.Day) {
                    if (day.Appointments.Remove(appointment))
                    {
                        store.DeleteAppointment(appointment);
                        break;
                    }
                }
            }
        }

        public void AddAppointment(Day day, Appointment appointment) {
            day.AddAppointment(appointment);
            store.AddAppointment(appointment);
        }
    }
}
