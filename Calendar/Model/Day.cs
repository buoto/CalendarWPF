using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    class Day
    {
        public DateTime DateTime { get; set; }

        public List<Appointment> Appointments { get; set; }

        public Day(DateTime dateTime)
        {
            DateTime = dateTime;
            Appointments = new List<Appointment>();
        }
    }
}
