using System;

namespace HomeVital.Models.Exceptions
{
    public class UserException : Exception
    {
        public UserException(string message) : base(message) {}
    }
}