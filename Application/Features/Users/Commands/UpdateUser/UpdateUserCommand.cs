using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand(string Id, CreateUserRequest Request) : IRequest<Response<string>>;

    internal sealed class UpdateUserCommandHandler(IIdentityService identityService)
        : IRequestHandler<UpdateUserCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var response = await identityService.UpdateUserAsync(command.Request, command.Id!);
            return response;
        }
    }
}
