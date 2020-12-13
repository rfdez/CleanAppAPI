using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.CustomEntities;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities;
using CleanApp.Core.QueryFilters;
using CleanApp.Core.Services;
using CleanApp.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    //[Authorize(Roles = nameof(RoleType.Organizer))]
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public RoomController(IRoomService roomService, IMapper mapper, IUriService uriService)
        {
            _roomService = roomService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Obtiene una lista de habitaciones
        /// </summary>
        /// <param name="filters">Filtrar por nombre</param>
        /// <returns>Lista de habitaciones</returns>
        [HttpGet(Name = nameof(GetRooms))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<RoomDto>>), StatusCodes.Status200OK)]
        public IActionResult GetRooms([FromQuery] RoomQueryFilter filters)
        {
            var rooms = _roomService.GetRooms(filters);
            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            var metadata = new Metadata
            {
                TotalCount = rooms.TotalCount,
                PageSize = rooms.PageSize,
                CurrentPage = rooms.CurrentPage,
                TotalPages = rooms.TotalPages,
                HasNextPage = rooms.HasNextPage,
                HasPreviousPage = rooms.HasPreviousPage,
                NextPageUrl = _uriSerice.GetRoomPaginationUri(filters, Url.RouteUrl(nameof(GetRooms))).ToString(),
                PreviousPageUrl = _uriSerice.GetRoomPaginationUri(filters, Url.RouteUrl(nameof(GetRooms))).ToString()

            };

            var response = new ApiResponse<IEnumerable<RoomDto>>(roomsDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene la habitación solicitada
        /// </summary>
        /// <param name="id">Identificador de la habitación</param>
        /// <returns>Una habitación</returns>
        [HttpGet("{id}", Name = nameof(GetRoom))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<RoomDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRoom(int id)
        {
            var room = await _roomService.GetRoom(id);
            var roomDto = _mapper.Map<RoomDto>(room);

            var response = new ApiResponse<RoomDto>(roomDto);

            return Ok(response);
        }

        /// <summary>
        /// Inserta un habitación
        /// </summary>
        /// <param name="roomDto">Valores de la habitación</param>
        /// <returns>Habitación insertada</returns>
        [HttpPost(Name = nameof(InsertRoom))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<RoomDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertRoom(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            await _roomService.InsertRoom(room);
            roomDto = _mapper.Map<RoomDto>(room);

            return CreatedAtAction(nameof(GetRoom), new { id = roomDto.Id }, new ApiResponse<RoomDto>(roomDto));
        }

        /// <summary>
        /// Actualiza una habitación
        /// </summary>
        /// <param name="id">Identificador de la habitación</param>
        /// <param name="roomDto">Nuevos valores de la habitación</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateRoomAsync))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRoomAsync(int id, RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            room.Id = id;

            await _roomService.UpdateRoomAsync(room);

            return NoContent();
        }

        /// <summary>
        /// Elimina una habitación
        /// </summary>
        /// <param name="id">Identificador de la habitación</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteRoom))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            await _roomService.DeleteRoom(id);

            return NoContent();
        }
    }
}
