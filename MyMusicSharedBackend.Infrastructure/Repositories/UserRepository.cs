using Microsoft.EntityFrameworkCore;
using MyMusicSharedBackend.Core.Domain;
using MyMusicSharedBackend.Core.Dto.GatewayResponses;
using MyMusicSharedBackend.Core.Interfaces.Gateways;
using MyMusicSharedBackend.Infrastructure.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Infrastructure.Repositories
{
    /// <summary>
    /// Repository for User Data
    /// </summary>
    internal class UserRepository : IUserRepository
    {
        private readonly MyMusicSharedDbContext _mySharedMusicDbContext;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="mySharedMusicDbContext">DB Context</param>
        public UserRepository(MyMusicSharedDbContext mySharedMusicDbContext)
        {
            _mySharedMusicDbContext = mySharedMusicDbContext;
        }

        /// <summary>
        /// Creates a user in the repository
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>Identifier of the created user</returns>
        public async Task<GatewayResponse<int>> CreateUserAsync(User user)
        {
            EntityFramework.Models.User userCreate = new EntityFramework.Models.User { Email = user.Email, Username = user.Username, Bio = user.Bio, FullName = user.FullName, Password = user.Password };

            var userCreated = _mySharedMusicDbContext.Users.Add(userCreate);

            await _mySharedMusicDbContext.SaveChangesAsync();
            return new GatewayResponse<int>(userCreated.Entity.Id);
        }

        /// <summary>
        /// Searchs an User by the username
        /// </summary>
        /// <param name="username">Username to search</param>
        /// <returns>User data</returns>
        public async Task<GatewayResponse<User>> GetUserByUsername(string username)
        {
            var user = await _mySharedMusicDbContext.Users.FirstOrDefaultAsync(a => a.Username == username);

            if (user == null)
            {
                return new GatewayResponse<User>(null);
            }

            return new GatewayResponse<User>(new User(user.Id, user.Email, user.Username, user.Password, user.FullName, user.Bio));
        }

        /// <summary>
        /// Search all users
        /// </summary>
        /// <returns>List of users</returns>
        public async Task<GatewayResponse<IEnumerable<User>>> SearchUsers()
        {
            var users = await _mySharedMusicDbContext.Users.ToArrayAsync();

            return new GatewayResponse<IEnumerable<User>>(users.Select(a => new User(a.Id, a.Email, a.Username, null, a.FullName, a.Bio)).ToArray());
        }
    }
}