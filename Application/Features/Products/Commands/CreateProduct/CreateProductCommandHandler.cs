using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Product>(request);
            var data = await _repository.AddAsync(entity, cancellationToken);
            return new Response<Guid>(data);
        }
    }
}
