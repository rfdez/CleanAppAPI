using AutoMapper;
using CleanApp.Core.DTOs;
using CleanApp.Core.Interfaces.Services;
using CleanApp.Core.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeTenantController : ControllerBase
    {
        private readonly IHomeTenantService _homeTenantService;
        private readonly IMapper _mapper;

        public HomeTenantController(IHomeTenantService homeTenantService, IMapper mapper)
        {
            _homeTenantService = homeTenantService;
            _mapper = mapper;
        }

        [HttpGet(Name = nameof(GetHomeTenants))]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<HomeTenantDto>>), StatusCodes.Status200OK)]
        public IActionResult GetHomeTenants()
        {
            var homeTenants = _homeTenantService.GetHomeTenants();
            var homeTenantsDto = _mapper.Map<IEnumerable<HomeTenantDto>>(homeTenants);

            var response = new ApiResponse<IEnumerable<HomeTenantDto>>(homeTenantsDto);

            return Ok(response);
        }
    }
}
