﻿using Microsoft.Extensions.Configuration;
using MyMusicSharedBackend.Core.Dto;
using MyMusicSharedBackend.Core.Dto.UseCaseRequests;
using MyMusicSharedBackend.Core.Dto.UseCaseResponses;
using MyMusicSharedBackend.Core.Interfaces;
using MyMusicSharedBackend.Core.Interfaces.Gateways;
using MyMusicSharedBackend.Core.Interfaces.UseCases.User;
using MyMusicSharedBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMusicSharedBackend.Core.UseCases.User
{
    /// <summary>
    /// Interface for login in the application
    /// </summary>
    internal class LoginUserUseCase : ILoginUserUseCase
    {
        private readonly TokenService _tokenService;

        private readonly IUserRepository _userRepository;

        private readonly ITokenRepository _tokenRepository;

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="tokenService"></param>
        /// <param name="userRepository"></param>
        /// <param name="configuration"></param>
        public LoginUserUseCase(TokenService tokenService, IUserRepository userRepository, IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _tokenService = tokenService;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _configuration = configuration;
        }

        /// <summary>
        /// Handles the use case
        /// </summary>
        /// <param name="message">Parameters for the execution</param>
        /// <param name="outputPort">Object that will handle the use case response</param>
        /// <returns>Operation Status</returns>
        public async Task<bool> HandleAsync(UseCaseRequest<LoginDto, UseCaseResponse<TokenDto>> message, IOutputPort<UseCaseResponse<TokenDto>> outputPort)
        {
            if (message.RequestContent.RefreshToken == null)
            {
                if (message.RequestContent.Scopes == null || message.RequestContent.Scopes.Count() == 0)
                {
                    outputPort.Handle(new UseCaseResponse<TokenDto>("Invalid login.", new List<string> { "When authenticating with user and password, must send scopes." }));
                    return false;
                }

                var user = await _userRepository.GetUserByUsername(message.RequestContent.Username);

                if (user.Result == null)
                {
                    outputPort.Handle(new UseCaseResponse<TokenDto>("Invalid login.", new List<string> { "Invalid login." }));
                    return false;
                }

                if (!user.Result.ValidatePassword(message.RequestContent.ToString(), _configuration.GetSection("Security").GetSection("PasswordHashSalt").Value, user.Result.Password))
                {
                    outputPort.Handle(new UseCaseResponse<TokenDto>("Invalid login.", new List<string> { "Invalid login." }));
                    return false;
                }

                var token = _tokenService.GenerateToken(user.Result, message.RequestContent.Scopes);

                var refreshToken = await _tokenRepository.CreateRefreshToken(user.Result.Username, message.RequestContent.Scopes);

                outputPort.Handle(new UseCaseResponse<TokenDto>(new TokenDto("Bearer", token, refreshToken.Result)));
                return true;
            }
            else
            {
                var refreshToken = await _tokenRepository.SearchRefreshToken(message.RequestContent.RefreshToken);

                if (refreshToken.Result == null || !refreshToken.Result.IsValid)
                {
                    await _tokenRepository.DeleteRefreshToken(message.RequestContent.RefreshToken);

                    outputPort.Handle(new UseCaseResponse<TokenDto>("Invalid login.", new List<string> { "Invalid refresh token." }));
                    return false;
                }

                var user = await _userRepository.GetUserByUsername(refreshToken.Result.Username);

                if (user.Result == null)
                {
                    outputPort.Handle(new UseCaseResponse<TokenDto>("Invalid login.", new List<string> { "Invalid login." }));
                    return false;
                }

                var token = _tokenService.GenerateToken(user.Result, refreshToken.Result.Scopes.Split("+"));

                var newRefreshToken = await _tokenRepository.CreateRefreshToken(user.Result.Username, refreshToken.Result.Scopes.Split("+"));

                await _tokenRepository.DeleteRefreshToken(message.RequestContent.RefreshToken);

                outputPort.Handle(new UseCaseResponse<TokenDto>(new TokenDto("Bearer", token, newRefreshToken.Result)));
                return true;
            }
        }
    }
}