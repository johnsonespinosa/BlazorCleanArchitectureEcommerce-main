using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery : IRequest<Response<CategoryResponse>>
    {
        public Guid Id { get; init; }
    }
    internal sealed class GetCategoryByIdQueryHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        : IRequestHandler<GetCategoryByIdQuery, Response<CategoryResponse>>
    {
        public async Task<Response<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(query.Id, cancellationToken);
            var data = mapper.Map<CategoryResponse>(entity);
            var response = new Response<CategoryResponse>(data);
            return response;
        }
    }
}
