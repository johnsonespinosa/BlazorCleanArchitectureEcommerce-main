using Application.Models;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<Response<CategoryResponse>>;
}
