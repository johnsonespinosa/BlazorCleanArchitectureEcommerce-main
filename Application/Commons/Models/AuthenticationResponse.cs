using System.Text.Json.Serialization;

namespace Application.Commons.Models
{
    public class AuthenticationResponse
    {
        public string? Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public List<string>? Roles { get; set; }
        public string? JwtToken { get; set; }
        public bool IsVerified { get; set; }

        [JsonIgnore]
        public string? RefreshJwtSecurityToken { get; set; }
    }
}
