using Application.Commons.Interfaces;
using Application.Commons.Models;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Queries.GetCategories;
using Application.Features.Categories.Queries.GetCategoriesWithPaginationAndFiltering;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Commons.Services;

public class CategoryService(IRepositoryAsync<Category> repository, IMapper mapper) : ICategoryService
{
    private readonly IRepositoryAsync<Category> _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<Response<Guid>> CreateCategoryAsync(CreateCategoryCommand command, CancellationToken cancellationToken)
    {
        // Mapping command to entity
        var entity = _mapper.Map<Category>(command);

        // Add the category via the repository
        var category = await _repository.AddAsync(entity, cancellationToken);

        // Create response with the ID of the new category
        var response = new Response<Guid>(data: category.Id, message: ResponseMessages.EntityCreated);
        return response;
    }

    public async Task<Response<CategoryResponse>> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // Get the category by ID
        var category = await _repository.GetByIdAsync(id, cancellationToken);

        // Check if the category exists
        if (category is null)
            return new Response<CategoryResponse>(message: ResponseMessages.EntityNotFound);

        // Map the entity to CategoryResponse
        var data = _mapper.Map<CategoryResponse>(category);
            
        // Create response with the data obtained
        var response = new Response<CategoryResponse>(data: data, message: ResponseMessages.RecordsRetrievedSuccessfully);
        return response;
    }

    public async Task<PaginatedResponse<CategoryResponse>> GetAllCategoriesWithPaginationAsync(FilterRequest filter, CancellationToken cancellationToken)
    {
        // Create specification to get categories with pagination and filtering
        var specification = new GetCategoriesWithPaginationAndFilteringSpecificationQuery(filter.PageNumber, filter.PageSize, filter.Text);
    
        // Get the list of categories
        var categories = await _repository.ListAsync(specification, cancellationToken);
            
        // Count the total number of categories (for pagination)
        var totalCount = await _repository.CountAsync(specification, cancellationToken);
    
        // Map categories to CategoryResponse
        var mappedCategories = _mapper.Map<IEnumerable<CategoryResponse>>(categories);
    
        // Create and return the paginated response
        return new PaginatedResponse<CategoryResponse>(items: mappedCategories.ToList(), totalCount, filter.PageNumber, filter.PageSize);
    }

    public async Task<Response<IEnumerable<CategoryResponse>>> GetAllCategoriesAsync(CancellationToken cancellationToken)
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

    public async Task<Response<Guid>> UpdateCategoryAsync(UpdateCategoryCommand command, CancellationToken cancellationToken)
    {
        // Get the existing category
        var category = await _repository.GetByIdAsync(command.Id, cancellationToken);

        // Check if the category exists
        if (category is null)
            return new Response<Guid>(message: ResponseMessages.EntityNotFound);

        // Map the changes from the command to the existing entity
        _mapper.Map(command, category);

        // Update the category via the repository
        await _repository.UpdateAsync(category, cancellationToken);

        // Create response with the updated category ID
        var response = new Response<Guid>(data: command.Id, message: ResponseMessages.EntityUpdated);
        return response;
    }

    public async Task<Response<Guid>> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken)
    {
        // Get the category by ID
        var category = await _repository.GetByIdAsync(id, cancellationToken);

        // Check if the category exists
        if (category is null)
            return new Response<Guid>(message: ResponseMessages.EntityNotFound);

        // Delete the category via the repository
        await _repository.DeleteAsync(category, cancellationToken);

        // Create response with the ID of the deleted category
        var response = new Response<Guid>(data: id, message: ResponseMessages.EntityDeleted);
        return response;
    }
}
