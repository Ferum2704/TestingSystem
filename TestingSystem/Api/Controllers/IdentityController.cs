using Application.Abstractions;
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
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var loginUser = mapper.Map<LoginUser>(loginModel);

            var tokenModel = await userService.Login(loginUser);

            if (string.IsNullOrEmpty(tokenModel.AccessToken) || string.IsNullOrEmpty(tokenModel.RefreshToken))
            {
                return Unauthorized();
            }

            return Ok(tokenModel);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            var registerUser = mapper.Map<RegisterUser>(model);

            var isSuccessful = await userService.Register(registerUser);

            return Ok(isSuccessful);
        }
    }
}
