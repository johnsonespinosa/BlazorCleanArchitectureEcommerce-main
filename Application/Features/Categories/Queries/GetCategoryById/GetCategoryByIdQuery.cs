using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<Response<CategoryResponse>>;

    internal sealed class GetCategoryByIdQueryHandler(ICategoryService service) : IRequestHandler<GetCategoryByIdQuery, Response<CategoryResponse>>
    {
        private readonly ICategoryService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            return await _service.GetCategoryByIdAsync(query.Id, cancellationToken);
        }
    }
}