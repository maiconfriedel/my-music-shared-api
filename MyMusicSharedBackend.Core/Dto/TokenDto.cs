using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Dto
{
    /// <summary>
    /// Token Data
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// Constructor with data
        /// </summary>
        /// <param name="type"> Token Type</param>
        /// <param name="accessToken">The token itself</param>
        /// <param name="refreshToken">Token to authenticate without passing user and password again</param>
        public TokenDto(string type, string accessToken, string refreshToken)
        {
            Type = type;
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

        /// <summary>
        /// Empty constructor for default values
        /// </summary>
        public TokenDto()
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