using CleanApp.Core.Enumerations;
using System;

namespace CleanApp.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public ErrorCode StatusCode { get; set; } = ErrorCode.BadRequest;

        public object Value { get; set; }
    }
}
