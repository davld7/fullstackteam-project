using Ioon.Application.Common.DTO;
using Ioon.Domain;

namespace Ioon.Application.Common.Interfaces.Repositories
{
    public interface IAccountRepository
    {
        ValueTask<int> AddAccountAsync(Owner owner, Business business);
        ValueTask<int> AddUserAccountAsync(User user);
        ValueTask<int> AddAccountConfigurationAsync(BusinessConfiguration accountConfiguration);

        ValueTask<int> UpdateAccountConfigurationAsync(BusinessConfiguration accountConfiguration);
        ValueTask<int> UpdateUserAccountAsync(User user);
        ValueTask<int> UpdateAccountAsync(Owner owner, Business business);

        ValueTask<int> DeleteUserAccountAsync(Guid Id);
        ValueTask<int> DeleteAccountConfigurationAsync(Guid Id);
        ValueTask<(Guid UserId, byte[] HashPassword, byte[] HashSalt)> GetInfoUserAsync(string email);
        ValueTask<AccountDTO?> GetDataAccountAsync(Guid userId);
    }
}
