using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.CustomEntities;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using CleanApp.Core.Services;
using CleanApp.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.Api.Controllers
{
    //[Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class WeekController : ControllerBase
    {
        private readonly IWeekService _weekService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public WeekController(IWeekService weekService, IMapper mapper, IUriService uriService)
        {
            _weekService = weekService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Obtiene todas las semanas
        /// </summary>
        /// <param name="filters">Filtrar semanas por mes</param>
        /// <returns>Lista de semanas</returns>
        [HttpGet(Name = nameof(GetWeeks))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<WeekDto>>), StatusCodes.Status200OK)]
        public IActionResult GetWeeks([FromQuery] WeekQueryFilter filters)
        {
            var weeks = _weekService.GetWeeks(filters);
            var weeksDto = _mapper.Map<IEnumerable<WeekDto>>(weeks);

            var metadata = new Metadata
            {
                TotalCount = weeks.TotalCount,
                PageSize = weeks.PageSize,
                CurrentPage = weeks.CurrentPage,
                TotalPages = weeks.TotalPages,
                HasNextPage = weeks.HasNextPage,
                HasPreviousPage = weeks.HasPreviousPage,
                NextPageUrl = _uriSerice.GetWeekPaginationUri(filters, Url.RouteUrl(nameof(GetWeeks))).ToString(),
                PreviousPageUrl = _uriSerice.GetWeekPaginationUri(filters, Url.RouteUrl(nameof(GetWeeks))).ToString()
            };

            var response = new ApiResponse<IEnumerable<WeekDto>>(weeksDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el mes solicitado
        /// </summary>
        /// <param name="id">Identidficador de la semana</param>
        /// <returns>Una semana</returns>
        [HttpGet("{id}", Name = nameof(GetWeek))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<WeekDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWeek(int id)
        {
            var week = await _weekService.GetWeek(id);
            var weekDto = _mapper.Map<WeekDto>(week);

            var response = new ApiResponse<WeekDto>(weekDto);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(WeekDto weekDto)
        {
            var week = _mapper.Map<Week>(weekDto);
            var inserted = await _weekService.InsertWeek(week);
            weekDto = _mapper.Map<WeekDto>(week);

            var response = new ApiResponse<string>("Ningún registro insertado.");

            if (inserted)
            {
                return Created($"{weekDto.Id}", new ApiResponse<WeekDto>(weekDto));

            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, WeekDto weekDto)
        {
            var week = _mapper.Map<Week>(weekDto);
            week.Id = id;

            var updated = await _weekService.UpdateWeekAsync(week);
            weekDto = _mapper.Map<WeekDto>(week);

            var response = new ApiResponse<string>("Ningún registro actualizado.");

            if (updated)
            {
                return Ok(new ApiResponse<WeekDto>(weekDto));
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _weekService.DeleteWeek(id);

            var response = new ApiResponse<string>("Ningún registro eliminado.");

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
