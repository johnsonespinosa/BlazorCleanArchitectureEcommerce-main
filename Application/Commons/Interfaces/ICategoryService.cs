using Application.Commons.Models;
using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.UpdateCategory;

namespace Application.Commons.Interfaces;

public interface ICategoryService
{
    Task<Response<Guid>> CreateCategoryAsync(CreateCategoryCommand command, CancellationToken cancellationToken);
    Task<Response<CategoryResponse>> GetCategoryByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PaginatedResponse<CategoryResponse>> GetAllCategoriesWithPaginationAsync(FilterRequest filter, CancellationToken cancellationToken);
    Task<Response<IEnumerable<CategoryResponse>>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    Task<Response<Guid>> UpdateCategoryAsync(UpdateCategoryCommand command, CancellationToken cancellationToken);
    Task<Response<Guid>> DeleteCategoryAsync(Guid id, CancellationToken cancellationToken);
}
