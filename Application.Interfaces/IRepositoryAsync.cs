using Application.Models;
using Domain.Commons;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<TEntity> where TEntity : BaseAuditableEntity
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PaginatedResponse<TEntity>> GetAsync(FilterRequest request, CancellationToken cancellationToken);
        Task<WritingResponse> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<WritingResponse> UpdateAsync(TEntity entity, CancellationToken cancelToken);
        Task<WritingResponse> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
