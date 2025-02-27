using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.API.Data;
using User.API.Interfaces;
using User.Data.Entities;

namespace User.Data.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
            Collection = context.Users;
        }

        public async Task<UserEntity> GetByEmail(string email)
        {
            return await Collection.FirstOrDefaultAsync(entity => entity.Email == email);
        }
    }
}
