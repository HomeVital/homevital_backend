using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeVital.Models.Exceptions
{
    public class VarArgumentException : Exception
    {
        public VarArgumentException(string message) : base(message) {}
    }
   
}