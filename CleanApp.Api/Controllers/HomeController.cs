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
    public class HomeController : ControllerBase
    {
        private readonly IHomeService _homeService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriSerice;

        public HomeController(IHomeService homeService, IMapper mapper, IUriService uriService)
        {
            _homeService = homeService;
            _mapper = mapper;
            _uriSerice = uriService;
        }

        /// <summary>
        /// Obtiene una lista de viviendas
        /// </summary>
        /// <param name="filters">Filtrar por ubicación de la vivienda o inquilinos</param>
        /// <returns>Lista de viviendas</returns>
        [HttpGet(Name = nameof(GetHomes))]
        [ProducesResponseType(typeof(ApiResponse<IEnumerable<HomeDto>>), StatusCodes.Status200OK)]
        public IActionResult GetHomes([FromQuery] HomeQueryFilter filters)
        {
            var homes = _homeService.GetHomes(filters);
            var homesDto = _mapper.Map<IEnumerable<HomeDto>>(homes);

            var metadata = new Metadata
            {
                TotalCount = homes.TotalCount,
                PageSize = homes.PageSize,
                CurrentPage = homes.CurrentPage,
                TotalPages = homes.TotalPages,
                HasNextPage = homes.HasNextPage,
                HasPreviousPage = homes.HasPreviousPage,
                NextPageUrl = _uriSerice.GetPaginationUri((int)homes.NextPageNumber, homes.PageSize, Url.RouteUrl(nameof(GetHomes))).ToString(),
                PreviousPageUrl = _uriSerice.GetPaginationUri((int)homes.NextPageNumber, homes.PageSize, Url.RouteUrl(nameof(GetHomes))).ToString()

            };

            var response = new ApiResponse<IEnumerable<HomeDto>>(homesDto)
            {
                Meta = metadata
            };

            return Ok(response);
        }

        /// <summary>
        /// Obtiene una vivienda
        /// </summary>
        /// <param name="id">Identificador de la vivienda</param>
        /// <returns>Vivienda solicitada</returns>
        [HttpGet("{id}", Name = nameof(GetHome))]
        [ProducesResponseType(typeof(ApiResponse<HomeDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetHome(int id)
        {
            var home = await _homeService.GetHome(id);
            var homeDto = _mapper.Map<HomeDto>(home);

            var response = new ApiResponse<HomeDto>(homeDto);

            return Ok(response);
        }

        /// <summary>
        /// Inserta una vivienda
        /// </summary>
        /// <param name="homeDto">Valores de la nueva vivienda</param>
        /// <returns>Vivienda insertada</returns>
        [HttpPost(Name = nameof(InsertHome))]
        [ProducesResponseType(typeof(ApiResponse<HomeDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> InsertHome(HomeDto homeDto)
        {
            var home = _mapper.Map<Home>(homeDto);
            await _homeService.InsertHome(home);
            homeDto = _mapper.Map<HomeDto>(home);

            return CreatedAtAction(nameof(GetHome), new { id = homeDto.Id }, new ApiResponse<HomeDto>(homeDto));
        }

        /// <summary>
        /// Actualiza una vivienda
        /// </summary>
        /// <param name="id">Identificador de la vivienda</param>
        /// <param name="homeDto">Nuevos valores para la vivienda</param>
        /// <returns></returns>
        [HttpPut("{id}", Name = nameof(UpdateHomeAsync))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateHomeAsync(int id, HomeDto homeDto)
        {
            var home = _mapper.Map<Home>(homeDto);
            home.Id = id;

            await _homeService.UpdateHomeAsync(home);

            return NoContent();
        }

        /// <summary>
        /// Elimina una vivienda
        /// </summary>
        /// <param name="id">Identificador de la vivienda</param>
        /// <returns></returns>
        [HttpDelete("{id}", Name = nameof(DeleteHome))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteHome(int id)
        {
            await _homeService.DeleteHome(id);

            return NoContent();
        }
    }
}
