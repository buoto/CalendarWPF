using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calendar.Model
{
    public class Person
    {
        [Key]
        public Guid PersonId { get; set; }
        [MaxLength(32)]
        public string FirstName { get; set; }
        [MaxLength(32)]
        public string LastName { get; set; }
        public string UserID { get; set; }
        public virtual List<Attendance> Attendances { get; set; }
    }
}
