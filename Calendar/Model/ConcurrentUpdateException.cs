using System;

namespace Calendar.Model
{
    [Serializable]
    class ConcurrentUpdateException : Exception
    {
        public ConcurrentUpdateException(string message)
            : base(message) { }
    }
}
