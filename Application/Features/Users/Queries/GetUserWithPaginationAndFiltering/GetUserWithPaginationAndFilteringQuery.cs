using Application.Commons.Interfaces;
using Application.Commons.Models;
using Application.Models;

namespace Application.Features.Users.Queries.GetUserWithPaginationAndFiltering
{
    public record GetUserWithPaginationAndFilteringQuery : IRequest<PaginatedResponse<UserResponse>>
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public string? Filter { get; init; }
    }

    internal sealed class GetUserWithPaginationAndFilteringQueryHandler : IRequestHandler<GetUserWithPaginationAndFilteringQuery, PaginatedResponse<UserResponse>>
    {
        private readonly IIdentityService _identityService;

        public GetUserWithPaginationAndFilteringQueryHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public Task<PaginatedResponse<UserResponse>> Handle(GetUserWithPaginationAndFilteringQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
