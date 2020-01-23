using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseRequests;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using MyMusicSharedBackend.Core.Interfaces;
using MyMusicSharedBackend.Core.Interfaces.UseCases;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.UseCases
{
    /// <summary>
    /// Registering a User use case
    /// </summary>
    internal class RegisterUserUseCase : IRegisterUserUseCase
    {
        /// <summary>
        /// Handles the use case
        /// </summary>
        /// <param name="message">Parameters for the execution</param>
        /// <param name="outputPort">Object that will handle the use case response</param>
        /// <returns>Operation Status</returns>
        public Task<bool> HandleAsync(UseCaseRequest<UserDto, UseCaseResponse<int>> message, IOutputPort<UseCaseResponse<int>> outputPort)
        {
            throw new NotImplementedException();
        }
    }
}