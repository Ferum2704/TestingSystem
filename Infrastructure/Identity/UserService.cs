using Application.Abstractions;
using Application.DTOs;
using Application.Identitity;
using Application.Utilities;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtProvider jwtProvider;
        private readonly IUnitOfWork unitOfWork;
        private readonly ICurrentUserService currentUserService;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtProvider jwtProvider,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtProvider = jwtProvider;
            this.unitOfWork = unitOfWork;
            this.currentUserService = currentUserService;
        }

        public async Task<TokenViewModel> Login(LoginDTO userToLogin)
        {
            userToLogin.NotNull(nameof(userToLogin));

            var tokenViewModel = new TokenViewModel
            {
                Tokens = new TokenDTO
                {
                    AccessToken = string.Empty,
                    RefreshToken = string.Empty,
                },
            };

            var user = await userManager.FindByNameAsync(userToLogin.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, userToLogin.Password))
            {
                var userRole = (await userManager.GetRolesAsync(user)).FirstOrDefault();

                if (userRole != null)
                {
                    tokenViewModel.Tokens.AccessToken = jwtProvider.GetAccessToken(user, userRole);
                    var (refreshToken, refreshTokenValidDays) = jwtProvider.GetRefreshToken();

                    tokenViewModel.Tokens.RefreshToken = refreshToken;
                    user.RefreshToken = refreshToken;
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidDays);
                    await userManager.UpdateAsync(user);
                }

                if (user.DomainUser is not null)
                {
                    tokenViewModel.DomainUserId = user.DomainUser.Id;
                }
            }

            return tokenViewModel;
        }

        public async Task<bool> Register(RegistrationDTO userToRegister)
        {
            userToRegister.NotNull(nameof(userToRegister));

            var userExists = await userManager.FindByNameAsync(userToRegister.Username);
            if (userExists != null)
            {
                return false;
            }

            var newUser = InitializeNewUser(userToRegister);

            var result = await userManager.CreateAsync(newUser, userToRegister.Password);
            if (!result.Succeeded)
            {
                return false;
            }

            if (await roleManager.RoleExistsAsync(userToRegister.Role.ToString()))
            {
                result = await userManager.AddToRoleAsync(newUser, userToRegister.Role.ToString());

                if (!result.Succeeded)
                {
                    return false;
                }

                if (newUser.DomainUser is Teacher teacher)
                {
                    unitOfWork.TeacherRepository.Add(teacher);
                }
                else if (newUser.DomainUser is Student student)
                {
                    unitOfWork.StudentRepository.Add(student);
                }
            }

            return true;
        }

        public async Task<string> RefreshToken(TokenDTO tokensModel)
        {
            tokensModel.NotNull(nameof(tokensModel));

            var principal = jwtProvider.GetPrincipalFromExpiredToken(tokensModel.AccessToken);

            if (principal is null)
            {
                return string.Empty;
            }

            var user = await userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null || user.RefreshToken != tokensModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return string.Empty;
            }

            var newAccessToken = jwtProvider.GetAccessToken(user, principal.FindFirst(ClaimTypes.Role).Value);

            return newAccessToken;
        }

        public async Task Revoke()
        {
            var user = await userManager.FindByNameAsync(currentUserService.CurrentUserUserName);

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            await userManager.UpdateAsync(user);
        }

        private static ApplicationUser InitializeNewUser(RegistrationDTO userToRegister)
        {
            var newUser = new ApplicationUser
            {
                UserName = userToRegister.Username,
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                DomainUser = userToRegister.Role switch
                {
                    ApplicationUserRole.Teacher => new Teacher(),
                    ApplicationUserRole.Student => new Student(),
                    _ => null
                },
            };

            if (newUser.DomainUser is not null && userToRegister.FirstName is not null && userToRegister.LastName is not null)
            {
                newUser.DomainUser.Id = Guid.NewGuid();
                newUser.DomainUser.Name = userToRegister.FirstName;
                newUser.DomainUser.Surname = userToRegister.LastName;
            }

            return newUser;
        }
    }
}
