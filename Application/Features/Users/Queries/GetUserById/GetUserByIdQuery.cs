using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Users.Queries.GetUserById
{
    public record GetUserByIdQuery(string Id) : IRequest<Response<UserResponse>>;
    internal sealed class GetUserByIdQueryHandler(IIdentityService identityService)
        : IRequestHandler<GetUserByIdQuery, Response<UserResponse>>
    {
        public async Task<Response<UserResponse>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var response = await identityService.GetUserById(query.Id!);
            return response;
        }
    }
}
