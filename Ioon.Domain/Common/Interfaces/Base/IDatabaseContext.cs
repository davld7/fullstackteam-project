using System.Data.Common;

namespace Ioon.Domain.Common.Interfaces.Base
{
    public interface IDatabaseContext : IDisposable
    {
        DbConnection DatabaseConnection { get; }

        Task<DbTransaction> BeginTransactionAsync();

        Task SaveChangesAsync();

        Task RollbackTransactionAsync();
    }

}
