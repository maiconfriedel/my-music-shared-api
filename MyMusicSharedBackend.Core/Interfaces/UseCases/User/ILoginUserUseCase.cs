using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseRequests;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyMusicSharedBackend.Core.Interfaces.UseCases.User
{
    /// <summary>
    /// Interface for login in the application
    /// </summary>
    public interface ILoginUserUseCase : IUseCaseRequestHandler<UseCaseRequest<LoginDto, UseCaseResponse<TokenDto>>, UseCaseResponse<TokenDto>>
    {
    }
}