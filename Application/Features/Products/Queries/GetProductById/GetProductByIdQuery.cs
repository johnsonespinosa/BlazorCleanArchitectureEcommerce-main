using Application.Commons.Interfaces;
using Application.Commons.Models;

namespace Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Response<ProductResponse>>;

    internal sealed class GetProductByIdQueryHandler(IProductService service) : IRequestHandler<GetProductByIdQuery, Response<ProductResponse>>
    {
        private readonly IProductService _service = service ?? throw new ArgumentNullException(nameof(service));

        public async Task<Response<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            return await _service.GetProductByIdAsync(query.Id, cancellationToken);
        }
    }
}