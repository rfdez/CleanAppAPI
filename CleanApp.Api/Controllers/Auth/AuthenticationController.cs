using AutoMapper;
using CleanApp.Api.Responses;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Enumerations;
using CleanApp.Core.Services;
using CleanApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPassworService _passwordService;

        public AuthenticationController(IMapper mapper, IAuthenticationService authenticationService, IPassworService passworService)
        {
            _mapper = mapper;
            _authenticationService = authenticationService;
            _passwordService = passworService;
        }

        /// <summary>
        /// Función para registrar un administrador
        /// </summary>
        /// <param name="authenticationDto">Datos del usuario</param>
        /// <returns>Autenticación</returns>
        [HttpPost(Name = nameof(RegisterAdministrator))]
        [Authorize(Roles = nameof(RoleType.Administrator))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<AuthenticationDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterAdministrator(AuthenticationDto authenticationDto)
        {
            var user = _mapper.Map<Authentication>(authenticationDto);
            user.UserPassword = _passwordService.Hash(user.UserPassword);
            user.UserRole = RoleType.Administrator;

            await _authenticationService.RegisterUser(user);

            authenticationDto = _mapper.Map<AuthenticationDto>(user);
            authenticationDto.UserPassword = null;

            var response = new ApiResponse<AuthenticationDto>(authenticationDto);

            return Created(authenticationDto.CurrentUser, response);
        }

        /// <summary>
        /// Función para registrar un inquilino
        /// </summary>
        /// <param name="authenticationDto">Datos del usuario</param>
        /// <returns>Autenticación</returns>
        [HttpPost(Name = nameof(RegisterTenant))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<AuthenticationDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterTenant(AuthenticationDto authenticationDto)
        {
            var user = _mapper.Map<Authentication>(authenticationDto);
            user.UserPassword = _passwordService.Hash(user.UserPassword);
            user.UserRole = RoleType.Organizer;

            await _authenticationService.RegisterUser(user);

            authenticationDto = _mapper.Map<AuthenticationDto>(user);
            authenticationDto.UserPassword = null;

            var response = new ApiResponse<AuthenticationDto>(authenticationDto);

            return Created(authenticationDto.CurrentUser, response);
        }
    }
}
