using Application.Services.BlogPosts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BlogPosts.Queries.GetList;

public class GetListBlogPostQuery : IRequest<GetListResponse<GetListBlogPostListItemDto>> /*, ISecuredRequest */
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListBlogPosts({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetBlogPosts";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListBlogPostQueryHandler : IRequestHandler<GetListBlogPostQuery, GetListResponse<GetListBlogPostListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _blogPostService;

        public GetListBlogPostQueryHandler(IMapper mapper, IBlogPostService blogPostService)
        {
            _mapper = mapper;
            _blogPostService = blogPostService;
        }

        public async Task<GetListResponse<GetListBlogPostListItemDto>> Handle(
            GetListBlogPostQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<BlogPost>? blogPosts = await _blogPostService.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderByDescending(y => y.CreatedDate),
                include: x => x.Include(x => x.User)
            );

            GetListResponse<GetListBlogPostListItemDto> response = _mapper.Map<GetListResponse<GetListBlogPostListItemDto>>(
                blogPosts
            );
            return response;
        }
    }
}
