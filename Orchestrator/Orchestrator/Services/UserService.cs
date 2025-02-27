using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Services
{
    public class UserService : IUserService
    {

        private readonly IUserClient _userClient;
        private readonly ICartClient _cartClient;
        
        public UserService(IUserClient userClient, ICartClient cartClient)
        {
            _userClient = userClient;
            _cartClient = cartClient;
        }


        public async Task CreateAccountService(UserPutModel user)
        {
            await _userClient.Register(user);
            var newUser = await _userClient.GetUserByEmail(user.Email);
            await _cartClient.AddCart(newUser.Id);
        }

        public async Task<JwtModel> LoginAccountService(UserPutModel user)
        {
            return await _userClient.Login(user);
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
            await _cartClient.DeleteCart(id);
        }

        public async Task<UserGetModel> GetUserByEmailService(string email)
        {
            var user = await _userClient.GetUserByEmail(email);
            return user;
        }
    }
}
