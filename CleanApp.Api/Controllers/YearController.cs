using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces;
using CleanApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YearController : ControllerBase
    {
        private readonly IYearService _yearService;
        private readonly IMapper _mapper;

        public YearController(IYearService yearService, IMapper mapper)
        {
            _yearService = yearService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var years = _yearService.GetYears();
            var yearsDto = _mapper.Map<IEnumerable<YearDto>>(years);

            var response = new ApiResponse<IEnumerable<YearDto>>(yearsDto);

            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var year = await _yearService.GetYear(id);
            var yearDto = _mapper.Map<YearDto>(year);

            var response = new ApiResponse<YearDto>(yearDto);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(YearDto yearDto)
        {
            var year = _mapper.Map<Year>(yearDto);
            var inserted = await _yearService.InsertYear(year);
            yearDto = _mapper.Map<YearDto>(year);

            var response = new ApiResponse<string>("Ningún registro insertado.");

            if (inserted)
            {
                return Created($"{yearDto.Id}", new ApiResponse<YearDto>(yearDto));

            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, YearDto yearDto)
        {
            var year = _mapper.Map<Year>(yearDto);
            year.Id = id;

            var updated = await _yearService.UpdateYearAsync(year);
            yearDto = _mapper.Map<YearDto>(year);

            var response = new ApiResponse<string>("Ningún registro actualizado.");

            if (updated)
            {
                return Ok(new ApiResponse<YearDto>(yearDto));
            }

            return BadRequest(response);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _yearService.DeleteYear(id);

            var response = new ApiResponse<string>("Ningún registro eliminado.");

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
