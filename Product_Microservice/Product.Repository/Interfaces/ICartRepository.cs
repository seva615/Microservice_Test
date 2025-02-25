using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Interfaces
{
    public interface ICartRepository : IGenericRepository<CartEntity>
    {
        public Task<CartEntity?> GetCartByUser(Guid userId);
    }
}
