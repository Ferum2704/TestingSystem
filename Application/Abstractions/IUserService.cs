using Application.Identitity;
using Application.ViewModels;

namespace Application.Abstractions
{
    public interface IUserService
    {
        public Task<TokenViewModel> Login(LoginUser loginUser);

        public Task<bool> Register(RegisterUser registerUser);
    }
}
