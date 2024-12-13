using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using NotFoundException = Application.Commons.Exceptions.NotFoundException;

namespace Application.Features.Products.Queries.GetProductsWithPagination
{
    public sealed class GetProductsWithPaginationQueryHandler : IRequestHandler<GetProductsWithPaginationQuery, PaginatedResponse<ProductResponse>>
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IMapper _mapper;
        public GetProductsWithPaginationQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<PaginatedResponse<ProductResponse>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
        {
            var entities = await _repository.GetAsync(request.Pagination, cancellationToken);
            if (entities is null || entities.TotalCount < 0)
                throw new NotFoundException();

            var response = _mapper.Map<PaginatedResponse<ProductResponse>>(entities);
            return response;
        }
    }
}
