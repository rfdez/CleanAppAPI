using CleanApp.Core.Enumerations;
using CleanApp.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanApp.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var statusCode = ErrorCodes.InternalServerError;

            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                statusCode = exception.StatusCode;
            };

            context.Result = new ObjectResult(context.Exception.Message)
            {
                StatusCode = (int)statusCode
            };
            context.ExceptionHandled = true;
        }
    }
}
