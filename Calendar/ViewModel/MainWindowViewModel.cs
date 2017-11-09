﻿using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        private IStore store;

        public RelayCommand PrevCommand { get; }
        public RelayCommand NextCommand { get; }
        private ObservableCollection<Day> days;
        public ObservableCollection<Day> Days {
            get => days;
            set {
                days = value;

                OnPropertyChanged("Days");
                OnPropertyChanged("FirstWeek");
                OnPropertyChanged("SecondWeek");
                OnPropertyChanged("ThirdWeek");
                OnPropertyChanged("FourthWeek");
            }
        }

        public string FirstWeek { get => String.Format("W{0:00}\n{1}", Days[0]?.DateTime.DayOfYear/7, Days[0]?.DateTime.Year); }
        public string SecondWeek { get => String.Format("W{0:00}\n{1}", Days[7]?.DateTime.DayOfYear/7, Days[7]?.DateTime.Year); }
        public string ThirdWeek { get => String.Format("W{0:00}\n{1}", Days[14]?.DateTime.DayOfYear/7, Days[14]?.DateTime.Year); }
        public string FourthWeek { get => String.Format("W{0:00}\n{1}", Days[21]?.DateTime.DayOfYear/7, Days[21]?.DateTime.Year); }

        public MainWindowViewModel(IStore store)
        {
            this.store = store;

            PrevCommand = new RelayCommand(
                new Action<object>(delegate (object obj)
                {
                    Days = new ObservableCollection<Day>(store.GetDays(Days[0].DateTime.AddDays(-7)));
                }
            ));

            NextCommand = new RelayCommand(
                new Action<object>(delegate (object obj)
                {
                    Days = new ObservableCollection<Day>(store.GetDays(Days[0].DateTime.AddDays(7)));
                }
            ));

            Days = new ObservableCollection<Day>(store.GetDaysWithNow());
        }

        public void EditAppointment(Appointment old, Appointment appointment) {
            foreach (var day in Days) {
                if (day.DateTime.Year == old.Start.Year && day.DateTime.Month == old.Start.Month && day.DateTime.Day == old.Start.Day) {
                    if (day.Appointments.Remove(old)) {
                        day.Appointments.Add(appointment);
                        day.Appointments = new ObservableCollection<Appointment>(day.Appointments.OrderBy(a => a.Start));
                        store.DeleteAppointment(old);
                        store.AddAppointment(appointment);
                    }
                }
            }
        }

        public void DeleteAppointment(Appointment appointment) { 
            foreach (var day in Days) {
                if (day.DateTime.Year == appointment.Start.Year && day.DateTime.Month == appointment.Start.Month && day.DateTime.Day == appointment.Start.Day) {
                    if (day.Appointments.Remove(appointment))
                    {
                        store.DeleteAppointment(appointment);
                        break;
                    }
                }
            }
        }

        public void AddAppointment(Day day, Appointment appointment) {
            day.Appointments.Add(appointment);
            day.Appointments = new ObservableCollection<Appointment>(day.Appointments.OrderBy(a => a.Start));
            store.AddAppointment(appointment);
        }
    }
}
