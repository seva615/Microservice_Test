using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Services
{
    public class CartService : ICartService
    {
        private readonly ICartClient _cartClient;

        public CartService(ICartClient cartClient)
        {
            _cartClient = cartClient;
        }

        public async Task AddToCart(Guid userId, Guid productId)
        {
            await _cartClient.AddToCart(userId, productId);
        }

        public async Task CreateCart(Guid userId)
        {
            await _cartClient.AddCart(userId);
        }

        public async Task DeleteCart(Guid cartId)
        {
            await _cartClient.DeleteCart(cartId);
        }

        public async Task<IEnumerable<CartGetModel>> GetAllCarts()
        {
            var carts = await _cartClient.GetAllCarts();
            return carts;
        }

        public async Task<CartGetModel> GetCart(Guid cartId)
        {
            var cart = await _cartClient.GetCart(cartId);
            return cart;
        }
    }
}
