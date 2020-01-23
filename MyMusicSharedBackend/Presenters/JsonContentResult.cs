using Microsoft.AspNetCore.Mvc;

namespace MyMusicSharedBackend.Presenters
{
    /// <summary>
    /// JSON Content Type
    /// </summary>
    public sealed class JsonContentResult : ContentResult
    {
        /// <summary>
        /// Class constructor
        /// </summary>
        public JsonContentResult()
        {
            ContentType = "application/json";
        }
    }
}