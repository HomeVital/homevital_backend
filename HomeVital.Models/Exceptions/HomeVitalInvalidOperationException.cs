using System;

namespace HomeVital.Models.Exceptions
{
    public class HomeVitalInvalidOperationException : Exception
    {
        public HomeVitalInvalidOperationException(string message) : base(message) {}
    }
}