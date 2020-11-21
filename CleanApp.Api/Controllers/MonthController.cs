using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.CustomEntities;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using CleanApp.Core.Services;
using CleanApp.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CleanApp.Api.Controllers
{
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

        [HttpGet(Name = nameof(Get))]
        public IActionResult Get([FromQuery] MonthQueryFilter filters)
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
                NextPageUrl = _uriSerice.GetMonthPaginationUri(filters, Url.RouteUrl(nameof(Get))).ToString(),
                PreviousPageUrl = _uriSerice.GetMonthPaginationUri(filters, Url.RouteUrl(nameof(Get))).ToString()

            };
            var response = new ApiResponse<IEnumerable<MonthDto>>(monthsDto)
            {
                Meta = metadata
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var month = await _monthService.GetMonth(id);
            var monthDto = _mapper.Map<MonthDto>(month);

            var response = new ApiResponse<MonthDto>(monthDto);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MonthDto monthDto)
        {
            var month = _mapper.Map<Month>(monthDto);
            var inserted = await _monthService.InsertMonth(month);
            monthDto = _mapper.Map<MonthDto>(month);

            var response = new ApiResponse<string>("Ningún registro insertado.");

            if (inserted)
            {
                return Created($"{monthDto.YearId}", new ApiResponse<MonthDto>(monthDto));

            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, MonthDto monthDto)
        {
            var month = _mapper.Map<Month>(monthDto);
            month.YearId = id;

            var updated = await _monthService.UpdateMonthAsync(month);
            monthDto = _mapper.Map<MonthDto>(month);

            var response = new ApiResponse<string>("Ningún registro actualizado.");

            if (updated)
            {
                return Ok(new ApiResponse<MonthDto>(monthDto));
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _monthService.DeleteMonth(id);

            var response = new ApiResponse<string>("Ningún registro eliminado.");

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
