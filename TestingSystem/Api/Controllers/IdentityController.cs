﻿using Application.Abstractions;
using Application.DTOs;
using Application.Identitity;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/testing-system/identity/")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public IdentityController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginDTO loginModel)
        {
            var tokenModel = await userService.Login(loginModel);

            if (string.IsNullOrEmpty(tokenModel.Tokens.AccessToken) || string.IsNullOrEmpty(tokenModel.Tokens.AccessToken))
            {
                return Unauthorized();
            }

            return Ok(tokenModel);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationDTO registrationModel)
        {
            var isSuccessful = await userService.Register(registrationModel);

            if (!isSuccessful)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(isSuccessful);
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh(TokenDTO tokensModel)
        {
            var newAccessToken = await userService.RefreshToken(tokensModel);

            if (string.IsNullOrEmpty(newAccessToken))
            {
                return BadRequest("Provided token are invalid");
            }

            return Ok(newAccessToken);
        }

        [HttpPost]
        [Authorize]
        [Route("revoke")]
        public async Task<IActionResult> Revoke()
        {
            await userService.Revoke();

            return NoContent();
        }
    }
}
