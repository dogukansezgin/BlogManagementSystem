using Application.Features.Users.Constants;
using Application.Services.BlogPosts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.BlogPosts.Commands.Update;

public class UpdateBlogPostCommand : IRequest<UpdatedBlogPostResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBlogPosts"];

    public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand, UpdatedBlogPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _blogPostService;

        public UpdateBlogPostCommandHandler(IMapper mapper, IBlogPostService blogPostService)
        {
            _mapper = mapper;
            _blogPostService = blogPostService;
        }

        public async Task<UpdatedBlogPostResponse> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            BlogPost? blogPost = await _blogPostService.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken
            );

            blogPost = _mapper.Map(request, blogPost);

            await _blogPostService.UpdateAsync(blogPost!);

            UpdatedBlogPostResponse response = _mapper.Map<UpdatedBlogPostResponse>(blogPost);
            return response;
        }
    }
}
