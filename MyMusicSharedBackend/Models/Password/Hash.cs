using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace MyMusicSharedBackend.Models.Password
{
    /// <summary>
    /// Hash the password
    /// </summary>
    public class Hash
    {
        /// <summary>
        /// Creates a Hash
        /// </summary>
        /// <param name="value">The Value to hash</param>
        /// <param name="salt">Salt to validade</param>
        /// <returns>Hashed Value</returns>
        public static string Create(string value, string salt)
        {
            var valueBytes = KeyDerivation.Pbkdf2(
                                password: value,
                                salt: Encoding.UTF8.GetBytes(salt),
                                prf: KeyDerivationPrf.HMACSHA512,
                                iterationCount: 10000,
                                numBytesRequested: 256 / 8);

            return Convert.ToBase64String(valueBytes);
        }

        /// <summary>
        /// Validats the hash to the value using the salt
        /// </summary>
        /// <param name="value">Value without hash</param>
        /// <param name="salt">Salt to validate</param>
        /// <param name="hash">Hashed value</param>
        /// <returns>True or false</returns>
        public static bool Validate(string value, string salt, string hash)
            => Create(value, salt) == hash;
    }
}