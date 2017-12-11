using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    class Storage : IStorage
    {
        public List<Appointment> getAppointments()
        {
            using (var db = new StorageContext())
                return db.Appointments.ToList();
        }

        public List<Person> getPersons()
        {
            using (var db = new StorageContext())
                return db.Persons.ToList();
        }

    }
}
