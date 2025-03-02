using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment.Data.Interface
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);

        Task<TEntity?> GetById(Guid id);

        Task<IEnumerable<TEntity>> GetAll();

        Task Delete(Guid id);

        Task Edit(TEntity entity);
    }
}

