using Application.DTOs;
using Application.ViewModels;

namespace Application.Abstractions
{
    public interface IUserService
    {
        Task<TokenViewModel> Login(LoginDTO loginModel);

        Task<bool> Register(RegistrationDTO registrationModel);

        Task<string> RefreshToken(TokenDTO tokensModel);

        Task Revoke();
    }
}
