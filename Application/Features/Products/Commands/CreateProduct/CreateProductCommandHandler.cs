using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Products.Commands.CreateProduct
{
    public sealed class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, WritingResponse>
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WritingResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Product>(request);
            var response = await _repository.AddAsync(entity, cancellationToken);
            return response;
        }
    }
}
