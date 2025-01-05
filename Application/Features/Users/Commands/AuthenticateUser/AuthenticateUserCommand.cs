using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Users.Commands.AuthenticateUser
{
    public record AuthenticateUserCommand(string Email, string Password, string IpAddress)
        : IRequest<Response<AuthenticationResponse>>;
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
