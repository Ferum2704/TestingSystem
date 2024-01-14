using Application.Abstractions;
using Application.Identitity;
using Application.Utilities;
using Application.ViewModels;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IJwtProvider jwtProvider;
        private readonly IUnitOfWork unitOfWork;

        public UserService(
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IJwtProvider jwtProvider,
            IUnitOfWork unitOfWork)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwtProvider = jwtProvider;
            this.unitOfWork = unitOfWork;
        }

        public async Task<TokenViewModel> Login(LoginUser userToLogin)
        {
            userToLogin.NotNull(nameof(userToLogin));

            var tokenViewModel = new TokenViewModel
            {
                AccessToken = string.Empty,
                RefreshToken = string.Empty,
            };

            var user = await userManager.FindByNameAsync(userToLogin.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, userToLogin.Password))
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

        public async Task<bool> Register(RegisterUser userToRegister)
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

        private static ApplicationUser InitializeNewUser(RegisterUser userToRegister)
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
