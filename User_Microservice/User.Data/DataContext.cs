using Microsoft.EntityFrameworkCore;
using User.Data.Entities;

namespace User.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}

    


