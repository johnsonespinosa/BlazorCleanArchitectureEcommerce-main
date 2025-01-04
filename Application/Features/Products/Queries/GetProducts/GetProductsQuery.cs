using Application.Commons.Models;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProducts
{
    public record GetProductsQuery : IRequest<Response<IEnumerable<ProductResponse>>> { }

    internal sealed class GetProductsQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        : IRequestHandler<GetProductsQuery, Response<IEnumerable<ProductResponse>>>
    {
        private readonly IRepositoryAsync<Product> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<IEnumerable<ProductResponse>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            // Create specification to get products
            var specification = new GetProductsSpecificationQuery();
            
            // Get the list of products from the repository
            var products = await _repository.ListAsync(specification, cancellationToken);
            
            // Map entities to DTOs
            var data = _mapper.Map<IEnumerable<ProductResponse>>(products);
            
            // Create response with the data obtained
            var response = new Response<IEnumerable<ProductResponse>>(data);
            return response;
        }
    }
}