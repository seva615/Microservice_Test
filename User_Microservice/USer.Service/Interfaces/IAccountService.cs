using User.Service.Models;

namespace User.API.Interfaces
{
    public interface IAccountService
    {
        public Task DeleteAccount(Guid id);

        public Task CreateAccount(UserModel user);

        public UserModel Authorize(UserModel user);      

        public Task<UserModel> GetAccount(Guid id);

        public Task EditAccount(UserModel user);

        public Task<IEnumerable<UserModel>> GetAllAccounts();
    }
}
