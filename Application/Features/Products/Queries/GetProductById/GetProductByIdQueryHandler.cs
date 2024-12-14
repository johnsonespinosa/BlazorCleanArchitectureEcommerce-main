using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Queries.GetProductById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, Response<ProductResponse>>
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            var data = _mapper.Map<ProductResponse>(entity);
            return new Response<ProductResponse>(data);
        }
    }
}
