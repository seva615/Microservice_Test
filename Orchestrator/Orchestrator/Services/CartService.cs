using Microsoft.AspNetCore.Http;
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

        public async Task AddToCart(Guid productId, string jwt, int amount)
        {
            var user = await _userClient.GetUserByJwt(jwt);
            if (user == null)
            {
                throw new Exception();
            }
            await _cartClient.AddToCart(user.Id, productId, amount);
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

        public async Task ClearCart(string jwt)
        {
            var user = await _userClient.GetUserByJwt(jwt);
            if (user == null)
            {
                throw new Exception();
            }
            await _cartClient.ClearCart(user.Id);
        }

        public async Task<CartGetModel> GetCart(string jwt)
        {
            var user = await _userClient.GetUserByJwt(jwt);
            if (user == null)
            {
                throw new Exception();
            }
            var cart = await _cartClient.GetCart(user.Id);
            return cart;
        }
    }
}
