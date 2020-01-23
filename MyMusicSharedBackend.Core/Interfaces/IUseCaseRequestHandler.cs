using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.Interfaces
{
    /// <summary>
    /// Interface that will handle a request to a use case
    /// </summary>
    /// <typeparam name="TUseCaseRequest">Request type for the use case</typeparam>
    /// <typeparam name="TUseCaseResponse">Response type for the use case</typeparam>
    public interface IUseCaseRequestHandler<in TUseCaseRequest, out TUseCaseResponse> where TUseCaseRequest : IUseCaseRequest<TUseCaseResponse>
    {
        Task<bool> HandleAsync(TUseCaseRequest message, IOutputPort<TUseCaseResponse> outputPort);
    }
}