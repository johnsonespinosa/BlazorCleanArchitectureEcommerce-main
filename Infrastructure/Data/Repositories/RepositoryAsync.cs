using Application.Interfaces;
using Ardalis.Specification.EntityFrameworkCore;
using Infrastructure.Data.Contexts;

namespace Infrastructure.Data.Repositories
{
    public class RepositoryAsync<TEntity> : RepositoryBase<TEntity>, IRepositoryAsync<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;

        public RepositoryAsync(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
