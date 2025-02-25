using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DataContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected IQueryable<TEntity> CollectionWithIncludes { get; set; }

        public GenericRepository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await CollectionWithIncludes.ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await CollectionWithIncludes.FirstOrDefaultAsync(entity => id == entity.Id);
        }

        public async Task<TEntity> GetByName(string name)
        {
            return await CollectionWithIncludes.FirstOrDefaultAsync(entity => name == entity);
        }

        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Edit(TEntity entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = (Microsoft.EntityFrameworkCore.EntityState)EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Guid id)
        {
            var entity = await _dbSet.FindAsync(id);
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }


    }
}
