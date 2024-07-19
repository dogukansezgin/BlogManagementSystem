using Application.Features.BlogPosts.Commands.Create;
using Application.Features.BlogPosts.Commands.Delete;
using Application.Features.BlogPosts.Commands.Update;
using Application.Features.BlogPosts.Queries.GetById;
using Application.Features.BlogPosts.Queries.GetList;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BlogPosts.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BlogPost, CreateBlogPostCommand>().ReverseMap();
        CreateMap<BlogPost, CreatedBlogPostResponse>().ReverseMap();
        CreateMap<BlogPost, UpdateBlogPostCommand>().ReverseMap();
        CreateMap<BlogPost, UpdatedBlogPostResponse>().ReverseMap();
        CreateMap<BlogPost, DeleteBlogPostCommand>().ReverseMap();
        CreateMap<BlogPost, DeletedBlogPostResponse>().ReverseMap();

        CreateMap<BlogPost, GetByIdBlogPostResponse>()
            .ForMember(dest => dest.UserUserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ReverseMap();
        CreateMap<BlogPost, GetListBlogPostListItemDto>()
            .ForMember(dest => dest.UserUserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
            .ReverseMap();
        CreateMap<IPaginate<BlogPost>, GetListResponse<GetListBlogPostListItemDto>>().ReverseMap();

        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.UserUserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Replies, opt => opt.MapFrom(src => src.Replies))
            .ReverseMap();
        CreateMap<Comment, CommentDtoCount>().ReverseMap();
    }
}
