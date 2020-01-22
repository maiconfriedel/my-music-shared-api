using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyMusicSharedBackend.Database;
using MyMusicSharedBackend.Models;

namespace MyMusicSharedBackend.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyMusicSharedDbContext _context;

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="context">Db Context</param>
        /// <param name="configuration">Configuration file</param>
        public UsersController(MyMusicSharedDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>List of all users</returns>
        [HttpGet]
        [Authorize(policy: "read")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id">User identifier</param>
        /// <returns>Details of an user</returns>
        [HttpGet("{id}")]
        [Authorize(policy: "read")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>The created user</returns>
        [HttpPost]
        //[Authorize(policy: "write")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            string salt = _configuration.GetSection("Security").GetSection("PasswordHashSalt").Value;
            user.Password = Models.Password.Hash.Create(user.ToString(), salt);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
    }
}