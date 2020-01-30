using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Dto
{
    /// <summary>
    /// User Dto
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Constructor with defined values
        /// </summary>
        /// <param name="id">Identifier of the user</param>
        /// <param name="email">Email of the user</param>
        /// <param name="username">Username for login</param>
        /// <param name="password">Password for login</param>
        /// <param name="fullName">Full Name</param>
        /// <param name="bio">Bio of the user</param>
        public UserDto(int id, string email, string username, string password, string fullName, string bio)
        {
            Id = id;
            Email = email;
            Username = username;
            Password = password;
            FullName = fullName;
            Bio = bio;
        }

        /// <summary>
        /// Empty constructor for default values
        /// </summary>
        public UserDto()
        {
        }

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