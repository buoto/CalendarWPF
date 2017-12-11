using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public interface IStorage
    {
        List<Person> getPersons();
        List<Appointment> getAppointments();
    }
}
