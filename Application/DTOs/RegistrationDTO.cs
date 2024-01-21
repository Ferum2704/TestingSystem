using Application.Identitity;

namespace Application.DTOs
{
    public class RegistrationDTO
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ApplicationUserRole Role { get; set; }
    }
}
