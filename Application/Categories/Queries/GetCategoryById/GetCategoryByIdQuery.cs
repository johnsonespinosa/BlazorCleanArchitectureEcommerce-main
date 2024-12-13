using Application.Models;
using MediatR;

namespace Application.UseCases.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryResponse>;
}
