using Microsoft.Extensions.Configuration;
using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseRequests;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using MyMusicSharedBackend.Core.Interfaces;
using MyMusicSharedBackend.Core.Interfaces.Gateways;
using MyMusicSharedBackend.Core.Interfaces.UseCases.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.UseCases.User
{
    /// <summary>
    /// Registering a User use case
    /// </summary>
    internal class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="userRepository">User repository</param>
        public RegisterUserUseCase(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Handles the use case
        /// </summary>
        /// <param name="message">Parameters for the execution</param>
        /// <param name="outputPort">Object that will handle the use case response</param>
        /// <returns>Operation Status</returns>
        public async Task<bool> HandleAsync(UseCaseRequest<UserDto, UseCaseResponse<int>> message, IOutputPort<UseCaseResponse<int>> outputPort)
        {
            //TODO - Validate user by username and email to see if already exists

            // creates a new User Domain instance
            Domain.User userCreate = new Domain.User(0, message.RequestContent.Email, message.RequestContent.Username, message.RequestContent.Password, message.RequestContent.FullName, message.RequestContent.Bio);

            // hashes the password
            userCreate.Password = userCreate.HashPassword(userCreate.ToString(), _configuration.GetSection("Security").GetSection("PasswordHashSalt").Value);

            // creates in the database and returns the result
            var createdUser = await _userRepository.CreateUserAsync(userCreate);

            if (createdUser.Success)
            {
                outputPort.Handle(new UseCaseResponse<int>(createdUser.Result));
                return true;
            }
            else
            {
                outputPort.Handle(new UseCaseResponse<int>(createdUser.Message, createdUser.Errors));
                return false;
            }
        }
    }
}