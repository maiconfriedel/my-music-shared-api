using MyMusicSharedBackend.Core.Domain;
using MyMusicSharedBackend.Core.Dto.GatewayResponses;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.Interfaces.Gateways
{
    /// <summary>
    /// Interface for token data access
    /// </summary>
    public interface ITokenRepository
    {
        /// <summary>
        /// Create a new refresh token for a username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="scopes">Scopes of the refresh token</param>
        /// <returns>Refresh token created</returns>
        Task<GatewayResponse<string>> CreateRefreshToken(string username, IEnumerable<string> scopes);

        /// <summary>
        /// Searches the refresh token
        /// </summary>
        /// <param name="refreshToken">The refresh token</param>
        /// <returns>Refresh token data</returns>
        Task<GatewayResponse<RefreshToken>> SearchRefreshToken(string refreshToken);

        /// <summary>
        /// Deletes the refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Result of the operation</returns>
        Task<GatewayResponse<bool>> DeleteRefreshToken(string refreshToken);
    }
}