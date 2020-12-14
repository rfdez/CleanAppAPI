using CleanApp.Core.Enumerations;
using System;

namespace CleanApp.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public ErrorCodes StatusCode { get; set; }

        public object Value { get; set; }
    }
}
