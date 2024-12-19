using Application.Commons.Interfaces;
using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand : IRequest<Response<string>>
    {
        public string? Email { get; init; }
        public string? UserName { get; init; }
        public string? PhoneNumber { get; init; }
        public string? Password { get; init; }
        public string? ConfirmPassword { get; init; }
        public string? Origin { get; init; }
    }
    internal sealed class CreateUserCommandHandler(IIdentityService identityService)
        : IRequestHandler<CreateUserCommand, Response<string>>
    {
        public async Task<Response<string>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            var request = new CreateUserRequest()
            {
                ConfirmPassword = command.ConfirmPassword,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber,
                Password = command.Password,
                UserName = command.UserName
            };
            var response = await identityService.CreateUserAsync(request, command.Origin!);
            return response;
        }
    }
}
