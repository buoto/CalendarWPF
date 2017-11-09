using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class MockStore : IStore
    {
        public void AddAppointment(Appointment appointment)
        {
        }

        public void DeleteAppointment(Appointment appointment)
        {
        }

        public List<Day> GetDays(DateTime dateTime)
        {
            List<Day> list = new List<Day>();
            DateTime monday = dateTime.AddDays(DayOfWeek.Monday - dateTime.DayOfWeek);

            for (int i = 0; i < 28; i++) {
                list.Add(new Day(monday.AddDays(i)));
            }

            list[3].Appointments.Add(new Appointment("FooBar", DateTime.Now, DateTime.Now.AddHours(1)));
            return list;
        }

        public List<Day> GetDaysWithNow()
        {
            return GetDays(DateTime.Now);
        }
    }
}
