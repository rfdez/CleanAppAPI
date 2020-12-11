using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class CleanlinessController : ControllerBase
    {
    }
}
