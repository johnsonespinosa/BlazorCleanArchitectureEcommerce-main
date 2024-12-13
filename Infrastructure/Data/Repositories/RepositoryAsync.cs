using Application.Interfaces;
using Application.Models;
using Domain.Commons;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : BaseAuditableEntity
    {
        private readonly IApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryAsync(IApplicationDbContext context, DbSet<TEntity> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

        public async Task<WritingResponse> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            var result = await SaveChangesAsync(cancellationToken);
            return result ? WritingResponse.Success() : WritingResponse.Failure(new[] { "Error al agregar la entidad." });
        }

        public async Task<WritingResponse> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                return WritingResponse.Failure(new[] { "Entidad no encontrada." });
            }

            _dbSet.Remove(entity);
            var result = await SaveChangesAsync(cancellationToken);
            return result ? WritingResponse.Success() : WritingResponse.Failure(new[] { "Error al eliminar la entidad." });
        }

        public async Task<PaginatedResponse<TEntity>> GetAsync(PaginationRequest request, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();

            var totalCount = await query.CountAsync(cancellationToken);

            var pageNumber = request.PageNumber;
            var pageSize = request.PageSize;

            var items = await query.Skip((pageNumber - 1) * pageSize)
                                    .Take(pageSize)
                                    .ToListAsync(cancellationToken);

            return new PaginatedResponse<TEntity>(items.ToList().AsReadOnly(), totalCount, pageNumber, pageSize);
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await _dbSet.FindAsync(id, cancellationToken);
            return entity!; 
        }

        public async Task<WritingResponse> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            var result = await SaveChangesAsync(cancellationToken);
            return result ? WritingResponse.Success() : WritingResponse.Failure(new[] { "Error al actualizar la entidad." });
        }

        private async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken) > 0;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}
