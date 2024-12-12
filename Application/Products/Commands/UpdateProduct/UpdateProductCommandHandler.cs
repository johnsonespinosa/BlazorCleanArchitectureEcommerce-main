using Application.Commons.Exceptions;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, WritingResponse>
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WritingResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                throw new NotFoundException();

            _mapper.Map(request, entity);

            var response = await _repository.UpdateAsync(entity, cancellationToken);
            return response;
        }
    }
}
