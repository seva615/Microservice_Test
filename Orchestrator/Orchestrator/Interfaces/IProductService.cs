using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Models;

namespace Orchestrator.API.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductGetModel>> GetAllProducts();

        public Task<ProductGetModel> GetProduct(Guid id);

        public Task AddProduct(ProductPutModel productPutModel);

        public Task EditProduct(ProductGetModel productGetModel);

        public Task ChangeProductStatus(Guid id);

        public Task DeleteProduct(Guid id);
    }
}
