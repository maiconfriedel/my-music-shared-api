﻿using System;
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
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Username for login
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Password for login
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Full Name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Bio of the user
        /// </summary>
        [MaxLength(100)]
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