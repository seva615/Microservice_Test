using AutoMapper;
using User.API.ControllerModels;
using User.API.ViewModels;
using User.Data.Entities;
using User.Service.Models;

namespace User.API
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<UserEntity, UserModel>();
            CreateMap<UserModel, UserEntity>();
            CreateMap<UserViewModel, UserModel>();
            CreateMap<UserModel, UserViewModel>();
            CreateMap<CreateUserViewModel, UserModel>();
            CreateMap<UserModel, CreateUserViewModel>();
            CreateMap<byte[], string>().ConvertUsing(src => Convert.ToBase64String(src));
        }
    }
}
