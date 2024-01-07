using Application.Abstractions;
using Application.Identitity;
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

        public IdentityController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var loginUser = new LoginUser
            {
                Password = loginModel.Password,
                Username = loginModel.Username,
            };

            var tokenModel = await userService.Login(loginUser);

            if (string.IsNullOrEmpty(tokenModel.AccessToken) || string.IsNullOrEmpty(tokenModel.RefreshToken))
            {
                return Unauthorized();
            }

            return Ok(tokenModel);
        }
    }
}
