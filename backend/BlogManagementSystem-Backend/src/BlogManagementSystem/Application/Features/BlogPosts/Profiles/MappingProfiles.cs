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
        CreateMap<BlogPost, GetByIdBlogPostResponse>().ReverseMap();
        CreateMap<BlogPost, GetListBlogPostListItemDto>().ReverseMap();
        CreateMap<IPaginate<BlogPost>, GetListResponse<GetListBlogPostListItemDto>>().ReverseMap();
    }
}
