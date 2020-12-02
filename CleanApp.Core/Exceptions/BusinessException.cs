using System;
using System.Net;

namespace CleanApp.Core.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }

        public HttpStatusCode Status { get; set; }
        public object Value { get; set; }
    }
}
