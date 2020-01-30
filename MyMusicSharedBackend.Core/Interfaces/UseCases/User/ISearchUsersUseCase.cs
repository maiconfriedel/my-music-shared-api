using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseRequests;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Interfaces.UseCases.User
{
    /// <summary>
    /// Interface for search all users use case
    /// </summary>
    public interface ISearchUsersUseCase : IUseCaseRequestHandler<UseCaseRequest<bool, UseCaseResponse<IEnumerable<UserDto>>>, UseCaseResponse<IEnumerable<UserDto>>>
    {
    }
}