namespace Application.ViewModels
{
    public class TokenViewModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public Guid? DomainUserId { get; set; }
    }
}
