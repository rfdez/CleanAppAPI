using CleanApp.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CleanApp.Infrastructure.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        // TODO: Hacerlo async y segun el status que se le pase mandar una response u otra
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(BusinessException))
            {
                var exception = (BusinessException)context.Exception;
                var validation = new
                {
                    Status = 400,
                    Title = "Bad Request",
                    Detail = exception.Message
                };

                var json = new
                {
                    errors = new[] { validation }
                };

                context.Result = new BadRequestObjectResult(json);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            };

            var response = new ProblemDetails()
            {
                Type = HttpStatusCode.InternalServerError.ToString(),
                Title = context.Exception.GetType().ToString(),
                Status = (int)HttpStatusCode.InternalServerError,
                Detail = context.Exception.Message,
                Instance = context.Exception.Source
            };

            context.Result = new ObjectResult(response);
            context.HttpContext.Response.StatusCode = (int)response.Status;
            context.ExceptionHandled = true;
        }
    }
}

