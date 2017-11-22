using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Day : INotifyPropertyChanged
    {
        public DateTime DateTime { get; set; }
        public ObservableCollection<Event> Appointments {
            get { return appointments; }
            set
            {
                if (value == appointments)
                    return;
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        private ObservableCollection<Event> appointments;

        public Day(DateTime dateTime)
        {
            DateTime = dateTime;
            Appointments = new ObservableCollection<Event>(new List<Event>());
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
    }
}
