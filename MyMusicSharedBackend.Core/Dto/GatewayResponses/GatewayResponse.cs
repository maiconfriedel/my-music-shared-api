using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Dto.GatewayResponses
{
    /// <summary>
    /// Response of a repository
    /// </summary>
    /// <typeparam name="TResult">Response type</typeparam>
    public class GatewayResponse<TResult> : BaseGatewayResponse
    {
        /// <summary>
        /// Constructor for cases where the operation is successful
        /// </summary>
        /// <param name="result">Result of the operation in the repository</param>
        public GatewayResponse(TResult result)
        {
            Result = result;
        }

        /// <summary>
        /// Constructor for cases where the operation is not successful
        /// </summary>
        /// <param name="errors">List of errors occurred</param>
        public GatewayResponse(IEnumerable<string> errors = null, string message = null) : base(false, errors, message)
        {
        }

        /// <summary>
        /// Result of the operation in the repository
        /// </summary>
        public TResult Result { get; }
    }
}