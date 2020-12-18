using AutoMapper;
using CleanApp.Core.DTOs;
using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Responses;
using CleanApp.Core.Services.Auth;
using CleanApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CleanApp.Api.Auth.Controllers
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
        /// Registra un usuario en la aplicación
        /// </summary>
        /// <param name="authenticationDto">Datos del usuario</param>
        /// <returns>Autenticación</returns>
        [HttpPost(Name = nameof(RegisterUser))]
        [ProducesResponseType(typeof(ApiResponse<AuthenticationDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(AuthenticationDto authenticationDto)
        {
            var user = _mapper.Map<Authentication>(authenticationDto);
            user.UserPassword = _passwordService.Hash(user.UserPassword);
            string currentUserRole = User.FindFirstValue(ClaimTypes.Role);

            await _authenticationService.RegisterUser(user, currentUserRole);

            authenticationDto = _mapper.Map<AuthenticationDto>(user);
            authenticationDto.UserPassword = null;

            var response = new ApiResponse<AuthenticationDto>(authenticationDto);

            return Created(authenticationDto.UserLogin, response);
        }
    }
}
