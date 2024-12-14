using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Category> _repository;

        public DeleteCategoryCommandHandler(IRepositoryAsync<Category> repository)
        {
            _repository = repository;
        }

        public async Task<Response<Guid>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var data = await _repository.DeleteAsync(request.Id, cancellationToken);
            return new Response<Guid>(data);
        }
    }
}
