using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Commands.AuthenticateUser
{
    public sealed class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, Response<AuthenticationResponse>>
    {
        public Task<Response<AuthenticationResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
