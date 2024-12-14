using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Commands.AuthenticateUser
{
    public record AuthenticateUserCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? IpAddress { get; set; }
    }
}
