using Microsoft.EntityFrameworkCore;
using MyMusicSharedBackend.Core.Domain;
using MyMusicSharedBackend.Core.Dto.GatewayResponses;
using MyMusicSharedBackend.Core.Interfaces.Gateways;
using MyMusicSharedBackend.Infrastructure.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Infrastructure.Repositories
{
    /// <summary>
    /// Interface for token data access
    /// </summary>
    internal class TokenRepository : ITokenRepository
    {
        private readonly MyMusicSharedDbContext _myMusicSharedDbContext;

        public TokenRepository(MyMusicSharedDbContext myMusicSharedDbContext)
        {
            _myMusicSharedDbContext = myMusicSharedDbContext;
        }

        /// <summary>
        /// Create a new refresh token for a username
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="scopes">Scopes of the refresh token</param>
        /// <returns>Refresh token created</returns>
        public async Task<GatewayResponse<string>> CreateRefreshToken(string username, IEnumerable<string> scopes)
        {
            string refreshToken = Guid.NewGuid().ToString("N");

            Infrastructure.EntityFramework.Models.RefreshToken refreshTokenCreate = new EntityFramework.Models.RefreshToken()
            {
                Scopes = string.Join('+', scopes),
                RefreshTokenValue = refreshToken,
                Username = username,
                ValidUntil = DateTime.Now.AddDays(1)
            };

            _myMusicSharedDbContext.RefreshTokens.Add(refreshTokenCreate);
            await _myMusicSharedDbContext.SaveChangesAsync();

            return new GatewayResponse<string>(refreshToken);
        }

        /// <summary>
        /// Deletes the refresh token
        /// </summary>
        /// <param name="refreshToken">Refresh token</param>
        /// <returns>Result of the operation</returns>
        public async Task<GatewayResponse<bool>> DeleteRefreshToken(string refreshToken)
        {
            var deleteRefreshToken = await _myMusicSharedDbContext.RefreshTokens.FirstOrDefaultAsync(a => a.RefreshTokenValue == refreshToken);

            if (deleteRefreshToken != null)
            {
                _myMusicSharedDbContext.Remove(deleteRefreshToken);
                await _myMusicSharedDbContext.SaveChangesAsync();
            }

            return new GatewayResponse<bool>(true);
        }

        /// <summary>
        /// Searches the refresh token
        /// </summary>
        /// <param name="refreshToken">The refresh token</param>
        /// <returns>Refresh token data</returns>
        public async Task<GatewayResponse<RefreshToken>> SearchRefreshToken(string refreshToken)
        {
            var refreshTokenReturn = await _myMusicSharedDbContext.RefreshTokens.FirstOrDefaultAsync(a => a.RefreshTokenValue == refreshToken);

            if (refreshTokenReturn != null)
            {
                return new GatewayResponse<RefreshToken>(new RefreshToken(refreshTokenReturn.Id, refreshTokenReturn.RefreshTokenValue, refreshTokenReturn.ValidUntil, refreshTokenReturn.Username, refreshTokenReturn.Scopes));
            }
            else
            {
                return new GatewayResponse<RefreshToken>(null);
            }
        }
    }
}