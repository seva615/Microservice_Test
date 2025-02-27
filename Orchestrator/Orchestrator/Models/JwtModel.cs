namespace Orchestrator.API.Models
{
    public class JwtModel
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string AccessToken { get; set; }
    }
}
