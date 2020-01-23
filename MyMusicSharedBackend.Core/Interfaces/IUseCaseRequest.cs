namespace MyMusicSharedBackend.Core.Interfaces
{
    /// <summary>
    /// Interface for requests to the use case
    /// </summary>
    /// <typeparam name="TUseCaseResponse">Type for use case response</typeparam>
    public interface IUseCaseRequest<out TUseCaseResponse> { }
}