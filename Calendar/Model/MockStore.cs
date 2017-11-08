using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    class MockStore : IStore
    {
        public void AddAppointment(Appointment appointment)
        {
        }

        public void DeleteAppointment(Appointment appointment)
        {
        }

        public List<Day> GetDaysWithNow()
        {
            return new List<Day>();
        }
    }
}
