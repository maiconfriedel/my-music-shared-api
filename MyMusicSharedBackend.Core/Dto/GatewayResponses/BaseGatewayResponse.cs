using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Dto.GatewayResponses
{
    /// <summary>
    /// Base response of a repository
    /// </summary>
    public abstract class BaseGatewayResponse
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="success">Indicates whether the operation was successful</param>
        /// <param name="errors">List of errors occurred if the operation is not successful</param>
        protected BaseGatewayResponse(bool success = true, IEnumerable<string> errors = null, string message = null)
        {
            Success = success;
            Errors = errors;
            Message = message;
        }

        /// <summary>
        /// List of errors occurred if the operation is not successful
        /// </summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool Success { get; }
    }
}