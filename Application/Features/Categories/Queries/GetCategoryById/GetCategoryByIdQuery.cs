using Application.Commons.Models;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<Response<CategoryResponse>>;

    internal sealed class GetCategoryByIdQueryHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        : IRequestHandler<GetCategoryByIdQuery, Response<CategoryResponse>>
    {
        private readonly IRepositoryAsync<Category> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<CategoryResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            // Get the category by ID
            var category = await _repository.GetByIdAsync(query.Id, cancellationToken);

            // Check if the category exists
            if (category is null)
                return new Response<CategoryResponse>(message: ResponseMessages.EntityNotFound);

            // Map the entity to CategoryResponse
            var data = _mapper.Map<CategoryResponse>(category);
            
            // Create response with the data obtained
            var response = new Response<CategoryResponse>(data: data, message: ResponseMessages.RecordsRetrievedSuccessfully);
            return response;
        }
    }
}