using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Products.Queries.GetProductById
{
    public sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse>
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            var response = _mapper.Map<ProductResponse>(entity);
            return response;
        }
    }
}
