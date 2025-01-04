using Application.Commons.Models;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProductById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Response<ProductResponse>>;

    internal sealed class GetProductByIdQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductByIdQuery, Response<ProductResponse>>
    {
        private readonly IRepositoryAsync<Product> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<ProductResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            // Get the product by ID
            var product = await _repository.GetByIdAsync(query.Id, cancellationToken);

            // Check if the product exists
            if (product is null)
                throw new NotFoundException(key: query.Id.ToString(), nameof(Product)); 

            // Map the entity to ProductResponse
            var data = _mapper.Map<ProductResponse>(product);
            
            // Create response with the data obtained
            var response = new Response<ProductResponse>(data);
            return response;
        }
    }
}