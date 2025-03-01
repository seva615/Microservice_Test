using AutoMapper;
using Product.Data;
using Product.Data.Entities;
using Product.Data.Interfaces;
using Product.Data.Repositories;
using Product.Service.Exceptions;
using Product.Service.Interfaces;
using Product.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Service.Services
{
    public class CartRecordService : ICartRecordService
    {
        private readonly ICartRecordRepository _cartRecordRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CartRecordService(ICartRecordRepository cartRecordRepository, IMapper mapper, IProductRepository productRepository)
        {
            _cartRecordRepository = cartRecordRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<CartRecordEntity> CreateCartRecord(Guid productId, Guid cartId, int amount)
        {
            if (productId == Guid.Empty )
            {
                throw new ArgumentNullException();
            }

            if (amount < 1)
            {
                throw new Exception("Product amount can't be negative");
            }

            var productEntity = await _productRepository.GetById(productId);

            if(productEntity == null)
            {
                throw new NotFoundException("Product not found");
            }

            if (productEntity.Status == ProductStatus.Stasuses.OutOfStock)
            {
                throw new InvalidOperationException("Product is out of stock");
            }

            int price = productEntity.Price * amount;

            CartRecordEntity newCartRecord = new CartRecordEntity
            {
                Product = productEntity,
                ProductAmount = amount,
                Price = price,
                CartId = cartId
            };
            await _cartRecordRepository.Add(newCartRecord);
            return newCartRecord;
        }



        public async Task DeleteCartRecord(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new ArgumentNullException();
            }
            await _cartRecordRepository.Delete(productId);
        }

        public async Task EditCartRecord(CartRecordModel cartRecordModel)
        {
            if (cartRecordModel == null)
            {
                throw new ArgumentNullException();
            }
            var cartRecordEntity = _mapper.Map<CartRecordModel, CartRecordEntity>(cartRecordModel);
            await _cartRecordRepository.Edit(cartRecordEntity);
        }

        public async Task<IEnumerable<CartRecordModel>> GetAllCartRecords()
        {
            var cartRecordEntities = await _cartRecordRepository.GetAll();
            var cartRecordModels = _mapper.Map<IEnumerable<CartRecordModel>>(cartRecordEntities);
            return cartRecordModels;
        }

        public async Task<CartRecordModel> GetCartRecord(Guid cartRecordId)
        {
            if(cartRecordId == Guid.Empty)
            { 
                throw new ArgumentNullException(); 
            }
            var cartRecordEntiy = await _cartRecordRepository.GetById(cartRecordId);
            if (cartRecordEntiy == null)
            {
                throw new DirectoryNotFoundException("Cart record not found");
            }
            var cartRecordModel = _mapper.Map<CartRecordEntity, CartRecordModel>(cartRecordEntiy);
            return cartRecordModel;
        }

        public async Task<CartRecordEntity?> GetCartRecordByCartId(Guid cartId)
        {
            if (cartId == Guid.Empty)
            {
                throw new ArgumentNullException();
            }
            return await _cartRecordRepository.GetCartRecordByCartId(cartId);
        }
    }
}
