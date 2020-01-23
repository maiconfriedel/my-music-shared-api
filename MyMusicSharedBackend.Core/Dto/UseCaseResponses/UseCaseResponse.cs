using MyMusicSharedBackend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Dto.UseCaseResponses
{
    /// <summary>
    /// Use case response
    /// </summary>
    /// <typeparam name="TResult">Use case result type</typeparam>
    public class UseCaseResponse<TResult> : UseCaseResponseMessage
    {
        /// <summary>
        /// Constructor for cases where the operation was not successful
        /// </summary>
        /// <param name="message">Friendly return message to present to the user</param>
        /// <param name="errors">List of errors occurred if the operation is not successful</param>
        public UseCaseResponse(string message, IEnumerable<string> errors) : base(false, message, errors)
        {
        }

        /// <summary>
        /// Constructor for cases where the operation was successful
        /// </summary>
        /// <param name="result">Entity resulting from the execution of the use case</param>
        public UseCaseResponse(TResult result) : base(true)
        {
            Result = result;
        }

        /// <summary>
        /// Class constructor with all attributes
        /// </summary>
        /// <param name="result">Entity resulting from the execution of the use case</param>
        /// <param name="success">Operation result indicator</param>
        /// <param name="message">Friendly return message to present to the user</param>
        /// <param name="errors">List of errors occurred if the operation is not successful</param>
        public UseCaseResponse(TResult result, bool success, string message, IEnumerable<string> errors) : base(success, message, errors)
        {
            Result = result;
        }

        /// <summary>
        /// Entity resulting from the execution of the use case
        /// </summary>
        public TResult Result { get; }
    }
}