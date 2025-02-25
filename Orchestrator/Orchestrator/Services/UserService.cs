using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Services
{
    public class UserService : IUserService
    {

        private readonly IUserClient _userClient;

        public UserService(IUserClient userClient)
        {
            _userClient = userClient;
        }


        public async Task CreateAccountService(UserPutModel user)
        {
            await _userClient.Register(user);
        }

        public Task<string> LoginAccountService(UserPutModel user)
        {
            return _userClient.Login(user);
        }

        public async Task<IEnumerable<UserGetModel>> GetAllUsersService()
        {
            var Users = await _userClient.GetAllUsers();
            return Users;
        }
             
        public async Task<UserGetModel> GetUserByIdService(Guid id)
        {
            var User = await _userClient.GetUser(id);
            return User;
        }
           
        public async Task DeleteUserService(Guid id)
        {
            await _userClient.DeleteUser(id);
        }
    }
}
