using Application.Models;
using MediatR;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryResponse>;
}
