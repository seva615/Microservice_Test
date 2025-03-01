using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;
using Orchestrator.API.Services;

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
        public async Task<IActionResult> GetAllCarts()
        {
            try
            {
                var carts = await _cartService.GetAllCarts();
                return Ok(carts);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("clearCart")]
        [JwtAuthorizationFilter()]
        public async Task<IActionResult> ClearCart()
        {
            var httpContext = HttpContext;
            string jwt = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            try
            {
                await _cartService.ClearCart(jwt);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("getCart")]
        [JwtAuthorizationFilter()]
        public async Task<IActionResult> GetCart()
        {
            var httpContext = HttpContext;
            string jwt = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            try
            {
                var cart = await _cartService.GetCart(jwt);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("createCart")]
        public async Task<IActionResult> CreateCart(Guid userId)
        {
            try
            {
                await _cartService.CreateCart(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("addProductToCart")]
        [JwtAuthorizationFilter()]
        public async Task<IActionResult> AddProductToCart(Guid productId, int amount)
        {
            var httpContext = HttpContext;
            string jwt = httpContext.Request.Headers["Authorization"].FirstOrDefault();
            try
            {
                await _cartService.AddToCart(productId, jwt, amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("deleteCart")]
        public async Task<IActionResult> DeleteCart(Guid cartId)
        {
            try
            {
                await _cartService.DeleteCart(cartId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
