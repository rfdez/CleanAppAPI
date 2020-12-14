using CleanApp.Core.CustomEntities;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.Api.Responses
{
    public class ApiResponse<T> : ActionResult
    {
        public ApiResponse(T data)
        {
            Data = data;
        }

        public T Data { get; set; }

        public Metadata Meta { get; set; }
    }
}
