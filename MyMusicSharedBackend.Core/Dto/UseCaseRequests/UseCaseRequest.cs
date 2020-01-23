using MyMusicSharedBackend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Dto.UseCaseRequests
{
    /// <summary>
    /// Request for Use Cases
    /// </summary>
    /// <typeparam name="TRequest">Type of request content</typeparam>
    /// <typeparam name="TResponse">Return type of use case</typeparam>
    public class UseCaseRequest<TRequest, TResponse> : IUseCaseRequest<TResponse> where TResponse : UseCaseResponseMessage
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="requestContent">Request content for the use case</param>
        public UseCaseRequest(TRequest requestContent)
        {
            RequestContent = requestContent;
        }

        /// <summary>
        /// Request content for the use case
        /// </summary>
        public TRequest RequestContent { get; }
    }
}