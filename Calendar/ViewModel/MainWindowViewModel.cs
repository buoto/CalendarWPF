using Calendar.Model;
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

        public ObservableCollection<Day> Days { get; set; }

        public MainWindowViewModel(IStore store)
        {
            this.store = store;

            Days = new ObservableCollection<Day>(store.GetDaysWithNow());
            DateTime monday = DateTime.Now.AddDays(DayOfWeek.Monday - DateTime.Now.DayOfWeek);
            for (int i = 0; i < 28; i++) {
                Days.Add(new Day(monday.AddDays(i)));
            }

            Days[3].Appointments.Add(new Appointment("FooBar", DateTime.Now, DateTime.Now.AddHours(1)));
        }

        public void EditAppointment(Appointment old, Appointment appointment) {
            foreach (var day in Days) {
                if (day.DateTime.Year == old.Start.Year && day.DateTime.Month == old.Start.Month && day.DateTime.Day == old.Start.Day) {
                    if (day.Appointments.Remove(old)) {
                        day.Appointments.Add(appointment);
                        day.Appointments = new ObservableCollection<Appointment>(day.Appointments.OrderBy(a => a.Start));
                    }
                }
            }
        }

        public void DeleteAppointment(Appointment appointment) { 
            foreach (var day in Days) {
                if (day.DateTime.Year == appointment.Start.Year && day.DateTime.Month == appointment.Start.Month && day.DateTime.Day == appointment.Start.Day) {
                    if (day.Appointments.Remove(appointment))
                        break;
                }
            }
        }

        public void AddAppointment(Day day, Appointment appointment) {
            day.Appointments.Add(appointment);
            day.Appointments = new ObservableCollection<Appointment>(day.Appointments.OrderBy(a => a.Start));
        }
    }
}
