using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthController : ControllerBase
    {
        private readonly IMonthService _monthService;
        private readonly IMapper _mapper;

        public MonthController(IMonthService monthService, IMapper mapper)
        {
            _monthService = monthService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var months = _monthService.GetMonths();
            var monthsDto = _mapper.Map<IEnumerable<MonthDto>>(months);

            return Ok(monthsDto);
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
