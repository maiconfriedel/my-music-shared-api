using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Models
{
    /// <summary>
    /// Class to login
    /// </summary>
    public class Login
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Scopes to authenticate
        /// </summary>
        [Required]
        public IEnumerable<string> Scopes { get; set; }

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