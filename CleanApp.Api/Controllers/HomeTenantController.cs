using AutoMapper;
using CleanApp.Core.CustomEntities;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Services;
using CleanApp.Core.QueryFilters;
using CleanApp.Core.Responses;
using CleanApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class HomeTenantController : ControllerBase
    {
        private readonly IHomeTenantService _homeTenantService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public HomeTenantController(IHomeTenantService homeTenantService, IMapper mapper, IUriService uriService)
        {
            _homeTenantService = homeTenantService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Obtiene relaciones entre inquilinos y viviendas
        /// </summary>
        /// <param name="filters">Filtrar por vivienda, inquilino o rol</param>
        /// <returns>Lista de relaciones entre inquilinos y viviendas</returns>
        [HttpGet(Name = nameof(GetHomeTenants))]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<HomeTenantDto>>), StatusCodes.Status200OK)]
        public IActionResult GetHomeTenants([FromQuery] HomeTenantQueryFilter filters)
        {
            var homeTenants = _homeTenantService.GetHomeTenants(filters);
            var homeTenantsDto = _mapper.Map<IEnumerable<HomeTenantDto>>(homeTenants);

            var metadata = new Metadata
            {
                TotalCount = homeTenants.TotalCount,
                PageSize = homeTenants.PageSize,
                CurrentPage = homeTenants.CurrentPage,
                TotalPages = homeTenants.TotalPages,
                HasNextPage = homeTenants.HasNextPage,
                HasPreviousPage = homeTenants.HasPreviousPage,
                NextPageUrl = _uriSerice.GetPaginationUri((int)homeTenants.NextPageNumber, homeTenants.PageSize, Url.RouteUrl(nameof(GetHomeTenants))).ToString(),
                PreviousPageUrl = _uriSerice.GetPaginationUri((int)homeTenants.NextPageNumber, homeTenants.PageSize, Url.RouteUrl(nameof(GetHomeTenants))).ToString()

            };

            var response = new ApiResponse<IEnumerable<HomeTenantDto>>(homeTenantsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el rol de un inquilino en una vivienda
        /// </summary>
        /// <param name="homeId">Identificador de la vivienda</param>
        /// <param name="tenantId">Identificador del inquilino</param>
        /// <returns>Relación del inquilino con la vivienda</returns>
        [HttpGet("home/{homeId}/tenant/{tenantId}", Name = nameof(GetHomeTenant))]
        [ProducesResponseType(typeof(ApiResponse<HomeTenantDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHomeTenant(int homeId, int tenantId)
        {
            var homeTenant = await _homeTenantService.GetHomeTenant(homeId, tenantId);
            var homeTenantDto = _mapper.Map<HomeTenantDto>(homeTenant);

            var response = new ApiResponse<HomeTenantDto>(homeTenantDto);

            return Ok(response);
        }

        /// <summary>
        /// Inserta un inquilino a una vivienda
        /// </summary>
        /// <param name="homeTenantDto">Relación del inquilino con la vivienda</param>
        /// <returns>Relación insertada</returns>
        [HttpPost(Name = nameof(InsertHomeTenant))]
        [ProducesResponseType(typeof(ApiResponse<HomeTenantDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertHomeTenant(HomeTenantDto homeTenantDto)
        {
            var homeTenant = _mapper.Map<HomeTenant>(homeTenantDto);
            await _homeTenantService.InsertHomeTenant(homeTenant);
            homeTenantDto = _mapper.Map<HomeTenantDto>(homeTenant);

            var response = new ApiResponse<HomeTenantDto>(homeTenantDto);

            return CreatedAtAction(nameof(GetHomeTenant), new { homeId = homeTenantDto.HomeId, tenantId = homeTenantDto.TenantId }, response);
        }

        /// <summary>
        /// Actualiza la relación de un inquilino con una vivienda
        /// </summary>
        /// <param name="homeId">Identificador de la vivienda</param>
        /// <param name="tenantId">Identificador del inquilino</param>
        /// <param name="homeTenantDto">Nueva relación del inquilino con la vivienda</param>
        /// <returns></returns>
        [HttpPut("home/{homeId}/tenant/{tenantId}", Name = nameof(UpdateHomeTenantAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateHomeTenantAsync(int homeId, int tenantId, HomeTenantDto homeTenantDto)
        {
            var homeTenant = _mapper.Map<HomeTenant>(homeTenantDto);
            homeTenant.HomeId = homeId;
            homeTenant.TenantId = tenantId;

            await _homeTenantService.UpdateHomeTenantAsync(homeTenant);

            return NoContent();
        }

        /// <summary>
        /// Elimina la relación del inquilino con una vivienda
        /// </summary>
        /// <param name="homeId">Identificador de la vivienda</param>
        /// <param name="tenantId">Identificador del inquilino</param>
        /// <returns></returns>
        [HttpDelete("home/{homeId}/tenant/{tenantId}", Name = nameof(DeleteHomeTenant))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteHomeTenant(int homeId, int tenantId)
        {
            await _homeTenantService.DeleteHomeTenant(homeId, tenantId);

            return NoContent();
        }
    }
}
