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
    [Authorize(Roles = nameof(RoleType.Organizer))]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService _jobService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public JobController(IJobService jobService, IMapper mapper, IUriService uriService)
        {
            _jobService = jobService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Obtiene la lista de tareas de una habitación
        /// </summary>
        /// <param name="filters">Filtrar por nombre o descripción de la tarea y su habitación</param>
        /// <returns>Lista de tareas de una habitación</returns>
        [HttpGet(Name = nameof(GetJobs))]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<JobDto>>), StatusCodes.Status200OK)]
        public IActionResult GetJobs([FromQuery] JobQueryFilter filters)
        {
            var jobs = _jobService.GetJobs(filters);
            var jobsDto = _mapper.Map<IEnumerable<JobDto>>(jobs);

            var metadata = new Metadata
            {
                TotalCount = jobs.TotalCount,
                PageSize = jobs.PageSize,
                CurrentPage = jobs.CurrentPage,
                TotalPages = jobs.TotalPages,
                HasNextPage = jobs.HasNextPage,
                HasPreviousPage = jobs.HasPreviousPage,
                NextPageUrl = _uriSerice.GetJobPaginationUri(filters, Url.RouteUrl(nameof(GetJobs))).ToString(),
                PreviousPageUrl = _uriSerice.GetJobPaginationUri(filters, Url.RouteUrl(nameof(GetJobs))).ToString()

            };

            var response = new ApiResponse<IEnumerable<JobDto>>(jobsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una tarea
        /// </summary>
        /// <param name="id">Identificador de la tarea</param>
        /// <returns>Una tarea</returns>
        [HttpGet("{id}", Name = nameof(GetJob))]
        [ProducesResponseType(typeof(ApiResponse<JobDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetJob(int id)
        {
            var job = await _jobService.GetJob(id);
            var jobDto = _mapper.Map<JobDto>(job);

            var response = new ApiResponse<JobDto>(jobDto);

            return Ok(response);
        }

        /// <summary>
        /// Inserta una tarea a una habitación
        /// </summary>
        /// <param name="jobDto">Valores de la tarea</param>
        /// <returns>Tarea insertada</returns>
        [HttpPost(Name = nameof(InsertJob))]
        [ProducesResponseType(typeof(ApiResponse<JobDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertJob(JobDto jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            await _jobService.InsertJob(job);
            jobDto = _mapper.Map<JobDto>(job);

            return CreatedAtAction(nameof(GetJob), new { id = jobDto.Id }, new ApiResponse<JobDto>(jobDto));
        }

        /// <summary>
        /// Actualiza la tarea de una habitación
        /// </summary>
        /// <param name="id">Identificador de la tarea</param>
        /// <param name="jobDto">Nuevos valores de la tarea</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateJobAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateJobAsync(int id, JobDto jobDto)
        {
            var job = _mapper.Map<Job>(jobDto);
            job.Id = id;

            await _jobService.UpdateJobAsync(job);

            return NoContent();
        }

        /// <summary>
        /// Elimina una tarea de la habitación
        /// </summary>
        /// <param name="id">Identificador de la tarea</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteJob))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteJob(int id)
        {
            await _jobService.DeleteJob(id);

            return NoContent();
        }
    }
}
