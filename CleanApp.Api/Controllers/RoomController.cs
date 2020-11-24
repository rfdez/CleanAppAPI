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
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public RoomController(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var rooms = _roomService.GetRooms();
            var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(rooms);

            var response = new ApiResponse<IEnumerable<RoomDto>>(roomsDto);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _roomService.GetRoom(id);
            var roomDto = _mapper.Map<RoomDto>(room);

            var response = new ApiResponse<RoomDto>(roomDto);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post(RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            var inserted = await _roomService.InsertRoom(room);
            roomDto = _mapper.Map<RoomDto>(room);

            var response = new ApiResponse<string>("Ningún registro insertado.");

            if (inserted)
            {
                return Created($"{roomDto.Id}", new ApiResponse<RoomDto>(roomDto));

            }

            return BadRequest(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, RoomDto roomDto)
        {
            var room = _mapper.Map<Room>(roomDto);
            room.Id = id;

            var updated = await _roomService.UpdateRoomAsync(room);
            roomDto = _mapper.Map<RoomDto>(room);

            var response = new ApiResponse<string>("Ningún registro actualizado.");

            if (updated)
            {
                return Ok(new ApiResponse<RoomDto>(roomDto));
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _roomService.DeleteRoom(id);

            var response = new ApiResponse<string>("Ningún registro eliminado.");

            if (deleted)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
