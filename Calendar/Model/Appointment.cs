using System;

namespace Calendar.Model
{
    internal class Appointment
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Appointment(string name)
        {
            Name = name;
        }
    }
}