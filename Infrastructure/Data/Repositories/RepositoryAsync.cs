using Application.Interfaces;
using Application.Models;
using Domain.Commons;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryAsync<TEntity> : IRepositoryAsync<TEntity> where TEntity : BaseAuditableEntity
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryAsync(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<Guid> AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }

        public async Task<Guid> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
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

        public async Task<Guid> UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Id;
        }
    }
}
