using Product.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Interfaces
{
    public interface ICartRecordRepository : IGenericRepository<CartRecordEntity>
    {
        public Task<CartRecordEntity?> GetCartRecordByCartId(Guid cartId);
    }
}
