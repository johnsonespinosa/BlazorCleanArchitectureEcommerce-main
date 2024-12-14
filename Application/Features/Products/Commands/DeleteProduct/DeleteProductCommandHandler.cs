using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Product> _repository;

        public DeleteProductCommandHandler(IRepositoryAsync<Product> repository)
        {
            _repository = repository;
        }

        public async Task<Response<Guid>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var data = await _repository.DeleteAsync(request.Id, cancellationToken);
            return new Response<Guid>(data);
        }
    }
}
