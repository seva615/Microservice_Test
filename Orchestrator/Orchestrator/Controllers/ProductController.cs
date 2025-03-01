using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;
using Orchestrator.API.Services;

namespace Orchestrator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProducts();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            try
            {
                var product = await _productService.GetProduct(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
             
        }

        [HttpPost("AddProduct")]
        [JwtAuthorizationFilter("Admin")]
        public async Task<IActionResult> AddProduct([FromBody] ProductPutModel productPutModel)
        {
            try
            {
                await _productService.AddProduct(productPutModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("EditProduct")]
        [JwtAuthorizationFilter("Admin")]
        public async Task<IActionResult> EditProduct(ProductGetModel productGetModel)
        {
            try
            {
                await _productService.EditProduct(productGetModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPatch("ChangeProductStatus")]
        [JwtAuthorizationFilter("Admin")]
        public async Task<IActionResult> ChangeProductStatus(Guid productId)
        {
            try
            {
                await _productService.ChangeProductStatus(productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeletProduct")]
        [JwtAuthorizationFilter("Admin")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            try
            {
                await _productService.DeleteProduct(productId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            }
        }
}

