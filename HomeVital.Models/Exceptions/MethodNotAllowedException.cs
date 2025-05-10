using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HomeVital.Models.Exceptions
{
    public class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException(string message) : base(message) {}
    }
}