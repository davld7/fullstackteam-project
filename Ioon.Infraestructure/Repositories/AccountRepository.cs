using Dapper;
using Ioon.Application.Common.DTO;
using Ioon.Application.Common.Exceptions;
using Ioon.Application.Common.Interfaces.Repositories;
using Ioon.Domain;
using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.Common.Interfaces.Services;
using Ioon.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Ioon.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        protected readonly IHasherService HashService;
        protected readonly IDatabaseContext Context;

        public AccountRepository(IHasherService hashService, IDatabaseContext _DbContext)
        {
            HashService = hashService;
            Context = _DbContext;
        }

        #region Metodos CRUD
        public async ValueTask<int> AddAccountAsync(Owner owner, Business business)
        {
            try
            {
                using (var transaction = await Context.BeginTransactionAsync())
                {
                    try
                    {
                        DynamicParameters ParamsAccount = new DynamicParameters();

                        business.AddParams(ParamsAccount);
                        owner.AddParams(ParamsAccount);

                        int result = await Context.DatabaseConnection.ExecuteAsync("[Runtime].[CreateAccount]", ParamsAccount, transaction: transaction, commandType: CommandType.StoredProcedure);

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

        public ValueTask<int> AddAccountConfigurationAsync(BusinessConfiguration accountConfiguration)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<int> AddUserAccountAsync(User user)
        {
            try
            {
                using (var transaction = await Context.BeginTransactionAsync())
                {
                    try
                    {
                        var Params = user.ToDynamicParameters();

                        int result = await Context.DatabaseConnection.ExecuteAsync("[Runtime].[InsertUser]", Params, transaction: transaction, commandType: CommandType.StoredProcedure);

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

        public ValueTask<int> DeleteAccountConfigurationAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public ValueTask<int> DeleteAccountUserAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public async ValueTask<AccountDTO?> GetDataAccountAsync(Guid userId)
        {
            return await Context.DatabaseConnection.QueryFirstOrDefaultAsync<AccountDTO>("[Runtime].[GetDataAccount]", new { UserId = userId }, commandType: CommandType.StoredProcedure);
        }

        public async ValueTask<(Guid UserId, byte[] HashPassword, byte[] HashSalt)> GetInfoUserAsync(string email)
        {
            return await Context.DatabaseConnection.QuerySingleOrDefaultAsync<(Guid UserId, byte[] HashPassword, byte[] HashSalt)>("[Runtime].[GetUserInfo]", new { Email = email }, commandType: CommandType.StoredProcedure);
        }

        public ValueTask<int> UpdateAccountConfigurationAsync(BusinessConfiguration accountConfiguration)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
