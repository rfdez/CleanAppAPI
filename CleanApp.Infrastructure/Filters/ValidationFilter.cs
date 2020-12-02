using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using System.Threading.Tasks;

namespace CleanApp.Infrastructure.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var response = new ValidationProblemDetails(modelState: context.ModelState)
            {
                Type = HttpStatusCode.BadRequest.ToString(),
                Status = (int)HttpStatusCode.BadRequest
            };

            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(response);
                return;
            }

            await next();
        }
    }
}
