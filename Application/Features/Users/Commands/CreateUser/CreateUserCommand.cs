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
}
