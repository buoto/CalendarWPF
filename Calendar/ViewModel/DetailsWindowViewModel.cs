﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Calendar.Model;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    class DetailsWindowViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand DeleteCommand { get; }

        public DetailsWindowViewModel()
        {
            SaveCommand = new RelayCommand(
                new Action<object>(delegate (object obj)
                {
                    DateTime time;
                    if (appointment == null)
                    {
                        time = Day.DateTime;
                    }
                    else
                    {
                        time = appointment.Start;
                    }
                    appointment = new Appointment(
                        Name,
                        new DateTime(time.Year, time.Month, time.Day, StartHour, StartMinute, 0),
                        new DateTime(time.Year, time.Month, time.Day, EndHour, EndMinute, 0));
                    CloseAction();
                }
            ));

            CloseCommand = new RelayCommand(
                new Action<object>(delegate (object obj)
                {
                    CloseAction();
                }
            ));

            DeleteCommand = new RelayCommand(
                new Action<object>(delegate (object obj)
                {
                    appointment = null;
                    CloseAction();
                }
            ));
        }

        public string Title { get; set; } = "Appointment";
         public Appointment Appointment {
            get
            {
                return appointment;
            }
            set
            {
                appointment = value;

                Name = value.Name;

                StartHour = value.Start.Hour;
                StartMinute = value.Start.Minute;

                EndHour = value.End.Hour;
                EndMinute = value.End.Minute;
            }
        }

        private string name;

        public string Name {
            get => name;
            set
            {
                if (name == value)
                    return;
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public int StartHour {
            get => startHour;
            set
            {
                if (startHour == value)
                    return;
                startHour = value;
                OnPropertyChanged("StartHour");
            }
        }
        public int StartMinute
        {
            get => startMinute;
            set
            {
                if (startMinute == value)
                    return;
                startMinute = value;
                OnPropertyChanged("StartMinute");
            }
        }
        public int EndHour {
            get => endHour;
            set
            {
                if (endHour == value)
                    return;
                endHour = value;
                OnPropertyChanged("EndHour");
            }
        }
        public int EndMinute {
            get => endMinute; 
            set
            {
                if (endMinute == value)
                    return;
                endMinute = value;
                OnPropertyChanged("EndMinute");
            }
        }

        public Day Day { get; set; }
        public Action CloseAction { get; set; }

        private int startHour, startMinute, endHour, endMinute;

        private Appointment appointment;

    }
}