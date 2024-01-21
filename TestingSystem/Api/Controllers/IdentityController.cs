﻿using Application.Abstractions;
using Application.DTOs;
using Application.Identitity;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Api.Models;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
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

            if (string.IsNullOrEmpty(tokenModel.AccessToken) || string.IsNullOrEmpty(tokenModel.RefreshToken))
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

            return Ok(isSuccessful);
        }
    }
}
