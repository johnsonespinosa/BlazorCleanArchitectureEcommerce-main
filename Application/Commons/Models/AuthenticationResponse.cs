using System.Text.Json.Serialization;

namespace Application.Commons.Models
{
    public class AuthenticationResponse
    {
        public string? Id { get; init; }
        public string? UserName { get; init; }
        public string? Email { get; init; }
        public List<string>? Roles { get; init; }
        public string? JwtToken { get; init; }
        public bool IsVerified { get; init; }

        [JsonIgnore]
        public string? RefreshJwtSecurityToken { get; init; }
    }
}
