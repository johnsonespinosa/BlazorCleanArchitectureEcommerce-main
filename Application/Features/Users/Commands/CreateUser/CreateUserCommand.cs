using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(CreateUserRequest Request, string Origin) : IRequest<Response<string>>;
    internal sealed class CreateUserCommandHandler(IIdentityService identityService)
        : IRequestHandler<CreateUserCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var response = await identityService.CreateUserAsync(command.Request, command.Origin!);
            return response;
        }
    }
}
