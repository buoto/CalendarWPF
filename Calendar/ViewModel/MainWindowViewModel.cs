using Calendar.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {

        public List<Day> Days { get; set; }

        public MainWindowViewModel()
        {
            Days = new List<Day>();
            DateTime monday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek);
            for (int i = 0; i < 28; i++) {
                Days.Add(new Day(monday.AddDays(i)));
            }

            Days[1].Appointments.Add(new Appointment("FooBar"));
        }
    }
}
