namespace MyMusicSharedBackend.Core.Interfaces
{
    /// <summary>
    /// Interface that will handle the return of the use case
    /// </summary>
    /// <typeparam name="TUseCaseResponse">Use case response type</typeparam>
    public interface IOutputPort<in TUseCaseResponse>
    {
        void Handle(TUseCaseResponse response);
    }
}