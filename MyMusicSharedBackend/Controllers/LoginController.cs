﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyMusicSharedBackend.Database;
using MyMusicSharedBackend.Models;
using MyMusicSharedBackend.Services;

namespace MyMusicSharedBackend.Controllers
{
    /// <summary>
    /// Controller for login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly MyMusicSharedDbContext _context;

        private readonly TokenService _tokenService;

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <param name="tokenService">Token service</param>
        /// <param name="configuration">Configuration properties</param>
        public LoginController(MyMusicSharedDbContext context, TokenService tokenService, IConfiguration configuration)
        {
            _context = context;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        /// <summary>
        /// Logins to the application
        /// </summary>
        /// <param name="login">Login Data</param>
        /// <returns>JWT Token</returns>
        [HttpPost]
        public async Task<ActionResult<object>> Login([FromBody] Login login)
        {
            string salt = _configuration.GetSection("Security").GetSection("PasswordHashSalt").Value;

            var user = await _context.Users.FirstOrDefaultAsync(a => a.Username == login.Username);

            if (user == null)
            {
                return BadRequest(new { error = "Invalid login" });
            }

            bool passwordMatch = Models.Password.Hash.Validate(login.ToString(), salt, user.Password);

            if (!passwordMatch)
            {
                return BadRequest(new { error = "Invalid login" });
            }

            var token = _tokenService.GenerateToken(user, login.Scopes);

            return new { token };
        }
    }
}