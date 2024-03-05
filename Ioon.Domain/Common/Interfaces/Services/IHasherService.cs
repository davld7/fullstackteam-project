namespace Ioon.Domain.Common.Interfaces.Services
{
    public interface IHasherService
    {
        (byte[] HashPassword, byte[] HashSalt) HashPassword(byte[] password);
        bool VerifyPassword(byte[] password, byte[] storedHash, byte[] storedSalt);
    }
}
