using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Commands.CreateUser
{
    public record CreateUserCommand(UserRequest User) : IRequest<Response<string>>;
}
