using Comment.Data.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment.Data.Repositories
{
    public class GenericRepository<TEntity>(DataContext context) : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

        protected IQueryable<TEntity> CollectionWithIncludes { get; set; }

        public async Task<IEnumerable<TEntity>> GetAll()
        {

            return await CollectionWithIncludes.ToListAsync();
        }

        public Task<TEntity?> GetById(Guid id)
        {
            return CollectionWithIncludes.FirstOrDefaultAsync(entity => id == entity.Id);
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
        }
        public async Task Edit(TEntity entity)
        {
            _dbSet.Update(entity);
            await context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Object not found");
            }
            _dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
