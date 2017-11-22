using System;

namespace Calendar.Model
{
    [Serializable]
    public class Event
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Event()
        {
        }

        public Event(string name)
        {
            Name = name;
        }

        public Event(string name, DateTime start, DateTime end) : this(name)
        {
            Start = start;
            End = end;
        }

        public override string ToString()
        {
            return String.Format("{0}-{1} {2}", Start, End, Name);
        }
    }
}