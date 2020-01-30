using MyMusicSharedBackend.Core.Domain;
using MyMusicSharedBackend.Core.Dto.GatewayResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.Interfaces.Gateways
{
    /// <summary>
    /// Repository for User Data
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Creates a user in the repository
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>Identifier of the created user</returns>
        Task<GatewayResponse<int>> CreateUserAsync(User user);

        /// <summary>
        /// Searchs an User by the username
        /// </summary>
        /// <param name="username">Username to search</param>
        /// <returns>User data</returns>
        Task<GatewayResponse<User>> GetUserByUsername(string username);

        /// <summary>
        /// Search all users
        /// </summary>
        /// <returns>List of users</returns>
        Task<GatewayResponse<IEnumerable<User>>> SearchUsers();
    }
}