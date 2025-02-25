using Product.Data.Entities;
using Product.Data;
using Product.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Product.Data.Interfaces;
using Product.Service.Interfaces;
using Product.Data.Migrations;

namespace Product.Service.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
        }
        public async Task DeleteCart(Guid id)
        {
            if (id == null)
            {
                throw new ArgumentNullException();
            }

            await _cartRepository.Delete(id);
        }

        public async Task CreateCart(Guid userId)
        {
            var cart = await _cartRepository.GetCartByUser(userId);
            if (cart == null)
            {
                CartEntity cartEntity = new CartEntity()
                {
                    UserId = userId,
                    TotalPrice = 0
                };                
                await _cartRepository.Add(cartEntity);
            }
        }

        public async Task AddProductToCart(Guid productId, Guid userId)
        {
            var cartEntity = await _cartRepository.GetCartByUser(userId);
            var productEntity = await _productRepository.GetById(productId);

            if (cartEntity == null)
            {
                throw new Exception("Cart not found");
            }

            if (productEntity != null)
            {
                cartEntity.Products.Add(productEntity);
                cartEntity.TotalPrice += productEntity.Price;
            }
            await _cartRepository.Edit(cartEntity);
            
        }        


        public async Task<CartModel> GetCart(Guid id)
        {
            var cartEntity = await _cartRepository.GetById(id);
            var cartModel = _mapper.Map<CartEntity, CartModel>(cartEntity);
            return cartModel;
        }

        public async Task EditCart(CartModel cart)
        {
            var cartEntity = _mapper.Map<CartModel, CartEntity>(cart);
            await _cartRepository.Edit(cartEntity);
        }

        public async Task<IEnumerable<CartModel>> GetAllCarts()
        {
            var cartEntities = await _cartRepository.GetAll();
            var cartModels = _mapper.Map<IEnumerable<CartModel>>(cartEntities);
            return cartModels;
        }
    }
}
