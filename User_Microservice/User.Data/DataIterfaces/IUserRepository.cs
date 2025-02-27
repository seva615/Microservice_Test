using User.Data;
using User.Data.Entities;

namespace User.API.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        Task<UserEntity> GetByEmail(string email);
    }
}
