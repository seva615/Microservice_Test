using Google.Apis.Admin.Directory.directory_v1.Data;
using ICSharpCode.Decompiler.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Orchestrator.API.Exceptions;
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
        public async Task<IActionResult> Register([FromBody] UserPutModel user)
        {
            try
            {
                await _userService.CreateAccountService(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserPutModel user)
        {
            try
            {
                var jwt = await _userService.LoginAccountService(user);
                return Ok(jwt);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
             
        }

        [HttpGet("getAll")]
        [JwtAuthorizationFilter("Admin")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var AllUsers = await _userService.GetAllUsersService();
                return Ok(AllUsers);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userService.GetUserByEmailService(email);
                return Ok(user);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getUser")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserByIdService(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUserService(id);
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }       
    }
}
