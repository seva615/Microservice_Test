using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using User.API.ControllerModels;
using User.API.Interfaces;
using User.API.JWT;
using User.API.ViewModels;
using User.Service.Models;

namespace User.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public AccountController(IAccountService accountService, IMapper mapper, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _accountService = accountService;
            _jwtGenerator = jwtGenerator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserViewModel user)
        {
            if (user == null)
            {
                return BadRequest("User model is empty");
            }

            UserModel userModel = _mapper.Map<CreateUserViewModel, UserModel>(user);

            try
            {
                await _accountService.CreateAccount(userModel);
                return Ok();
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }


        [HttpPost("login")]        
        public async Task<IActionResult> Login([FromBody] CreateUserViewModel loginModel)
        {
            UserModel userModel = _mapper.Map<CreateUserViewModel, UserModel>(loginModel);

            try
            {
                userModel = await _accountService.Authorize(userModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            User.Claims.FirstOrDefault(x => x.Type == "");

            UserViewModel userViewModel = _mapper.Map<UserModel, UserViewModel>(userModel);

            if (userViewModel != null)
            {
                var token = _jwtGenerator.GenerateJwtToken(userViewModel);
                JwtModel jwt = new JwtModel
                {
                    AccessToken = token,
                    Role = userViewModel.Role,
                    Id = userViewModel.Id,
                    Email = userViewModel.Email,
                };

                return Ok(jwt);
            }

            return BadRequest();
        }

        [HttpPatch("editAccount")]
        public async Task<IActionResult> EditAccount(EditUserViewModel userViewModel)
        {
            if (userViewModel == null)
            {
                return BadRequest();
            }
            var userModel = _mapper.Map<EditUserViewModel, UserModel>(userViewModel);
            try
            {
                await _accountService.EditAccount(userModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("getUserByJwt")]
        //[Authorize]
        public async Task<IActionResult> GetUserByJwt()
        {
           
            UserModel userModel;
            try
            {
                var userId = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                Guid id = Guid.Parse(userId);
                userModel = await _accountService.GetAccount(id);
            } catch (Exception ex) 
            {
                return BadRequest(ex);
            }
            var userViewModel = _mapper.Map<UserModel, UserViewModel>(userModel);
            return Ok(userViewModel);
        }


        [HttpGet("getAccounts")]            
        public async Task<IActionResult> GetAllAccounts()
        {
            IEnumerable<UserModel> userModels;
            try
            {
                userModels = await _accountService.GetAllAccounts();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }            
            var userViewModels = _mapper.Map<IEnumerable<UserViewModel>>(userModels);
            return Ok(userViewModels);
        }

        [HttpGet("getUser")]        
        public async Task<IActionResult> GetUser(Guid id)
        {
           
            UserModel userModel;
            try
            {
                userModel = await _accountService.GetAccount(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var userViewModel = _mapper.Map<UserModel, UserViewModel>(userModel);
            return Ok(userViewModel);
        }

        [HttpGet("getUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {

            UserModel userModel;
            try
            {
                userModel = await _accountService.GetUserByEmail(email);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var userViewModel = _mapper.Map<UserModel, UserViewModel>(userModel);
            return Ok(userViewModel);
        }
        [HttpDelete("deleteUser")]               
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _accountService.DeleteAccount(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
