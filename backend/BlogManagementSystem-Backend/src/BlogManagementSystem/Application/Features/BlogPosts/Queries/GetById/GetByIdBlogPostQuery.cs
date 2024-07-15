using Application.Features.Users.Constants;
using Application.Services.BlogPosts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.BlogPosts.Queries.GetById;

public class GetByIdBlogPostQuery : IRequest<GetByIdBlogPostResponse> /*, ISecuredRequest*/
{
    public Guid Id { get; set; }

    public string[] Roles => [];

    public class GetByIdBlogPostQueryHandler : IRequestHandler<GetByIdBlogPostQuery, GetByIdBlogPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _blogPostService;

        public GetByIdBlogPostQueryHandler(IMapper mapper, IBlogPostService blogPostService)
        {
            _mapper = mapper;
            _blogPostService = blogPostService;
        }

        public async Task<GetByIdBlogPostResponse> Handle(GetByIdBlogPostQuery request, CancellationToken cancellationToken)
        {
            BlogPost? blogPost = await _blogPostService.GetByIdAsync(request.Id);

            GetByIdBlogPostResponse response = _mapper.Map<GetByIdBlogPostResponse>(blogPost);
            return response;
        }
    }
}
