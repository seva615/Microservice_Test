using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Models;

namespace Orchestrator.API.Interfaces
{
    public interface IUserService
    {
        public Task CreateAccountService(UserPutModel user);

        public Task<JwtModel> LoginAccountService(UserPutModel user);

        public Task<IEnumerable<UserGetModel>> GetAllUsersService();

        public Task<UserGetModel> GetUserByEmailService(string email);

        public Task<UserGetModel> GetUserByIdService(Guid id);

        public Task DeleteUserService(Guid id);
        
    }
}
