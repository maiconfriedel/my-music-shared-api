using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Dto
{
    /// <summary>
    /// Class to login
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Constructor with values
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="scopes"></param>
        /// <param name="refreshToken"></param>
        public LoginDto(string username, string password, IEnumerable<string> scopes, string refreshToken)
        {
            Username = username;
            Password = password;
            Scopes = scopes;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Constructor with default values
        /// </summary>
        public LoginDto()
        {
        }

        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Scopes to authenticate
        /// </summary>
        public IEnumerable<string> Scopes { get; set; }

        /// <summary>
        /// Refresh token to get new Token
        /// </summary>
        public string RefreshToken { get; set; }

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