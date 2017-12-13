using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace Calendar.Model
{
    public class Day : INotifyPropertyChanged
    {
        public DateTime DateTime { get; set; }
        public ObservableCollection<Appointment> Appointments {
            get { return appointments; }
            set
            {
                if (value == appointments)
                    return;
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        private ObservableCollection<Appointment> appointments;

        public Day(DateTime dateTime)
        {
            DateTime = dateTime;
            Appointments = new ObservableCollection<Appointment>(new List<Appointment>());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public override bool Equals(object obj)
        {
            var day = obj as Day;
            return day != null &&
                   DateTime == day.DateTime &&
                   EqualityComparer<ObservableCollection<Appointment>>.Default.Equals(appointments, day.appointments);
        }

        public void AddAppointment(Appointment appointment)
        {
            appointments.Add(appointment);
            Appointments = new ObservableCollection<Appointment>(Appointments.OrderBy(a => a.StartTime));
        }
    }
}
