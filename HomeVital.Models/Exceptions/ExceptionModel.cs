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
}