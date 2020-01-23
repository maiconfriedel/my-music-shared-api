using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Models
{
    /// <summary>
    /// Token Data
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Constructor with values
        /// </summary>
        /// <param name="type"></param>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        public Token(string type, string accessToken, string refreshToken)
        {
            Type = type;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Empty constructor with default values
        /// </summary>
        public Token()
        {
        }

        /// <summary>
        /// Token Type
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The token itself
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Token to authenticate without passing user and password again
        /// </summary>
        public string RefreshToken { get; set; }
    }
}