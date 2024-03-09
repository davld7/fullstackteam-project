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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDatabaseContext _DbContext;

        public CategoryRepository(IDatabaseContext dbContext)
        {
            _DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
       
        public async ValueTask<int> AddAsync(Category entity)
        {
            try
            {
                using (var transaction = await _DbContext.BeginTransactionAsync())
                {
                    try
                    {
                        var Params = entity.ToDynamicParameters();

                        int result = await _DbContext.DatabaseConnection.ExecuteAsync("[Runtime].[InsertCategory]", Params, transaction: transaction, commandType: CommandType.StoredProcedure);

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

        public async ValueTask<int> UpdateAsync(Category entity)
        {
            try
            {
                using (var transaction = await _DbContext.BeginTransactionAsync())
                {
                    try
                    {
                        var Params = entity.ToDynamicParameters();

                        int result = await _DbContext.DatabaseConnection.ExecuteAsync("[Runtime].[InsertCategory]", Params, transaction: transaction, commandType: CommandType.StoredProcedure);

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
                        int result = await _DbContext.DatabaseConnection.ExecuteAsync("[Runtime].[DeleteCategory]", new { CategoryUUID = Id }, commandType: CommandType.StoredProcedure);

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

        public async ValueTask<CategoryDTO?> GetByIdAsync(Guid Id)
        {
            return await _DbContext.DatabaseConnection.QueryFirstOrDefaultAsync<CategoryDTO>("[Runtime].[GetCategory]", new { CategoryUUID = Id }, commandType: CommandType.StoredProcedure);
        }    

        public async ValueTask<IReadOnlyCollection<CategoryDTO>?> GetAllAsync(Guid? Id = null)
        {
            return (IReadOnlyCollection<CategoryDTO>?)await _DbContext.DatabaseConnection.QueryAsync<CategoryDTO>("[Runtime].[GetAllCategories]", new { BusinessUUID = Id }, commandType: CommandType.StoredProcedure);
        }
     
    }
}
