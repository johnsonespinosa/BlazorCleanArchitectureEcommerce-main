using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Models;
using MediatR;

namespace Application.UseCases.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, WritingResponse>
    {
        private readonly IRepositoryAsync<Category> _repository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<WritingResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Category>(request);
            var response = await _repository.AddAsync(entity, cancellationToken);
            return response;
        }
    }
}
