using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string id) : IRequest<Response<UserResponse>>;
}
