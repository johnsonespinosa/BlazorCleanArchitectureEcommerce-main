using Application.Commons.Models;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Features.Categories.Queries.GetCategories
{
    /// <summary>
    /// Query to get all categories.
    /// </summary>
    public record GetCategoriesQuery : IRequest<Response<IEnumerable<CategoryResponse>>> { }

    internal sealed class GetCategoriesQueryHandler(IRepositoryAsync<Category> repository, IMapper mapper)
        : IRequestHandler<GetCategoriesQuery, Response<IEnumerable<CategoryResponse>>>
    {
        private readonly IRepositoryAsync<Category> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<Response<IEnumerable<CategoryResponse>>> Handle(GetCategoriesQuery query, CancellationToken cancellationToken)
        {
            // Create specification to get categories
            var specification = new GetCategoriesSpecificationQuery();
            
            // Get the list of categories from the repository
            var categories = await _repository.ListAsync(specification, cancellationToken);

            if (categories.Count == 0)
                return new Response<IEnumerable<CategoryResponse>>(message: ResponseMessages.EntityNotFound);
            
            // Map entities to DTOs
            var data = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
            
            // Create response with the data obtained
            var response = new Response<IEnumerable<CategoryResponse>>(data: data, message: ResponseMessages.RecordsRetrievedSuccessfully);
            return response;
        }
    }
}