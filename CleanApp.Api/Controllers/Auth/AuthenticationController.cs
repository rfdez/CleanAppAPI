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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Authorize(Roles = nameof(RoleType.Administrator))]
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
        /// Función para darle autorización a un usuario al uso de tokens
        /// </summary>
        /// <param name="authenticationDto">Datos del usuario</param>
        /// <returns></returns>
        [HttpPost(Name = nameof(RegisterUser))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<AuthenticationDto>), StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser(AuthenticationDto authenticationDto)
        {
            // a la hora de crear una authenticacion tambien se crearia un tenant
            var user = _mapper.Map<Authentication>(authenticationDto);
            user.UserPassword = _passwordService.Hash(user.UserPassword);

            await _authenticationService.RegisterUser(user);

            authenticationDto = _mapper.Map<AuthenticationDto>(user);

            var response = new ApiResponse<AuthenticationDto>(authenticationDto);

            return Created(authenticationDto.CurrentUser, response);
        }
    }
}
