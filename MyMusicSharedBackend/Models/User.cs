using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Models
{
    /// <summary>
    /// User Data
    /// </summary>
    public class User
    {
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
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Bio of the user
        /// </summary>
        [MaxLength(100)]
        public string Bio { get; set; }
    }
}