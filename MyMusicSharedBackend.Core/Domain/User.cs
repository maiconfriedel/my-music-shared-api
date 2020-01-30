using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Domain
{
    /// <summary>
    /// User Data
    /// </summary>
    public class User
    {
        /// <summary>
        /// Empty constructor for default values
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Constructor with defined values
        /// </summary>
        /// <param name="id">Identifier of the user</param>
        /// <param name="email">Email of the user</param>
        /// <param name="username">Username for login</param>
        /// <param name="password">Password for login</param>
        /// <param name="fullName">Full Name</param>
        /// <param name="bio">Bio of the user</param>
        public User(int id, string email, string username, string password, string fullName, string bio)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
            FullName = fullName;
            Bio = bio;
        }

        /// <summary>
        /// Creates a Hash
        /// </summary>
        /// <param name="value">The Value to hash</param>
        /// <param name="salt">Salt to validade</param>
        /// <returns>Hashed Value</returns>
        public string HashPassword(string value, string salt)
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
        public bool ValidatePassword(string value, string salt, string hash) => HashPassword(value, salt) == hash;

        /// <summary>
        /// User Identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Email of the user
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Username for login
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password for login
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Bio of the user
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// ToString of the class
        /// </summary>
        /// <returns>String</returns>
        public override string ToString()
        {
            return $"{Username.TrimEnd()} - {Password.TrimEnd()}";
        }
    }
}