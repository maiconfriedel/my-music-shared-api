using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseRequests;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Interfaces.UseCases
{
    /// <summary>
    /// Interface for registering a User use case
    /// </summary>
    public interface IRegisterUserUseCase : IUseCaseRequestHandler<UseCaseRequest<UserDto, UseCaseResponse<int>>, UseCaseResponse<int>>
    {
    }
}