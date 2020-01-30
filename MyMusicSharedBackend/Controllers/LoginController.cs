using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyMusicSharedBackend.Core.Interfaces.UseCases.User;
using MyMusicSharedBackend.Infrastructure.EntityFramework;
using MyMusicSharedBackend.Models;
using MyMusicSharedBackend.Presenters;
using MyMusicSharedBackend.Services;

namespace MyMusicSharedBackend.Controllers
{
    /// <summary>
    /// Controller for login
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginUserUseCase _loginUserUseCase;

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="loginUserUseCase">Use Case to login</param>
        public LoginController(ILoginUserUseCase loginUserUseCase)
        {
            _loginUserUseCase = loginUserUseCase;
        }

        /// <summary>
        /// Logins to the application
        /// </summary>
        /// <param name="login">Login Data</param>
        /// <returns>JWT Token</returns>
        [HttpPost]
        public async Task<ActionResult<Token>> Post([FromBody] Login login)
        {
            LoginUserPresenter presenter = new LoginUserPresenter();

            _ = await _loginUserUseCase.HandleAsync(new Core.Dto.UseCaseRequests.UseCaseRequest<Core.Dto.LoginDto, Core.Dto.UseCaseResponses.UseCaseResponse<Core.Dto.TokenDto>>(new Core.Dto.LoginDto(login.Username, login.Password, login.Scopes, login.RefreshToken)), presenter);

            return presenter.ContentResult;
        }
    }
}