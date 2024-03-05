namespace Ioon.Domain.Common.Interfaces.Base
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
