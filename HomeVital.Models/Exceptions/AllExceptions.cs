using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeVital.Models.Exceptions
{
    public class ExceptionModel
    {
        public int StatusCode { get; set; }
        public string ExceptionMessage { get; set; } = null!;
        public string? StackTrace { get; set; }
        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public class ModelFormatException : Exception
    {
        public ModelFormatException(string message) : base(message) { }
        public ModelFormatException(string message, Exception innerException) : base(message, innerException) { }
        public ModelFormatException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 400;
        public string? StackTrace { get; set; }
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }
        public NotFoundException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 404;
        public string? StackTrace { get; set; }
    }
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
        public BadRequestException(string message, Exception innerException) : base(message, innerException) { }
        public BadRequestException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 400;
        public string? StackTrace { get; set; }
    }

    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message) : base(message) { }
        public UnauthorizedException(string message, Exception innerException) : base(message, innerException) { }
        public UnauthorizedException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 401;
        public string? StackTrace { get; set; }
    }

    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message) : base(message) { }
        public ForbiddenException(string message, Exception innerException) : base(message, innerException) { }
        public ForbiddenException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 403;
        public string? StackTrace { get; set; }
    }

    public class ConflictException : Exception
    {
        public ConflictException(string message) : base(message) { }
        public ConflictException(string message, Exception innerException) : base(message, innerException) { }
        public ConflictException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 409;
        public string? StackTrace { get; set; }
    }

    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message) : base(message) { }
        public InternalServerErrorException(string message, Exception innerException) : base(message, innerException) { }
        public InternalServerErrorException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 500;
        public string? StackTrace { get; set; }
    }

    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException(string message) : base(message) { }
        public ServiceUnavailableException(string message, Exception innerException) : base(message, innerException) { }
        public ServiceUnavailableException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 503;
        public string? StackTrace { get; set; }
    }

    public class NotImplementedException : Exception
    {
        public NotImplementedException(string message) : base(message) { }
        public NotImplementedException(string message, Exception innerException) : base(message, innerException) { }
        public NotImplementedException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 501;
        public string? StackTrace { get; set; }
    }

    public class NotAcceptableException : Exception
    {
        public NotAcceptableException(string message) : base(message) { }
        public NotAcceptableException(string message, Exception innerException) : base(message, innerException) { }
        public NotAcceptableException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 406;
        public string? StackTrace { get; set; }
    }

    public class UnprocessableEntityException : Exception
    {
        public UnprocessableEntityException(string message) : base(message) { }
        public UnprocessableEntityException(string message, Exception innerException) : base(message, innerException) { }
        public UnprocessableEntityException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 422;
        public string? StackTrace { get; set; }
    }

    public class TooManyRequestsException : Exception
    {
        public TooManyRequestsException(string message) : base(message) { }
        public TooManyRequestsException(string message, Exception innerException) : base(message, innerException) { }
        public TooManyRequestsException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 429;
        public string? StackTrace { get; set; }
    }

    public class GatewayTimeoutException : Exception
    {
        public GatewayTimeoutException(string message) : base(message) { }
        public GatewayTimeoutException(string message, Exception innerException) : base(message, innerException) { }
        public GatewayTimeoutException(string message, string stackTrace) : base(message)
        {
            StackTrace = stackTrace;
        }
        public int StatusCode { get; set; } = 504;
        public string? StackTrace { get; set; }
    }

    
   

    
}