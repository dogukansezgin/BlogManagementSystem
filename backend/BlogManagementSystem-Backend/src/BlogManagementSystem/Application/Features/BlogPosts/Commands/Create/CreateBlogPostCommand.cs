using Application.Features.Users.Constants;
using Application.Services.BlogPosts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.BlogPosts.Commands.Create;

public class CreateBlogPostCommand : IRequest<CreatedBlogPostResponse>, ISecuredRequest
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBlogPosts"];

    public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, CreatedBlogPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _blogPostService;

        public CreateBlogPostCommandHandler(IMapper mapper, IBlogPostService blogPostService)
        {
            _mapper = mapper;
            _blogPostService = blogPostService;
        }

        public async Task<CreatedBlogPostResponse> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            BlogPost blogPost = _mapper.Map<BlogPost>(request);

            await _blogPostService.AddAsync(blogPost);

            blogPost.CreatedDate = DateTime.Now;

            CreatedBlogPostResponse response = _mapper.Map<CreatedBlogPostResponse>(blogPost);
            return response;
        }
    }
}
