using Application.Interfaces;
using Application.Models;
using Domain.Entities;

namespace Application.Features.Categories.Commands.CreateCategory
{
    public sealed class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Response<Guid>>
    {
        private readonly IRepositoryAsync<Category> _repository;
        private readonly IMapper _mapper;
        public CreateCategoryCommandHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Response<Guid>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Category>(request);
            var data = await _repository.AddAsync(entity, cancellationToken);
            return new Response<Guid>(data);
        }
    }
}
