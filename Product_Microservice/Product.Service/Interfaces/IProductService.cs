

using Product.Service.Models;

namespace Product.Service.Interfaces
{
    public interface IProductService
    {
        public Task DeleteProduct(Guid id);

        public Task CreateProduct(ProductModel product);
               
        public Task<ProductModel> GetProduct(Guid id);

        public Task EditProduct(ProductModel product);

        public Task<IEnumerable<ProductModel>> GetAllProducts();

        public Task ChangeProductStatus(Guid id);
    }
}
