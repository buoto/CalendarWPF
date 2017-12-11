using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    [Serializable]
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual List<Attendance> Attendances { get; set; }

        public override string ToString()
        {
            return String.Format("{0}-{1} {2}", StartTime, EndTime, Title);
        }
    }
}
