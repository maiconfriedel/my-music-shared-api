using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using MyMusicSharedBackend.Core.Interfaces;
using MyMusicSharedBackend.Models;
using System.Net;
using System.Text.Json;

namespace MyMusicSharedBackend.Presenters
{
    /// <summary>
    /// Presents the token for the API in JSON Format
    /// </summary>
    public class LoginUserPresenter : IOutputPort<UseCaseResponse<TokenDto>>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public LoginUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        /// <summary>
        /// Presentation content
        /// </summary>
        public JsonContentResult ContentResult { get; }

        /// <summary>
        /// Handles the result of the use case and prepares the presentation
        /// </summary>
        /// <param name="response">Result of the use case</param>
        public void Handle(UseCaseResponse<TokenDto> response)
        {
            if (response is null)
            {
                throw new System.ArgumentNullException(nameof(response));
            }

            ContentResult.StatusCode = response.Success ? (int)(HttpStatusCode.OK) : (int)(HttpStatusCode.BadRequest);
            ContentResult.Content = response.Success ? JsonSerializer.Serialize(new Token(response.Result.Type, response.Result.AccessToken, response.Result.RefreshToken)) : JsonSerializer.Serialize(response);
        }
    }
}