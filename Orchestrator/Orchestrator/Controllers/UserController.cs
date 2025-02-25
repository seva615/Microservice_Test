using Google.Apis.Admin.Directory.directory_v1.Data;
using ICSharpCode.Decompiler.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;
using Orchestrator.API.Services;
using Refit;

namespace Orchestrator.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task Register([FromBody] UserPutModel user)
        {
           await _userService.CreateAccountService(user);
        }

        [HttpPost("login")]
        public Task<string> Login([FromBody] UserPutModel user)
        {
             return _userService.LoginAccountService(user);
        }

        [HttpGet("getAll")]
        [JwtAuthorizationFilter("Admin")]
        public async Task<IEnumerable<UserGetModel>> GetAll()
        {
            var AllUsers = await _userService.GetAllUsersService();
            return AllUsers;
        }

        [HttpGet("getUser")]
        public async Task<UserGetModel> GetUserById(Guid id)
        {
            var User = await _userService.GetUserByIdService(id);
            return User;
        }

        [HttpDelete("deleteUser")]
        public async Task DeleteUser(Guid id)
        {
            await _userService.DeleteUserService(id);
        }       
    }
}
