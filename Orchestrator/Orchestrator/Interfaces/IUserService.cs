using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Models;

namespace Orchestrator.API.Interfaces
{
    public interface IUserService
    {
        public Task CreateAccountService(UserPutModel user);

        public Task<string> LoginAccountService(UserPutModel user);

        public Task<IEnumerable<UserGetModel>> GetAllUsersService();

        public Task<UserGetModel> GetUserByIdService(Guid id);

        public Task DeleteUserService(Guid id);
        
    }
}
