using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product.Api.ViewModels;
using Product.Service.Interfaces;
using Product.Service.Models;

namespace Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("getAllProducts")]        
        public async Task<IActionResult> GetAllProducts()
        {
            IEnumerable<ProductModel> productModels;
            try
            {
                productModels = await _productService.GetAllProducts();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(productModels);
            return Ok(productViewModels);
        }

        [HttpGet("getProduct")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            ProductModel productModel;
            try
            {
                productModel = await _productService.GetProduct(id);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            var productViewModel = _mapper.Map<ProductModel, ProductViewModel>(productModel);
            return Ok(productViewModel);
        }

        [HttpPost("createProduct")]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductViewModel productViewModel)
        {
            var productModel = _mapper.Map<CreateProductViewModel, ProductModel>(productViewModel);
            try
            {
                await _productService.CreateProduct(productModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("editProduct")]        
        public async Task<IActionResult> EditProduct(ProductViewModel productViewModel)
        {
            var productModel = _mapper.Map<ProductViewModel, ProductModel>(productViewModel);
            try
            {
                await _productService.EditProduct(productModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("changeProductStatus")]
        public async Task<IActionResult> ChangeProductStatus(Guid id)
        {            
           try
           {
               await _productService.ChangeProductStatus(id);
               return Ok();
           }
           catch (Exception e)
           {
               return BadRequest (e.Message);
           }           
            
        }

        [HttpDelete("deleteProduct")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProduct(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}

