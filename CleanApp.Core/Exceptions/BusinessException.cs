using System;

namespace CleanApp.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public int? StatusCode { get; set; }
    }
}
