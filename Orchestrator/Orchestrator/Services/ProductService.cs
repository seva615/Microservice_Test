using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Interfaces;
using Orchestrator.API.Models;

namespace Orchestrator.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductClient _productClient;

        public ProductService(IProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<IEnumerable<ProductGetModel>> GetAllProducts()
        {
            var products = await _productClient.GetAllProducts();
            return products;
        }

        public async Task<ProductGetModel> GetProduct(Guid id)
        {
            var product = await _productClient.GetProduct(id);
            return product;
        }

        public async Task AddProduct([FromBody] ProductPutModel productPutModel)
        {
            await _productClient.AddProduct(productPutModel);
        }


        public async Task EditProduct(ProductGetModel productGetModel)
        {
            await _productClient.EditProduct(productGetModel);
        }

        public async Task ChangeProductStatus(Guid id)
        {
            await _productClient.ChangeProductStatus(id);
        }

        public async Task DeleteProduct(Guid id)
        {
            await _productClient.DeleteProduct(id);
        }

    }
}
