using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseRequests;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using MyMusicSharedBackend.Core.Interfaces;
using MyMusicSharedBackend.Core.Interfaces.Gateways;
using MyMusicSharedBackend.Core.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.UseCases.User
{
    /// <summary>
    /// Use case for search all users use case
    /// </summary>
    internal class SearchUsersUseCase : ISearchUsersUseCase
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="userRepository">User repository for Users data</param>
        public SearchUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Handles the use case
        /// </summary>
        /// <param name="message">Parameters for the execution</param>
        /// <param name="outputPort">Object that will handle the use case response</param>
        /// <returns>Operation Status</returns>
        public async Task<bool> HandleAsync(UseCaseRequest<bool, UseCaseResponse<IEnumerable<UserDto>>> message, IOutputPort<UseCaseResponse<IEnumerable<UserDto>>> outputPort)
        {
            var response = await _userRepository.SearchUsers();

            if (response.Success)
            {
                outputPort.Handle(new UseCaseResponse<IEnumerable<UserDto>>(response.Result.Select(a => new UserDto(a.Id, a.Email, a.Username, a.Password, a.FullName, a.Bio)).ToArray()));
                return true;
            }
            else
            {
                outputPort.Handle(new UseCaseResponse<IEnumerable<UserDto>>(response.Message, response.Errors));
                return false;
            }
        }
    }
}