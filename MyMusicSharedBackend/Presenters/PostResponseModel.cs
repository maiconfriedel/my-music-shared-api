using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Presenters
{
    /// <summary>
    /// POST Method Response
    /// </summary>
    /// <typeparam name="TResult">Result Type</typeparam>
    public class PostResponseModel<TResult>
    {
        /// <summary>
        /// Error Messages
        /// </summary>
        public IEnumerable<string> Errors { get; set; }

        /// <summary>
        /// Identifier of the created entity
        /// </summary>
        public TResult Id { get; set; }

        /// <summary>
        /// Friendly message to present to the user in case of error
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Operation status
        /// </summary>
        public bool Success { get; set; }
    }
}