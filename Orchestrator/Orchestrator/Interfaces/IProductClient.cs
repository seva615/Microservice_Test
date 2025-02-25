using Microsoft.AspNetCore.Mvc;
using Orchestrator.API.Models;
using Refit;

namespace Orchestrator.API.Interfaces
{
    public interface IProductClient
    {
        [Get("/Product/getAllProducts")]
        Task<IEnumerable<ProductGetModel>> GetAllProducts();

        [Get("/Product/getProduct")]
        Task<ProductGetModel> GetProduct(Guid id);

        [Post("/Product/createProduct")]
        Task AddProduct([FromBody] ProductPutModel productPutModel);

        [Patch("/Product/editProduct")]
        Task EditProduct(ProductGetModel productGetModel);

        [Patch("/Product/changeProductStatus")]
        Task ChangeProductStatus(Guid id);

        [Delete("/Product/deleteProduct")]
        Task DeleteProduct(Guid id);
    }
}
