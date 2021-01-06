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
    public class YearController : ControllerBase
    {
        private readonly IYearService _yearService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public YearController(IYearService yearService, IMapper mapper, IUriService uriService)
        {
            _yearService = yearService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Devuelve todos los años
        /// </summary>
        /// <returns>Lista de años</returns>
        [HttpGet(Name = nameof(GetYears))]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<MonthDto>>), StatusCodes.Status200OK)]
        public IActionResult GetYears([FromQuery] YearQueryFilter filters)
        {
            var years = _yearService.GetYears(filters);
            var yearsDto = _mapper.Map<IEnumerable<YearDto>>(years);

            var metadata = new Metadata
            {
                TotalCount = years.TotalCount,
                PageSize = years.PageSize,
                CurrentPage = years.CurrentPage,
                TotalPages = years.TotalPages,
                HasNextPage = years.HasNextPage,
                HasPreviousPage = years.HasPreviousPage,
                NextPageUrl = years.HasNextPage ? _uriSerice.GetPaginationUri((int)years.NextPageNumber, years.PageSize, Url.RouteUrl(nameof(GetYears))).ToString() : null,
                PreviousPageUrl = years.HasPreviousPage ? _uriSerice.GetPaginationUri((int)years.NextPageNumber, years.PageSize, Url.RouteUrl(nameof(GetYears))).ToString() : null

            };

            var response = new ApiResponse<IEnumerable<YearDto>>(yearsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el año solicitado
        /// </summary>
        /// <param name="id">Id del año</param>
        /// <returns>El año solicitado</returns>
        [HttpGet("{id}", Name = nameof(GetYear))]
        [ProducesResponseType(typeof(ApiResponse<MonthDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetYear(int id)
        {
            var year = await _yearService.GetYear(id);
            var yearDto = _mapper.Map<YearDto>(year);

            var response = new ApiResponse<YearDto>(yearDto);

            return Ok(response);
        }

        /// <summary>
        /// Inserta un nuevo año
        /// </summary>
        /// <param name="yearDto">Valor del año</param>
        /// <returns>El año insertado</returns>
        [HttpPost(Name = nameof(InsertYear))]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [ProducesResponseType(typeof(ApiResponse<MonthDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertYear(YearDto yearDto)
        {
            var year = _mapper.Map<Year>(yearDto);
            await _yearService.InsertYear(year);
            yearDto = _mapper.Map<YearDto>(year);

            var response = new ApiResponse<YearDto>(yearDto);

            return CreatedAtAction(nameof(GetYear), new { id = yearDto.Id }, new ApiResponse<YearDto>(yearDto));
        }

        /// <summary>
        /// Edita el año solicitado
        /// </summary>
        /// <param name="id">Id del año</param>
        /// <param name="yearDto">Nuevos datos del año</param>
        /// <returns>Año actualizado</returns>
        [HttpPut("{id}", Name = nameof(UpdateYearAsync))]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateYearAsync(int id, YearDto yearDto)
        {
            var year = _mapper.Map<Year>(yearDto);
            year.Id = id;
            await _yearService.UpdateYearAsync(year);

            return NoContent();
        }

        /// <summary>
        /// Elimina el año solicitado
        /// </summary>
        /// <param name="id">Id del año</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteYear))]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteYear(int id)
        {
            await _yearService.DeleteYear(id);

            return NoContent();
        }
    }
}
