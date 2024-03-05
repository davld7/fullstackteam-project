using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Infrastructure.Context;
using Microsoft.Extensions.Options;
using System.Data;
using System.Data.Common;

namespace Ioon.Infrastructure.Data
{
    public sealed class DatabaseContext : IDatabaseContext
    {
        public DbConnection DatabaseConnection { get; }

        private DbTransaction DbTransaction
        {
            get
            {
                return DbTransaction;
            }
            set
            {
                DbTransaction = value;
            }
        }

        public DatabaseContext(IOptions<DatabaseOptions> options)
        {
            DatabaseConnection = options.Value.CreateConnection();
        }

        public async Task<DbTransaction> BeginTransactionAsync()
        {
            if (DatabaseConnection.State != ConnectionState.Open)
            {
                await DatabaseConnection.OpenAsync();
            }

            DbTransaction = await DatabaseConnection.BeginTransactionAsync();

            return DbTransaction;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                if (DbTransaction != null)
                {
                    await DbTransaction.CommitAsync();
                }
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            try
            {
                if (DbTransaction != null)
                {
                    await DbTransaction.RollbackAsync();
                }
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        private async Task DisposeTransactionAsync()
        {
            DbTransaction?.Dispose();
            DbTransaction = null;

            if (DatabaseConnection.State == ConnectionState.Open)
            {
                await DatabaseConnection.CloseAsync();
            }
        }

        public async void Dispose()
        {
            await DatabaseConnection.DisposeAsync();
            await DisposeTransactionAsync();
        }

    }
}
