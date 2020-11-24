using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class WeekController : ControllerBase
    {
        private readonly IWeekService _weekService;
        private readonly IMapper _mapper;

        public WeekController(IWeekService weekService, IMapper mapper)
        {
            _weekService = weekService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var weeks = _weekService.GetWeeks();
            var weeksDto = _mapper.Map<IEnumerable<WeekDto>>(weeks);

            var response = new ApiResponse<IEnumerable<WeekDto>>(weeksDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
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
