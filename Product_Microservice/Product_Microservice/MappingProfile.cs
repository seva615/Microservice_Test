using AutoMapper;
using Product.Api.ViewModels;
using Product.Data.Entities;
using Product.Service.Models;

namespace Product.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ProductEntity, ProductModel>();
            CreateMap<ProductModel, ProductEntity>();
            CreateMap<ImageEntity, ImageModel>();
            CreateMap<ImageModel, ImageEntity>();
            CreateMap<ProductViewModel, ProductModel>();
            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<ImageViewModel, ImageModel>();
            CreateMap<ImageModel, ImageViewModel>();
            CreateMap<CreateProductViewModel, ProductModel>();
            CreateMap<ProductModel, ProductViewModel>();
            CreateMap<CartEntity, CartModel>();
            CreateMap<CartModel, CartEntity>();
            CreateMap<CartViewModel, CartModel>();
            CreateMap<CartModel, CartViewModel>();
            CreateMap<byte[], string>().ConvertUsing(src => Convert.ToBase64String(src));
        }
    }
}
