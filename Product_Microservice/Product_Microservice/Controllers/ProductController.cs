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
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            var productModels = await _productService.GetAllProducts();
            var productViewModels = _mapper.Map<IEnumerable<ProductViewModel>>(productModels);
            return productViewModels;
        }

        [HttpGet("getProduct")]
        public async Task<ProductViewModel> GetProduct(Guid id)
        {
            ProductModel productModel;
            try
            {
                productModel = await _productService.GetProduct(id);
            }
            catch (Exception e)
            {
                throw new Exception("Product not found");
            }
            var productViewModel = _mapper.Map<ProductModel, ProductViewModel>(productModel);
            return productViewModel;
        }

        [HttpPost("createProduct")]
        public async Task AddProduct([FromBody] CreateProductViewModel productViewModel)
        {
            var productModel = _mapper.Map<CreateProductViewModel, ProductModel>(productViewModel);
            await _productService.CreateProduct(productModel);
        }

        [HttpPatch("editProduct")]        
        public async Task EditProduct(ProductViewModel productViewModel)
        {
            var productModel = _mapper.Map<ProductViewModel, ProductModel>(productViewModel);
            await _productService.EditProduct(productModel);
        }

        [HttpPatch("changeProductStatus")]
        public async Task ChangeProductStatus(Guid id)
        {            
           try
            {
              await _productService.ChangeProductStatus(id);
            }
            catch (Exception e)
            {
                throw new Exception("Product not found");
            }           
            
        }

        [HttpDelete("deleteProduct")]
        public async Task DeleteProduct(Guid id)
        {
            try
            {
                await _productService.DeleteProduct(id);
            }
            catch (Exception e)
            {
                throw new Exception("Product not found");
            }
        }
    }

}

