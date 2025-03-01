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
using Product.Service.Exceptions;

namespace Product.Service.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly ICartRecordService _cartRecordService;

        public CartService(ICartRepository cartRepository, IMapper mapper, IProductRepository productRepository,
            ICartRecordService cartRecordService)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _cartRecordService= cartRecordService;
            
        }
        public async Task DeleteCart(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }
            var cart = await _cartRepository.GetCartByUser(id);

            if (cart == null)
            {
                throw new NotFoundException("Cart not found");
            }
            await _cartRepository.Delete(cart.Id);
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
            else
            {
                throw new Exception("User already has cart");
            } 
        }

        public async Task AddProductToCart(Guid productId, Guid userId, int amount)
        {
            var cartEntity = await _cartRepository.GetCartByUser(userId);
            
            if (cartEntity == null)
            {
                throw new NotFoundException("Cart not found");
            }

            
            var newCartRecord = await _cartRecordService.CreateCartRecord(productId, cartEntity.Id, amount);

            if (newCartRecord == null)
            {
                throw new NotFoundException("Cart recort not found");
            }

            cartEntity.TotalPrice += newCartRecord.Price;
            cartEntity.CartRecords.Add(newCartRecord);
            await _cartRepository.Edit(cartEntity);
        }        

        public async Task ClearCart(Guid userId)
        {
            var cartEntity = await _cartRepository.GetCartByUser(userId);
            if (cartEntity == null)
            {
                throw new NotFoundException("Cart not found");
            }

            cartEntity.TotalPrice = 0;
            cartEntity.CartRecords = new List<CartRecordEntity>();

            await _cartRepository.Edit(cartEntity);
        }

        public async Task<CartModel> GetCart(Guid userId)
        {
            var cartEntity = await _cartRepository.GetCartByUser(userId);
            if (cartEntity == null)
            {
                throw new NotFoundException("Cart not found");
            }

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
