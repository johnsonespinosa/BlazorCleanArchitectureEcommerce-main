using Application.Interfaces;
using Application.Models;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Categories.Commands.DeleteCategory
{
    public sealed class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, WritingResponse>
    {
        private readonly IRepositoryAsync<Category> _repository;

        public DeleteCategoryCommandHandler(IRepositoryAsync<Category> repository)
        {
            _repository = repository;
        }

        public async Task<WritingResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = await _repository.DeleteAsync(request.Id, cancellationToken);
            return response;
        }
    }
}
