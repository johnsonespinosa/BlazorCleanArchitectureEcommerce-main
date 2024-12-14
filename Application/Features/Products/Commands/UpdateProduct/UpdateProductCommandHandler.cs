using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using NotFoundException = Application.Commons.Exceptions.NotFoundException;

namespace Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Product> _repository;
        private readonly IMapper _mapper;
        public UpdateProductCommandHandler(IRepositoryAsync<Product> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (entity is null)
                throw new NotFoundException();

            _mapper.Map(request, entity);

            var data = await _repository.UpdateAsync(entity, cancellationToken);
            return new Response<Guid>(data);
        }
    }
}
