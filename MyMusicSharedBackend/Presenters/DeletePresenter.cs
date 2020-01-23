using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using MyMusicSharedBackend.Core.Interfaces;
using System.Net;
using System.Text.Json;

namespace MyMusicSharedBackend.Presenters
{
    /// <summary>
    /// Object responsible for formatting the presentation of the return of the use case in DELETE methods
    /// </summary>
    /// <typeparam name="TResult">Use case result type</typeparam>
    public class DeletePresenter<TResult> : IOutputPort<UseCaseResponse<TResult>>
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public DeletePresenter()
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
        public void Handle(UseCaseResponse<TResult> response)
        {
            if (response is null)
            {
                throw new System.ArgumentNullException(nameof(response));
            }

            ContentResult.StatusCode = response.Success ? (int)(HttpStatusCode.OK) : (int)(HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.Serialize(new { response.Success, response.Message, response.Errors });
        }
    }
}