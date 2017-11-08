using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    class Day
    {
        public DateTime DateTime { get; set; }

        public ObservableCollection<Appointment> Appointments { get; set; }

        public Day(DateTime dateTime)
        {
            DateTime = dateTime;
            Appointments = new ObservableCollection<Appointment>(new List<Appointment>());
        }
    }
}
