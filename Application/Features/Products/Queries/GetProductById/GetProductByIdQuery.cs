using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery : IRequest<Response<ProductResponse>>
    {
        public Guid Id { get; init; }
    }
    internal sealed class GetProductByIdQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductByIdQuery, Response<ProductResponse>>
    {
        public async Task<Response<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await repository.GetByIdAsync(query.Id, cancellationToken);
            var data = mapper.Map<ProductResponse>(entity);
            var response = new Response<ProductResponse>(data);
            return response;
        }
    }
}
