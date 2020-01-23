using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Domain
{
    /// <summary>
    /// Token Data
    /// </summary>
    public class Token
    {
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