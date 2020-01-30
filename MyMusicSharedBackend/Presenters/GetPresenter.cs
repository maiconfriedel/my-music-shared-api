using AutoMapper;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using MyMusicSharedBackend.Core.Interfaces;
using System;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyMusicSharedBackend.Presenters
{
    /// <summary>
    /// Object responsible for formatting the presentation of the return of the use case in GET methods
    /// </summary>
    /// <typeparam name="TResult">Use case result type</typeparam>
    /// <typeparam name="TModel">Presentation template type</typeparam>
    public class GetPresenter<TResult, TModel> : IOutputPort<UseCaseResponse<TResult>>
    {
        /// <summary>
        /// AutoMapper instance
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="mapper">AutoMapper instance</param>
        public GetPresenter(IMapper mapper)
        {
            ContentResult = new JsonContentResult();
            _mapper = mapper;
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
                throw new ArgumentNullException(nameof(response));
            }

            if (response.Success)
            {
                if (response.Result == null)
                {
                    ContentResult.StatusCode = (int)HttpStatusCode.NoContent;
                    ContentResult.Content = null;
                }
                else
                {
                    ContentResult.StatusCode = (int)HttpStatusCode.OK;

                    JsonSerializerOptions serializerOptions = new JsonSerializerOptions
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    };
                    serializerOptions.Converters.Add(new JsonStringEnumConverter());

                    TModel model = _mapper.Map<TModel>(response.Result);

                    ContentResult.Content = JsonSerializer.Serialize(model, model.GetType(), serializerOptions);
                }
            }
            else
            {
                ContentResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                JsonSerializerOptions serializerOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                ContentResult.Content = JsonSerializer.Serialize(new { response.Success, response.Message, response.Errors }, serializerOptions);
            }
        }
    }
}