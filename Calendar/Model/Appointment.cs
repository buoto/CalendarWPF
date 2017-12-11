using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Calendar.Model
{
    [Serializable]
    public class Appointment
    {
        [Key]
        public Guid AppointmentId { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

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
