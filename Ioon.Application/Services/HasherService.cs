using Ioon.Domain.Common.Interfaces.Services;
using System.Security.Cryptography;

namespace Ioon.Application.Services
{
    public class HasherService : IHasherService
    {
        private const int SaltSize = 32;

        public (byte[] HashPassword, byte[] HashSalt) HashPassword(byte[] password)
        {
            byte[] salt = GenerateSalt();
            byte[] passwordBytes = password;
            byte[] combinedBytes = new byte[salt.Length + passwordBytes.Length];

            Buffer.BlockCopy(salt, 0, combinedBytes, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, salt.Length, passwordBytes.Length);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return (HashPassword: hashBytes, HashSalt: salt);
            }
        }

        public bool VerifyPassword(byte[] password, byte[] storedHash, byte[] storedSalt)
        {
            byte[] salt = storedSalt;
            byte[] passwordBytes = password;
            byte[] combinedBytes = new byte[salt.Length + passwordBytes.Length];

            Buffer.BlockCopy(salt, 0, combinedBytes, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, combinedBytes, salt.Length, passwordBytes.Length);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(combinedBytes);
                return CryptographicOperations.FixedTimeEquals(storedHash, hashBytes);
            }
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }
    }
}
