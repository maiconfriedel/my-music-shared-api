using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MyMusicSharedBackend.Models;
using System.Linq;
using MyMusicSharedBackend.Infrastructure.EntityFramework;
using MyMusicSharedBackend.Core.Interfaces.UseCases;
using MyMusicSharedBackend.Presenters;

namespace MyMusicSharedBackend.Controllers
{
    /// <summary>
    /// Users Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        /// <summary>
        /// Register user use case
        /// </summary>
        private readonly IRegisterUserUseCase _registerUserUseCase;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="registerUserUseCase">Register user use case</param>
        public UsersController(IRegisterUserUseCase registerUserUseCase)
        {
            _registerUserUseCase = registerUserUseCase;
        }

        ///// <summary>
        ///// Get all users
        ///// </summary>
        ///// <returns>List of all users</returns>
        //[HttpGet]
        //[Authorize(policy: "users.read")]
        //public IEnumerable<object> GetUsers()
        //{
        //    return _context.Users.ToList().Select(a => new { a.Id, a.Email, a.Username });
        //}

        ///// <summary>
        ///// Get user by Id
        ///// </summary>
        ///// <param name="id">User identifier</param>
        ///// <returns>Details of an user</returns>
        //[HttpGet("{id}")]
        //[Authorize(policy: "users.read")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return user;
        //}

        /// <summary>
        /// Create a user
        /// </summary>
        /// <param name="user">User data</param>
        /// <returns>The created user Id</returns>
        [HttpPost]
        [Authorize(policy: "users.read")]
        public async Task<ActionResult<int>> PostUser(User user)
        {
            PostPresenter<int> postPresenter = new PostPresenter<int>();

            var userLoggedIn = User;

            _ = await _registerUserUseCase.HandleAsync(new Core.Dto.UseCaseRequests.UseCaseRequest<Core.Dto.UserDto, Core.Dto.UseCaseResponses.UseCaseResponse<int>>(new Core.Dto.UserDto(user.Email, user.Username, user.Password, user.FullName, user.Bio)), postPresenter).ConfigureAwait(false);

            return postPresenter.ContentResult;
        }
    }
}