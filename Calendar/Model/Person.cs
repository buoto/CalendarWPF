using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [MaxLength(32), Required, Index(IsUnique = true)]
        public string UserID { get; set; }

        public virtual List<Attendance> Attendances { get; set; }
    }
}
