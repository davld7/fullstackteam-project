namespace Ioon.Domain.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        ValueTask AuthenticateUserAsync(string username, byte[] password);
        ValueTask<bool> VerifyUser(string email, string identifier);
        Task RegisterUserAsync(User user);
    }
}
