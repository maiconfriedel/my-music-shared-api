﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.Domain
{
    /// <summary>
    /// Refresh Token
    /// </summary>
    public class RefreshToken
    {
        public RefreshToken(int id, string refreshTokenValue, DateTime validUntil, string username, string scopes)
        {
            Id = id;
            RefreshTokenValue = refreshTokenValue;
            ValidUntil = validUntil;
            Username = username;
            Scopes = scopes;
        }

        public RefreshToken()
        {
        }

        /// <summary>
        /// Identifier of the refresh token
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Value of the refresh token
        /// </summary>
        public string RefreshTokenValue { get; set; }

        /// <summary>
        /// Until when the token is valid
        /// </summary>
        public DateTime ValidUntil { get; set; }

        /// <summary>
        /// Username of the token
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Scopes for the token
        /// </summary>
        public string Scopes { get; set; }

        /// <summary>
        /// The refresh token stills valid
        /// </summary>
        [NotMapped]
        public bool IsValid { get { return ValidUntil > DateTime.Now; } }
    }
}