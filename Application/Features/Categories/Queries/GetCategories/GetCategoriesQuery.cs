using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Categories.Queries.GetCategories
{
    public record GetCategoriesQuery : IRequest<Response<IEnumerable<CategoryResponse>>> { }

    internal sealed class GetCategoriesQueryHandler(ICategoryService service) : IRequestHandler<GetCategoriesQuery, Response<IEnumerable<CategoryResponse>>>
    {
        private readonly ICategoryService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<IEnumerable<CategoryResponse>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            return await _service.GetAllCategoriesAsync(cancellationToken);
        }
    }
}