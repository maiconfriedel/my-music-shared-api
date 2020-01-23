using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyMusicSharedBackend.Core.Domain;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyMusicSharedBackend.Services
{
    /// <summary>
    /// Token methods
    /// </summary>
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="configuration">Configuration properties</param>
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Generates a token
        /// </summary>
        /// <param name="user">User data</param>
        /// <param name="scopes">Scopes for the token</param>
        /// <returns>JWT Token</returns>
        public string GenerateToken(User user, IEnumerable<string> scopes)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Security").GetSection("JWTSecurityKey").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim("email", user.Email.ToString()),
                    new Claim("scope", string.Join(" ", scopes))
                }),
                NotBefore = DateTime.Now,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "MyMusicShared"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}