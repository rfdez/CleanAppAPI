using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;
        private readonly IMapper _mapper;

        public TenantController(ITenantService tenantService, IMapper mapper)
        {
            _tenantService = tenantService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var tenants = _tenantService.GetTenants();
            var tenantsDto = _mapper.Map<IEnumerable<TenantDto>>(tenants);

            var response = new ApiResponse<IEnumerable<TenantDto>>(tenantsDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var tenant = await _tenantService.GetTenant(id);
            var tenantDto = _mapper.Map<TenantDto>(tenant);

            var response = new ApiResponse<TenantDto>(tenantDto);

            return Ok(response);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(TenantDto tenantDto)
        {
            var tenant = _mapper.Map<Tenant>(tenantDto);
            var inserted = await _tenantService.InsertTenant(tenant);
            tenantDto = _mapper.Map<TenantDto>(tenant);

            var response = new ApiResponse<string>("Ningún registro insertado.");

            if (inserted)
            {
                return Created($"{tenantDto.Id}", new ApiResponse<TenantDto>(tenantDto));

            }

            return BadRequest(response);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TenantDto tenantDto)
        {
            var tenant = _mapper.Map<Tenant>(tenantDto);
            tenant.Id = id;

            var updated = await _tenantService.UpdateTenantAsync(tenant);
            tenantDto = _mapper.Map<TenantDto>(tenant);

            var response = new ApiResponse<string>("Ningún registro actualizado.");

            if (updated)
            {
                return Ok(new ApiResponse<TenantDto>(tenantDto));
            }

            return BadRequest(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _tenantService.DeleteTenant(id);

            var response = new ApiResponse<string>("Ningún registro eliminado.");

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
