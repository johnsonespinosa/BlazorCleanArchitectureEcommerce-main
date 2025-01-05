using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Users.Commands.DeleteUser
{
    public record DeleteUserCommand : IRequest<Response<string>>
    {
        public string? Id { get; init; }
    }

    internal sealed class DeleteUSerCommandHandler(IIdentityService identityService)
        : IRequestHandler<DeleteUserCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var response = await identityService.DeleteUserAsync(command.Id!);
            return response;
        }
    }
}
