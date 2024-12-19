using Ardalis.Specification;

namespace Application.Interfaces
{
    public interface IRepositoryAsync<TEntity> : IRepositoryBase<TEntity> where TEntity : class { }
    public interface IReadRepositoryAsync<TEntity> : IReadRepositoryBase<TEntity> where TEntity : class { }
}
