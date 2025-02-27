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
        public async Task<IEnumerable<CartViewModel>> GetAllCarts()
        {
            var cartModels = await _cartService.GetAllCarts();
            var cartViewModels = _mapper.Map<IEnumerable<CartViewModel>>(cartModels);
            return cartViewModels;
        }

        [HttpGet("getCart")]
        public async Task<CartViewModel> GetCart(Guid id)
        {
            CartModel cartModel;
            try
            {
                cartModel = await _cartService.GetCart(id);
            }
            catch (Exception e)
            {
                throw new Exception("Cart not found");
            }
            var cartViewModel = _mapper.Map<CartModel, CartViewModel>(cartModel);
            return cartViewModel;
        }

        [HttpGet("addToCart")]
        public async Task AddToCart(Guid userId,Guid productId)
        {
            await _cartService.AddProductToCart(productId, userId);
        }

        [HttpPost("createCart")]
        public async Task CreateCart(Guid userId)
        {
            await _cartService.CreateCart(userId);
        }

        [HttpPatch("editCart")]
        public async Task EditCart(CartViewModel cartViewModel)
        {
            var cartModel = _mapper.Map<CartViewModel, CartModel>(cartViewModel);
            await _cartService.EditCart(cartModel);
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
