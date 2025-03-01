using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Api.ViewModels;
using Product.Service.Interfaces;
using Product.Service.Models;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(ICartService cartService, IMapper mapper)
        {
            _mapper = mapper;
            _cartService = cartService;
        }

        [HttpGet("getAllCarts")]
        public async Task<IActionResult> GetAllCarts()
        {
            IEnumerable<CartModel> cartModels;
            try
            {
                cartModels = await _cartService.GetAllCarts();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var cartViewModels = _mapper.Map<IEnumerable<CartViewModel>>(cartModels);
            return Ok(cartViewModels);
        }

        [HttpGet("getCart")]
        public async Task<IActionResult> GetCart(Guid userId)
        {
            CartModel cartModel;
            try
            {
                cartModel = await _cartService.GetCart(userId);               
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            var cartViewModel = _mapper.Map<CartModel, CartViewModel>(cartModel);
            return Ok(cartViewModel);
        }

        [HttpGet("addToCart")]
        public async Task<IActionResult> AddToCart(Guid userId, Guid productId, int amount)
        {
            try
            {
                await _cartService.AddProductToCart(productId, userId, amount);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("createCart")]
        public async Task<IActionResult> CreateCart(Guid userId)
        {
            if(userId == Guid.Empty)
            {
                throw new ArgumentNullException("userId");
            }
            try
            {
                await _cartService.CreateCart(userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("editCart")]
        public async Task<IActionResult> EditCart(CartViewModel cartViewModel)
        {
            var cartModel = _mapper.Map<CartViewModel, CartModel>(cartViewModel);
            try
            {
                await _cartService.EditCart(cartModel);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("clearCart")]
        public async Task<IActionResult> ClearCart(Guid userId)
        {
            try
            {
                await _cartService.ClearCart(userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("deleteCart")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _cartService.DeleteCart(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
