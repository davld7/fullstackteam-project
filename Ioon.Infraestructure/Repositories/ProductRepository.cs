using Dapper;
using Ioon.Application.Common.DTO;
using Ioon.Application.Common.Exceptions;
using Ioon.Application.Common.Interfaces.Repositories;
using Ioon.Domain;
using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ioon.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IDatabaseContext _DbContext;

        public ProductRepository(IDatabaseContext dbContext)
        {
            _DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }  


        public async ValueTask<int> AddAsync(Product entity)
        {
            try
            {
                var productParams = entity.ToDynamicParameters();

                int result = await _DbContext.DatabaseConnection.ExecuteAsync("[Runtime].[InsertProduct]", productParams, commandType: CommandType.StoredProcedure);

                return result;
            }
            catch (SqlException ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException!);
            }
        }

        public async ValueTask<int> UpdateAsync(Product entity)
        {
            try
            {
                using (var transaction = await _DbContext.BeginTransactionAsync())
                {
                    try
                    {
                        var productParams = entity.ToDynamicParameters();

                        int result = await _DbContext.DatabaseConnection.ExecuteAsync("[Runtime].[UpdateProduct]", productParams, transaction: transaction, commandType: CommandType.StoredProcedure);

                        await transaction.CommitAsync();

                        return result;
                    }
                    catch (SqlException ex)
                    {
                        await transaction.RollbackAsync();
                        throw new RepositoryException(ex.Message, ex.InnerException!);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException!);
            }
        }

        public async ValueTask<int> DeleteAsync(Guid Id)
        {
            try
            {
                using (var transaction = await _DbContext.BeginTransactionAsync())
                {
                    try
                    {
                        int result = await _DbContext.DatabaseConnection.ExecuteAsync("[Runtime].[DeleteProduct]", new { ProductUUID = Id }, commandType: CommandType.StoredProcedure);

                        await transaction.CommitAsync();

                        return result;
                    }
                    catch (SqlException ex)
                    {
                        await transaction.RollbackAsync();
                        throw new RepositoryException(ex.Message, ex.InnerException!);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RepositoryException(ex.Message, ex.InnerException!);
            }
        }

        public async ValueTask<ProductDTO?> GetByIdAsync(Guid Id)
        {
            return await _DbContext.DatabaseConnection.QueryFirstOrDefaultAsync<ProductDTO>("[Runtime].[GetProduct]", new { ProductUUID = Id }, commandType: CommandType.StoredProcedure);
        }

        public async ValueTask<IReadOnlyCollection<ProductDTO>?> GetAllAsync(Guid? Id = null)
        {
            return (IReadOnlyCollection<ProductDTO>)await _DbContext.DatabaseConnection.QueryAsync<ProductDTO>("[Runtime].[GetAllProducts]", new { BusinessUUID = Id }, commandType: CommandType.StoredProcedure);
        }
    }
}
