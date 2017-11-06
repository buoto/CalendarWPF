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

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();

        public Day(DateTime dateTime)
        {
            DateTime = dateTime;
        }
    }
}
