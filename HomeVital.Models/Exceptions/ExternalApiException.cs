using System;

namespace HomeVital.Models.Exceptions
{
    public class ExternalApiException : Exception
    {
        public ExternalApiException(String message) : base(message) {}
    }
}