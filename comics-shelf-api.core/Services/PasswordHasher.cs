using comics_shelf_api.core.Consts;
using comics_shelf_api.core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace comics_shelf_api.core.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        /// <summary>
        /// Size of salt.
        /// </summary>
        private const int SaltSize = 16;

        /// <summary>
        /// Size of hash.
        /// </summary>
        private const int HashSize = 20;

        private const int iterations = 10000;

        /// <summary>
        /// Creates a hash from a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The hash.</returns>
        public string GetHash(string password)
        {
            // Create salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt;
                rng.GetBytes(salt = new byte[SaltSize]);
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
                {
                    var hash = pbkdf2.GetBytes(HashSize);
                    // Combine salt and hash
                    var hashBytes = new byte[SaltSize + HashSize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                    // Convert to base64
                    var base64Hash = Convert.ToBase64String(hashBytes);

                    // Format hash with extra information
                    return $"$HASH|V1${iterations}${base64Hash}";
                }
            }

        }

        /// <summary>
        /// Checks if hash is supported.
        /// </summary>
        /// <param name="hashString">The hash.</param>
        /// <returns>Is supported?</returns>
        private bool IsHashSupported(string hashString)
        {
            return hashString.Contains("HASH|V1$");
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public bool VerifyPassword(string hashedPassword, string password)
        {
            // Check hash
            if (!IsHashSupported(hashedPassword))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$HASH|V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations))
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                // Get result
                for (var i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                    {
                        return false;
                    }
                }

                return true;
            }

        }
    }
}
