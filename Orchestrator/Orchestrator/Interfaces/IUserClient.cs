using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface IUserClient
    {

        [Post("/Account/register")]
        Task Register(UserPutModel user);

        [Post("/Account/login")]
        Task<JwtModel> Login([Body]UserPutModel user);

        [Get("/Account/getUserByJwt")]
        Task<UserGetModel> GetUserByJwt([Header("Authorization")] string token);

        [Get("/Account/getUserByEmail")]
        Task<UserGetModel> GetUserByEmail(string email);

        [Get("/Account/getAccounts")]
        Task<IEnumerable<UserGetModel>> GetAllUsers();

        [Get("/Account/getUser?id={id}")]
        Task<UserGetModel> GetUser(Guid id);

        [Delete("/Account/deleteUser?id={id}")]
        Task DeleteUser(Guid id);
    }
}
