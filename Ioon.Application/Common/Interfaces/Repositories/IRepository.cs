namespace Ioon.Application.Common.Interfaces.Repositories
{
    public interface IRepository<TEntity, TResponse>
    {
        ValueTask<int> AddAsync(TEntity entity);
        ValueTask<int> UpdateAsync(TEntity entity);
        ValueTask<int> DeleteAsync(Guid Id);
        ValueTask<TResponse?> GetByIdAsync(Guid Id);
        ValueTask<IReadOnlyCollection<TResponse>?> GetAllAsync(Guid? Id = default);
    }
}
