using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace realstate.Helpers
{
    public static class PasswordHelper
    {
        // Configuration: salt size in bytes and iteration count
        private const int SaltSize = 16;        // 128 bit
        private const int KeySize = 32;         // 256 bit
        private const int Iterations = 100_000; // Recommended minimum

        /// <summary>
        /// Hashes a plaintext password, returning a string that includes salt and hash.
        /// Format: {iterations}.{saltBase64}.{hashBase64}
        /// </summary>
        public static string Hash(string password)
        {
            // 1. Generate a salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // 2. Derive the subkey (hash) 
            byte[] subkey = KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: Iterations,
                numBytesRequested: KeySize);

            // 3. Format: {iterations}.{salt}.{hash}
            return $"{Iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(subkey)}";
        }

        /// <summary>
        /// Verifies a supplied plaintext password against the stored hash.
        /// </summary>
        public static bool Verify(string hashedPassword, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(hashedPassword))
                return false;

            var parts = hashedPassword.Split('.', 3);
            if (parts.Length != 3 ||
                !int.TryParse(parts[0], out int iterations) ||
                parts[1].Length == 0 ||
                parts[2].Length == 0)
            {
                // Invalid format
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] expectedSubkey = Convert.FromBase64String(parts[2]);

            // Derive a key from the incoming password using the same salt and iterations
            byte[] actualSubkey = KeyDerivation.Pbkdf2(
                password: plainPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: iterations,
                numBytesRequested: expectedSubkey.Length);

            // Compare in constant time to prevent timing attacks
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
    }
}
