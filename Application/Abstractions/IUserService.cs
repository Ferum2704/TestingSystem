using Application.DTOs;
using Application.ViewModels;

namespace Application.Abstractions
{
    public interface IUserService
    {
        public Task<TokenViewModel> Login(LoginDTO loginModel);

        public Task<bool> Register(RegistrationDTO registrationModel);

        public Task<string> RefreshToken(TokenDTO tokensModel);
    }
}
