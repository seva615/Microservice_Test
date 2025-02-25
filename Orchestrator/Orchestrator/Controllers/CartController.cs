using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("getAllcarts")]
        public async Task<IEnumerable<CartGetModel>> GetAllCarts()
        {
            var carts = await _cartService.GetAllCarts();
            return carts;
        }

        [HttpGet("getCart")]
        public async Task<CartGetModel> GetCart(Guid cartId)
        {
            var cart = await _cartService.GetCart(cartId);
            return cart;
        }

        [HttpPost("createCart")]
        public async Task CreateCart(Guid userId)
        {
            await _cartService.CreateCart(userId);
        }

        [HttpGet("addProductToCart")]
        public async Task AddProductToCart(Guid cartId, Guid productId)
        {
            await _cartService.AddToCart(cartId, productId);
        }

        [HttpDelete("deleteCart")]
        public async Task DeleteCart(Guid cartId)
        {
            await _cartService.DeleteCart(cartId);
        }
    }
}
