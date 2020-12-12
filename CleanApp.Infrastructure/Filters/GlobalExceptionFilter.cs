using CleanApp.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CleanApp.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var response = new ProblemDetails()
            {
                Type = HttpStatusCode.InternalServerError.ToString(),
                Title = context.Exception.Message,
                Status = (int)HttpStatusCode.InternalServerError
            };

            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                var responseType = (HttpStatusCode)(exception.StatusCode ?? (int)HttpStatusCode.BadRequest);

                response.Type = responseType.ToString();
                response.Status = exception.StatusCode ?? (int)HttpStatusCode.BadRequest;
            };

            context.Result = new ObjectResult(response)
            {
                StatusCode = response.Status
            };
            //context.HttpContext.Response.StatusCode = (int)response.Status;
            context.ExceptionHandled = true;
        }
    }
}
