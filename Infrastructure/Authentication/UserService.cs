using Application.Abstractions;
using Application.Identitity;
using Application.ViewModels;
using Azure;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtProvider jwtProvider;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtProvider jwtProvider)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtProvider = jwtProvider;
        }

        public async Task<TokenViewModel> Login(LoginUser loginUser)
        {
            var tokenViewModel = new TokenViewModel
            {
                AccessToken = string.Empty,
                RefreshToken = string.Empty,
            };

            var user = await userManager.FindByNameAsync(loginUser.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, loginUser.Password))
            {
                var userRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();

                if (userRole != null)
                {
                    tokenViewModel.AccessToken = jwtProvider.GetAccessToken(user, userRole);
                    tokenViewModel.RefreshToken = jwtProvider.GetRefreshToken();
                }
            }

            return tokenViewModel;
        }

        public async Task<bool> Register(RegisterUser registerUser)
        {
            var userExists = await userManager.FindByNameAsync(registerUser.Username);
            if (userExists != null)
            {
                return false;
            }

            var newUser = new ApplicationUser();
            newUser.UserName = registerUser.Username;
            newUser.ConcurrencyStamp = Guid.NewGuid().ToString();

            var result = await userManager.CreateAsync(newUser, registerUser.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            if (await roleManager.RoleExistsAsync(registerUser.Role.ToString()))
            {
                result = await userManager.AddToRoleAsync(newUser, registerUser.Role.ToString());

                if (!result.Succeeded)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
