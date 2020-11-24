using System.Collections.Generic;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CleanApp.Api.Controllers
{
    //[Authorize]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
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

        /// <summary>
        /// Devuelve todos los años
        /// </summary>
        /// <returns>Lista de años</returns>
        [HttpGet(Name = nameof(GetYears))]

        [ProducesDefaultResponseType]
        public IActionResult GetYears()
        {
            var years = _yearService.GetYears();
            var yearsDto = _mapper.Map<IEnumerable<YearDto>>(years);

            var response = new ApiResponse<IEnumerable<YearDto>>(yearsDto);

            return Ok(response);
        }
        
        /// <summary>
        /// Obtiene el año solicitado
        /// </summary>
        /// <param name="id">Id del año</param>
        /// <returns>El año solicitado</returns>
        [HttpGet("{id}", Name = nameof(Get) + "[controller]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<YearDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<System.Exception>))]
        public async Task<IActionResult> Get(int id)
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
        [HttpPost(Name = nameof(Post) + "[controller]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<YearDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
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

        /// <summary>
        /// Edita el año solicitado
        /// </summary>
        /// <param name="id">Id del año</param>
        /// <param name="yearDto">Nuevos datos del año</param>
        /// <returns>Año actualizado</returns>
        [HttpPut("{id}", Name = nameof(Put) + "[controller]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<YearDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
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

        /// <summary>
        /// Elimina el año solicitado
        /// </summary>
        /// <param name="id">Id del año</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(Delete) + "[controller]")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<string>))]
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
