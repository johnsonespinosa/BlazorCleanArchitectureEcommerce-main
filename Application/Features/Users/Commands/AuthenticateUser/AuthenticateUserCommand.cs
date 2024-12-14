using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Commands.AuthenticateUser
{
    public record AuthenticateUserCommand(AuthenticationRequest Authentication) : IRequest<Response<AuthenticationResponse>>;
}
