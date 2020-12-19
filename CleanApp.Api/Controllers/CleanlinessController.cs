using AutoMapper;
using CleanApp.Core.CustomEntities;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.Interfaces.Services;
using CleanApp.Core.QueryFilters;
using CleanApp.Core.Responses;
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
    public class CleanlinessController : ControllerBase
    {
        private readonly ICleanlinessService _cleanlinessService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public CleanlinessController(ICleanlinessService cleanlinessService, IMapper mapper, IUriService uriService)
        {
            _cleanlinessService = cleanlinessService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Obtiene todos los turnos de limpieza
        /// </summary>
        /// <param name="filters">Filtrar por habitación, semana o inquilino</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetCleanlinesses))]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<CleanlinessDto>>), StatusCodes.Status200OK)]
        public IActionResult GetCleanlinesses([FromQuery] CleanlinessQueryFilter filters)
        {
            var cleanlinesses = _cleanlinessService.GetCleanlinesses(filters);
            var clenlinessesDto = _mapper.Map<IEnumerable<CleanlinessDto>>(cleanlinesses);

            var metadata = new Metadata
            {
                TotalCount = cleanlinesses.TotalCount,
                PageSize = cleanlinesses.PageSize,
                CurrentPage = cleanlinesses.CurrentPage,
                TotalPages = cleanlinesses.TotalPages,
                HasNextPage = cleanlinesses.HasNextPage,
                HasPreviousPage = cleanlinesses.HasPreviousPage,
                NextPageUrl = cleanlinesses.HasNextPage ? $"{_uriSerice.GetPaginationUri((int)cleanlinesses.NextPageNumber, cleanlinesses.PageSize, Url.RouteUrl(nameof(GetCleanlinesses)))}{_uriSerice.GetCleanlinessFilterUri(filters)}" : null,
                PreviousPageUrl = cleanlinesses.HasPreviousPage ? $"{_uriSerice.GetPaginationUri((int)cleanlinesses.PreviousPageNumber, cleanlinesses.PageSize, Url.RouteUrl(nameof(GetCleanlinesses)))}{_uriSerice.GetCleanlinessFilterUri(filters)}" : null

            };

            var response = new ApiResponse<IEnumerable<CleanlinessDto>>(clenlinessesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene el turno de limpieza
        /// </summary>
        /// <param name="roomId">Identificador de la habitación</param>
        /// <param name="weekId">Identificador de la semana</param>
        /// <returns>Turno de limpieza</returns>
        [HttpGet("room/{roomId}/week/{weekId}", Name = nameof(GetCleanliness))]
        [ProducesResponseType(typeof(ApiResponse<CleanlinessDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCleanliness(int roomId, int weekId)
        {
            var cleanliness = await _cleanlinessService.GetCleanliness(roomId, weekId);
            var cleanlinessDto = _mapper.Map<CleanlinessDto>(cleanliness);

            var response = new ApiResponse<CleanlinessDto>(cleanlinessDto);

            return Ok(response);
        }

        /// <summary>
        /// Establece un nuevo turno de limpieza
        /// </summary>
        /// <param name="cleanlinessDto">Valores del turno de limpieza</param>
        /// <returns>Turno de limpieza insertado</returns>
        [HttpPost(Name = nameof(InsertCleanliness))]
        [ProducesResponseType(typeof(ApiResponse<CleanlinessDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertCleanliness(CleanlinessDto cleanlinessDto)
        {
            var cleanliness = _mapper.Map<Cleanliness>(cleanlinessDto);
            await _cleanlinessService.InsertCleanliness(cleanliness);
            cleanlinessDto = _mapper.Map<CleanlinessDto>(cleanliness);

            var response = new ApiResponse<CleanlinessDto>(cleanlinessDto);

            return CreatedAtAction(nameof(GetCleanliness), new { roomId = cleanlinessDto.RoomId, weekId = cleanlinessDto.WeekId }, response);
        }

        /// <summary>
        /// Actualiza un turno de limpieza
        /// </summary>
        /// <param name="roomId">Identificador de la habitación</param>
        /// <param name="weekId">Identificador de la semana</param>
        /// <param name="cleanlinessDto">Nuevos valores del turno de limpieza</param>
        /// <returns></returns>
        [HttpPut("room/{roomId}/week/{weekId}", Name = nameof(UpdateCleanlinessAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateCleanlinessAsync(int roomId, int weekId, CleanlinessDto cleanlinessDto)
        {
            var cleanliness = _mapper.Map<Cleanliness>(cleanlinessDto);
            cleanliness.RoomId = roomId;
            cleanliness.WeekId = weekId;

            await _cleanlinessService.UpdateCleanlinessAsync(cleanliness);

            return NoContent();
        }

        /// <summary>
        /// Elimina un turno de limpieza
        /// </summary>
        /// <param name="roomId">Identificador de la habitación</param>
        /// <param name="weekId">Identificador de la semana</param>
        /// <returns></returns>
        [HttpDelete("room/{roomId}/week/{weekId}", Name = nameof(DeleteCleanliness))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteCleanliness(int roomId, int weekId)
        {
            await _cleanlinessService.DeleteCleanliness(roomId, weekId);

            return NoContent();
        }
    }
}
