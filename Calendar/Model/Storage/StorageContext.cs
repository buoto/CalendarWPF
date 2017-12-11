using System.Data.Entity;

namespace Calendar.Model
{
    class StorageContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
    }
}
