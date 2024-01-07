using Application.Identitity;
using Infrastructure.Authentication;

namespace Presentation.Api.Models
{
    public class LoginModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
