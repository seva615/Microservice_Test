using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.Api.ViewModels;
using Product.Service.Interfaces;
using Product.Service.Models;
using Product.Service.Services;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartRecordController : ControllerBase
    {
        private readonly ICartRecordService _cartRecordService;
        private readonly IMapper _mapper;

        public CartRecordController(ICartRecordService cartRecordService, IMapper mapper)
        {
            _cartRecordService = cartRecordService;
            _mapper = mapper;
        }

        [HttpGet("getAllCartRecords")]
        public async Task<IActionResult> GetAllCartRecords()
        {
            IEnumerable<CartRecordModel> cartRecordModels;
            try
            {
                cartRecordModels = await _cartRecordService.GetAllCartRecords();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var cartRecordViewModels = _mapper.Map<IEnumerable<CartRecordViewModel>>(cartRecordModels);
            return Ok(cartRecordViewModels);
        }

        [HttpPost("createCartRecord")]
        public async Task<IActionResult> CreateCartRecord(Guid productId, Guid cartId, int amount)
        {
            try
            {
                await _cartRecordService.CreateCartRecord(productId, cartId, amount);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("deleteCartRecord")]
        public async Task<IActionResult> GetCartRecords(Guid recordId)
        {
            CartRecordModel cartRecordModel;
            try
            {
                cartRecordModel = await _cartRecordService.GetCartRecord(recordId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            var cartRecordViewModel = _mapper.Map<CartRecordModel, CartRecordViewModel>(cartRecordModel);
            return Ok(cartRecordViewModel);
        }

        [HttpDelete("deleteCartRecord")]
        public async Task<IActionResult> DeleteCartRecords(Guid recordId)
        {
            try
            {
                await _cartRecordService.DeleteCartRecord(recordId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
