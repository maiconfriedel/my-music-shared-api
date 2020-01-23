using System.Collections.Generic;

namespace MyMusicSharedBackend.Core.Interfaces
{
    /// <summary>
    /// Base Response of an use case
    /// </summary>
    public abstract class UseCaseResponseMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="success">Indicates whether the operation was successful</param>
        /// <param name="message">Friendly return message to present to the user</param>
        /// <param name="errors">List of errors occurred if the operation is not successful</param>
        protected UseCaseResponseMessage(bool success = false, string message = null, IEnumerable<string> errors = null)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }

        /// <summary>
        /// List of errors occurred if the operation is not successful
        /// </summary>
        public IEnumerable<string> Errors { get; }

        /// <summary>
        /// Friendly return message to present to the user
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Indicates whether the operation was successful
        /// </summary>
        public bool Success { get; }
    }
}