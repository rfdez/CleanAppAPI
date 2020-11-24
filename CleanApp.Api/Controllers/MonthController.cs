using System.Collections.Generic;
using System.Net;
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace CleanApp.Api.Controllers
{
    //[Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
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
        /// Devuelve todos los meses
        /// </summary>
        /// <param name="filters">Filtrar por año</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetMonths))]
        [ProducesDefaultResponseType]
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
                NextPageUrl = _uriSerice.GetMonthPaginationUri(filters, Url.RouteUrl(nameof(GetMonths))).ToString(),
                PreviousPageUrl = _uriSerice.GetMonthPaginationUri(filters, Url.RouteUrl(nameof(GetMonths))).ToString()

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
        /// <param name="id">Id del mes</param>
        /// <returns>Mes</returns>
        [HttpGet("{id}", Name = nameof(GetMonth))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<MonthDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMonth(int id)
        {
            var month = await _monthService.GetMonth(id);
            var monthDto = _mapper.Map<MonthDto>(month);

            var response = new ApiResponse<MonthDto>(monthDto);

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="monthDto"></param>
        /// <returns></returns>
        [HttpPost(Name = nameof(InsertMonth))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<MonthDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertMonth(MonthDto monthDto)
        {
            var month = _mapper.Map<Month>(monthDto);
            await _monthService.InsertMonth(month);
            monthDto = _mapper.Map<MonthDto>(month);

            return CreatedAtAction(nameof(GetMonth), new { id = monthDto.Id }, new ApiResponse<MonthDto>(monthDto));
        }

        [HttpPut("{id}", Name = nameof(UpdateMonthAsync))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateMonthAsync(int id, MonthDto monthDto)
        {
            var month = _mapper.Map<Month>(monthDto);
            month.YearId = id;

            await _monthService.UpdateMonthAsync(month);

            return NoContent();
        }

        [HttpDelete("{id}", Name = nameof(DeleteMonth))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteMonth(int id)
        {
            await _monthService.DeleteMonth(id);

            return NoContent();
        }
    }
}
