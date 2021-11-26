using System.Security.Cryptography;
using System.Text;

namespace CKO.Payments.BL.Helpers
{
    public static class SecurityHelper
    {
        public const int SALT_MAX_LENGTH = 32;

        public static string GenerateHash(string hashPart, string salt)
        {
            // Generate a new salt if one was not passed through
            salt ??= GetNewSalt();

            using var sha = SHA256.Create();
            var hashBytes = sha.ComputeHash(Encoding.Default.GetBytes(hashPart + salt));

            return Convert.ToBase64String(hashBytes);
        }

        private static string GetNewSalt()
        {
            var salt = new byte[SALT_MAX_LENGTH];

            using var rng = RandomNumberGenerator.Create();
            rng.GetNonZeroBytes(salt);

            return Convert.ToBase64String(salt);
        }
    }
}
