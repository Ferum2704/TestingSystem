using Application.DTOs;

namespace Application.ViewModels
{
    public class TokenViewModel
    {
        public TokenDTO Tokens { get; set; }

        public Guid? DomainUserId { get; set; }
    }
}
