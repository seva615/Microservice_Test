using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task Register([FromBody] CreateUserViewModel user)
        {
            UserModel userModel = _mapper.Map<CreateUserViewModel, UserModel>(user);
            await _accountService.CreateAccount(userModel);
        }


        [HttpPost("login")]        
        public IActionResult Login([FromBody] CreateUserViewModel loginModel)
        {
            UserModel userModel = _mapper.Map<CreateUserViewModel, UserModel>(loginModel);

            try
            {
                userModel = _accountService.Authorize(userModel);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            User.Claims.FirstOrDefault(x => x.Type == "");

            UserViewModel userViewModel = _mapper.Map<UserModel, UserViewModel>(userModel);

            if (userViewModel != null)
            {
                var token = _jwtGenerator.GenerateJwtToken(userViewModel);

                return Ok(new
                {
                    access_token = token,
                    role = userViewModel.Role,
                    id = userViewModel.Id,
                    user_name = userViewModel.Email,
                });
            }

            return BadRequest();
        }


        [HttpGet("getUserByJwt")]
        [Authorize]
        public async Task<UserViewModel> GetUserByJwt()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            Guid id = Guid.Parse(userId);
            var userModel = await _accountService.GetAccount(id); 
            var userViewModel = _mapper.Map<UserModel, UserViewModel>(userModel);
            return userViewModel;
        }


        [HttpGet("getAccounts")]            
        public async Task<IEnumerable<UserViewModel>> GetAllAccounts()
        {
            var UserModels = await _accountService.GetAllAccounts();
            var UserViewModels = _mapper.Map<IEnumerable<UserViewModel>>(UserModels);
            return UserViewModels;
        }

        [HttpGet("getUser")]        
        public async Task<UserViewModel> GetUser(Guid id)
        {
            var UserModel = await _accountService.GetAccount(id);
            var UserViewModel = _mapper.Map<UserModel, UserViewModel>(UserModel);
            return UserViewModel;
        }

        [HttpDelete("deleteUser")]               
        public async Task DeleteUser(Guid id)
        {
            await _accountService.DeleteAccount(id);
        }
    }
}
