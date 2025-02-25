using User.Data;
using User.Data.Entities;

namespace User.API.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity>
    {
        UserEntity GetByEmail(string email);
    }
}
