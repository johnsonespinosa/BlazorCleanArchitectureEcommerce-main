using Application.Commons.Interfaces;
using Application.Commons.Models;
using Application.Features.Users.Commands.CreateUser;

namespace Application.Features.Users.Commands.UpdateUser
{
    public record UpdateUserCommand : CreateUserCommand
    {
        public string? Id { get; init; }
    }

    internal sealed class UpdateUserCommandHandler(IIdentityService identityService)
        : IRequestHandler<UpdateUserCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var request = new CreateUserRequest()
            {
                ConfirmPassword = command.ConfirmPassword,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Password = command.Password,
                UserName = command.UserName
            };
            var response = await identityService.UpdateUserAsync(request, command.Id!);
            return response;
        }
    }
}
