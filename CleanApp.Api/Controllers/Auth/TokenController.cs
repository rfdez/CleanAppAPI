﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CleanApp.Api.Responses;
using CleanApp.Core.Entities;
using CleanApp.Core.Entities.Auth;
using CleanApp.Core.Services;
using CleanApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanApp.Api.Controllers
{
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationService _authenticationService;
        private readonly IPassworService _passwordService;
        public TokenController(IConfiguration configuration, IAuthenticationService authenticationService, IPassworService passworService)
        {
            _configuration = configuration;
            _authenticationService = authenticationService;
            _passwordService = passworService;
        }

        /// <summary>
        /// Obtener un token mediante credenciales
        /// </summary>
        /// <param name="login">Usuario y contraseña</param>
        /// <returns>Token</returns>
        [HttpPost(Name = nameof(GenerateToken))]
        [ProducesDefaultResponseType]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GenerateToken(UserLogin login)
        {
            var userValid = await ValidateUser(login);

            var token = GenerateToken(userValid);

            var response = new ApiResponse<object>(new
            {
                Token = token
            });

            return Ok(response);
        }

        #region Private methods

        private async Task<Authentication> ValidateUser(UserLogin login)
        {
            var user = await _authenticationService.GetLoginByCredentials(login);

            _passwordService.Check(user.UserPassword, login.Password);

            return user;
        }

        private string GenerateToken(Authentication authentication)
        {
            //Header
            var _symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretKey"]));
            var signingCredentials = new SigningCredentials(_symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, authentication.CurrentUser),
                new Claim(ClaimTypes.Name, authentication.UserName),
                new Claim(ClaimTypes.Role, authentication.UserRole.ToString()),
            };

            //Payload
            var payload = new JwtPayload
                (
                    _configuration["Authentication:Issuer"],
                    _configuration["Authentication:Audience"],
                    claims,
                    DateTime.Now,
                    DateTime.Now.AddMinutes(_configuration.GetValue<double>("TokenLife"))
                );

            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
