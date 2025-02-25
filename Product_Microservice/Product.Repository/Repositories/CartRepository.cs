using Microsoft.EntityFrameworkCore;
using Product.Data.Entities;
using Product.Data.Interfaces;



namespace Product.Data.Repositories
{
    public class CartRepository : GenericRepository<CartEntity>, ICartRepository
    {
        private readonly DataContext _context;

        public CartRepository(DataContext context) : base(context)
        {
            _context = context;
            CollectionWithIncludes = context.Carts
                .Include(e => e.Products).ThenInclude(e => e.ProductImages);
        }

        public async Task<CartEntity?> GetCartByUser(Guid userId)
        {
            return await CollectionWithIncludes.FirstOrDefaultAsync(entity => entity.UserId == userId);
        }
    }
}
