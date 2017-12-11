using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.Model
{
    public class Attendance
    {
        [Key]
        public Guid AttendanceId { get; set; }

        public Guid AppointmentId { get; set; }
        public Guid PersonId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Person Person { get; set; }
        public bool Accepted { get; set; }
    }
}
