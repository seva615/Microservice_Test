using AutoMapper;
using Comment.Api;
using Comment.Data.Entity;
using Commet.Service.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CommentEntity, CommentModel>();
        CreateMap<CommentModel, CommentEntity>();
        CreateMap<CommentModel, CommentViewModel>();
        CreateMap<CommentViewModel, CommentModel>();

    }
}