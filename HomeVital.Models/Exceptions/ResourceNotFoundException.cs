using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeVital.Models.Exceptions
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException(string message) : base(message) {}
    }
}