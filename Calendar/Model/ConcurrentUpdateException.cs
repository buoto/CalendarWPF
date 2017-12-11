using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    class ConcurrentUpdateException : Exception
    {
        public ConcurrentUpdateException(string message)
            : base(message) { }
    }
}
