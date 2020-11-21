using System;
using System.Collections.Generic;
using System.Linq;
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
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            _jobService = jobService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var jobs = _jobService.GetJobs();
            var jobsDto = _mapper.Map<IEnumerable<JobDto>>(jobs);

            var response = new ApiResponse<IEnumerable<JobDto>>(jobsDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var job = await _jobService.GetJob(id);
            var jobDto = _mapper.Map<JobDto>(job);

            var response = new ApiResponse<JobDto>(jobDto);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(JobDto jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            var inserted = await _jobService.InsertJob(job);
            jobDto = _mapper.Map<JobDto>(job);

            var response = new ApiResponse<string>("Ningún registro insertado.");

            if (inserted)
            {
                return Created($"{jobDto.Id}", new ApiResponse<JobDto>(jobDto));

            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, JobDto jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            job.Id = id;

            var updated = await _jobService.UpdateJobAsync(job);
            jobDto = _mapper.Map<JobDto>(job);

            var response = new ApiResponse<string>("Ningún registro actualizado.");

            if (updated)
            {
                return Ok(new ApiResponse<JobDto>(jobDto));
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _jobService.DeleteJob(id);

            var response = new ApiResponse<string>("Ningún registro eliminado.");

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
