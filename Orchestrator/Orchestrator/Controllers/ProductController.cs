using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

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
        public async Task<IEnumerable<ProductGetModel>> GetAllProducts()
        {
            return await _productService.GetAllProducts();
        }

        [HttpGet("GetProduct")]
        public async Task<ProductGetModel> GetProduct(Guid id)
        {
            return await _productService.GetProduct(id);
        }

        [HttpPost("AddProduct")]
        public async Task AddProduct([FromBody] ProductPutModel productPutModel)
        {
            await _productService.AddProduct(productPutModel);
        }

        [HttpPatch("EditProduct")]
        public async Task EditProduct(ProductGetModel productGetModel)
        {
            await _productService.EditProduct(productGetModel);
        }

        [HttpPatch("ChangeProductStatus")]
        public async Task ChangeProductStatus(Guid productId)
        {
            await _productService.ChangeProductStatus(productId);
        }

        [HttpDelete("DeletProduct")]
        public async Task DeleteProduct(Guid productId)
        {
            await _productService.DeleteProduct(productId);
        }
    }
}

