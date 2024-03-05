using Dapper;
using Ioon.Application.Common.Interfaces.Data;
using Ioon.Domain;
using Ioon.Domain.Common.Interfaces.Base;
using Ioon.Domain.Common.Interfaces.Repositories;
using Ioon.Domain.Common.Interfaces.Services;
using Ioon.Infrastructure.Persistence;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Ioon.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly IHasherService HashService;
        protected readonly ApplicationDbContext _DbContext;

        public UserRepository(IHasherService hashService, ApplicationDbContext appDbContext)
        {
            HashService = hashService;
            _DbContext = appDbContext;
        }

        public async ValueTask AuthenticateUserAsync(string email, byte[] password)
        {
            //using (var transaction = await Context.BeginTransactionAsync())
            //{
            //    try
            //    {
            //        string userAuthquery = @"SELECT U.IdUsuario AS UserId, U.HashClaveAcceso AS HashPassword, U.HashSalt 
            //                          FROM `usuarios` AS U WHERE U.Correo = @Email AND U.Estado = 1;";

            //        var result = await Context.DatabaseConnection.QuerySingleOrDefaultAsync<(int UserId, byte[] HashPassword, byte[] HashSalt)>(userAuthquery, new { Email = email }, transaction: transaction, commandType: CommandType.Text);

            //        if (result.UserId != 0)
            //        {
            //            if (HashService.VerifyPassword(password, result.HashPassword, result.HashSalt))
            //            {

            //                await transaction.CommitAsync();

            //            }
            //        }

            //    }
            //    catch (SqlException)
            //    {
            //        await transaction.RollbackAsync();

            //    }

            //}

            throw new NotImplementedException();
        }

        public async Task RegisterUserAsync(User user) => await _DbContext.Users.AddAsync(user);
        

        public async ValueTask<bool> VerifyUser(string email, string identifier)
        {
            return await Task.FromResult(true);
        }
    }
}
