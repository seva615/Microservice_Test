using Microsoft.EntityFrameworkCore;
using Product.Data.Entities;
using Product.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Data.Repositories
{
    public class CartRecordRepository : GenericRepository<CartRecordEntity>, ICartRecordRepository
    {
        private readonly DataContext _context;

        public CartRecordRepository(DataContext context) : base(context)
        {
            _context = context;
            CollectionWithIncludes = context.CartRecords
                .Include(e => e.Product).ThenInclude(e => e.ProductImages);
        }
        public async Task<CartRecordEntity?> GetCartRecordByCartId(Guid cartId)
        {
            return await CollectionWithIncludes.FirstOrDefaultAsync(entity => entity.CartId == cartId);
        }
    }
}
