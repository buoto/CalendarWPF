using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class Attendance
    {
        [Key]
        public Guid AttendanceId { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Person Person { get; set; }
        public bool accepted { get; set; }
    }
}
