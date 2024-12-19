using Application.Commons.Interfaces;
using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Commands.AuthenticateUser
{
    public record AuthenticateUserCommand : IRequest<Response<AuthenticationResponse>>
    {
        public string? Email { get; init; }
        public string? Password { get; init; }
        public string? IpAddress { get; init; }
    }
    internal sealed class AuthenticateUserCommandHandler(IIdentityService identityService)
        : IRequestHandler<AuthenticateUserCommand, Response<AuthenticationResponse>>
    {
        public async Task<Response<AuthenticationResponse>> Handle(AuthenticateUserCommand command, CancellationToken cancellationToken)
        {
            var response = await identityService.AuthenticateAsync(new AuthenticationRequest
            {
                Email = command.Email,
                Password = command.Password,
            }, command.IpAddress!);
            return response;
        }
    }
}
