using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Services
{
    public class CartService : ICartService
    {
        private readonly ICartClient _cartClient;
        private readonly IUserClient _userClient;

        public CartService(ICartClient cartClient, IUserClient userClient)
        {
            _cartClient = cartClient;
            _userClient = userClient;
        }

        public async Task AddToCart(Guid userId, Guid productId)
        {
            await _cartClient.AddToCart(userId, productId);
        }

        public async Task CreateCart(Guid userId)
        {
            var user = await _userClient.GetUser(userId);
            if (user == null)
            {
                throw new Exception("User doesn't exist");
            }
            await _cartClient.AddCart(userId);
        }

        public async Task DeleteCart(Guid userId)
        {
            await _cartClient.DeleteCart(userId);
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
