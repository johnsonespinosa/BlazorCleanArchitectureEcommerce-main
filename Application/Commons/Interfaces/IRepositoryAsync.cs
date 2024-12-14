using Application.Models;
using Domain.Commons;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<TEntity> where TEntity : BaseAuditableEntity
    {
        Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<PaginatedResponse<TEntity>> GetAsync(PaginationRequest request, CancellationToken cancellationToken);
        Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken);
        Task<Guid> UpdateAsync(TEntity entity, CancellationToken cancelToken);
        Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
