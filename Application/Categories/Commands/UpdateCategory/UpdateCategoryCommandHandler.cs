using Application.Commons.Exceptions;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Categories.Commands.UpdateCategory
{
    public sealed class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, WritingResponse>
    {
        private readonly IRepositoryAsync<Category> _repository;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WritingResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
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
