using AutoMapper;
using Product.Data;
using Product.Data.Entities;
using Product.Data.Interfaces;
using Product.Data.Repositories;
using Product.Service.Exceptions;
using Product.Service.Interfaces;
using Product.Service.Models;



namespace Product.Service.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;   

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;          
        }
        public async Task DeleteProduct(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            await _productRepository.Delete(id);
        }

        public async Task ChangeProductStatus(Guid id)
        {         

            var productEntity = await _productRepository.GetById(id);
            if (productEntity == null)
            {
                throw new NotFoundException("Product not found");
            }

            if (productEntity.Status != ProductStatus.Stasuses.Ok)
            {
                productEntity.Status = ProductStatus.Stasuses.Ok;
            } 
            else productEntity.Status = ProductStatus.Stasuses.OutOfStock;            

            await _productRepository.Edit(productEntity);
        }

        public async Task CreateProduct(ProductModel product)
        {
            if (product == null)
            {
                throw new ArgumentNullException();
            }
            product.Status = ProductStatus.Stasuses.Ok;
            var productEntity = _mapper.Map<ProductModel, ProductEntity>(product);
            await _productRepository.Add(productEntity);
        }

        public async Task<ProductModel> GetProduct(Guid id)
        {
            var productEntity = await _productRepository.GetById(id);
            if (productEntity == null)
            {
                throw new NotFoundException("Product not found");
            }
            var productModel = _mapper.Map<ProductEntity, ProductModel>(productEntity);
            return productModel;
        }

        public async Task EditProduct(ProductModel product)
        {
            var productEntity = _mapper.Map<ProductModel, ProductEntity>(product);
            await _productRepository.Edit(productEntity);
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            var productEntities = await _productRepository.GetAll();
            var productModels = _mapper.Map<IEnumerable<ProductModel>>(productEntities);
            return productModels;
        }


    }
}
