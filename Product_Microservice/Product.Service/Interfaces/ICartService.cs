using Product.Data.Entities;
using Product.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Interfaces
{
    public interface ICartService
    {
        public Task DeleteCart(Guid id);

        public Task CreateCart(Guid userId);

        public Task ClearCart(Guid userId);

        public Task<CartModel> GetCart(Guid id);

        public Task EditCart(CartModel cart);

        public Task<IEnumerable<CartModel>> GetAllCarts();

        public Task AddProductToCart(Guid productId, Guid userId, int amount);
        
    }
}
