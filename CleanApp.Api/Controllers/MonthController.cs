using AutoMapper;
using CleanApp.Core.CustomEntities;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Enumerations;
using CleanApp.Core.QueryFilters;
using CleanApp.Core.Responses;
using CleanApp.Core.Services;
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
    public class MonthController : ControllerBase
    {
        private readonly IMonthService _monthService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public MonthController(IMonthService monthService, IMapper mapper, IUriService uriService)
        {
            _monthService = monthService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Obtiene todos los meses
        /// </summary>
        /// <param name="filters">Filtrar por año</param>
        /// <returns>Lista de meses</returns>
        [HttpGet(Name = nameof(GetMonths))]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MonthDto>>), StatusCodes.Status200OK)]
        public IActionResult GetMonths([FromQuery] MonthQueryFilter filters)
        {
            var months = _monthService.GetMonths(filters);
            var monthsDto = _mapper.Map<IEnumerable<MonthDto>>(months);

            var metadata = new Metadata
            {
                TotalCount = months.TotalCount,
                PageSize = months.PageSize,
                CurrentPage = months.CurrentPage,
                TotalPages = months.TotalPages,
                HasNextPage = months.HasNextPage,
                HasPreviousPage = months.HasPreviousPage,
                NextPageUrl = months.HasNextPage ? _uriSerice.GetPaginationUri((int)months.NextPageNumber, months.PageSize, Url.RouteUrl(nameof(GetMonths))).ToString() : null,
                PreviousPageUrl = months.HasPreviousPage ? _uriSerice.GetPaginationUri((int)months.NextPageNumber, months.PageSize, Url.RouteUrl(nameof(GetMonths))).ToString() : null

            };

            var response = new ApiResponse<IEnumerable<MonthDto>>(monthsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Devuelve el mes solicitado
        /// </summary>
        /// <param name="id">Identificador del mes</param>
        /// <returns>Un mes</returns>
        [HttpGet("{id}", Name = nameof(GetMonth))]
        [ProducesResponseType(typeof(ApiResponse<MonthDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonth(int id)
        {
            var month = await _monthService.GetMonth(id);
            var monthDto = _mapper.Map<MonthDto>(month);

            var response = new ApiResponse<MonthDto>(monthDto);

            return Ok(response);
        }

        /// <summary>
        /// Inserta un mes a un año
        /// </summary>
        /// <param name="monthDto">Mes a insertar</param>
        /// <returns>Mes insertado</returns>
        [HttpPost(Name = nameof(InsertMonth))]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [ProducesResponseType(typeof(ApiResponse<MonthDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertMonth(MonthDto monthDto)
        {
            var month = _mapper.Map<Month>(monthDto);
            await _monthService.InsertMonth(month);
            monthDto = _mapper.Map<MonthDto>(month);

            return CreatedAtAction(nameof(GetMonth), new { id = monthDto.Id }, new ApiResponse<MonthDto>(monthDto));
        }

        /// <summary>
        /// Actualiza el mes de un año
        /// </summary>
        /// <param name="id">Identificador del mes</param>
        /// <param name="monthDto">Nuevo valor del mes</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateMonthAsync))]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMonthAsync(int id, MonthDto monthDto)
        {
            var month = _mapper.Map<Month>(monthDto);
            month.Id = id;

            await _monthService.UpdateMonthAsync(month);

            return NoContent();
        }

        /// <summary>
        /// Borra el mes de un año
        /// </summary>
        /// <param name="id">Identificador del mes</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteMonth))]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMonth(int id)
        {
            await _monthService.DeleteMonth(id);

            return NoContent();
        }
    }
}
