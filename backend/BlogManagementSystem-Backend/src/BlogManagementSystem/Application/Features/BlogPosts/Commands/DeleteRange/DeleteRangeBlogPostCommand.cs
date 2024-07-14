using Application.Features.Users.Constants;
using Application.Services.BlogPosts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BlogPosts.Commands.DeleteRange;

public class DeleteRangeBlogPostCommand : IRequest<DeletedRangeBlogPostResponse>, ISecuredRequest
{
    public List<Guid> Ids { get; set; }
    public bool IsPermament { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => new[] { "GetBlogPosts" };

    public class DeleteRangeBlogPostCommandHandler : IRequestHandler<DeleteRangeBlogPostCommand, DeletedRangeBlogPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _blogPostService;

        public DeleteRangeBlogPostCommandHandler(IMapper mapper, IBlogPostService blogPostService)
        {
            _mapper = mapper;
            _blogPostService = blogPostService;
        }

        public async Task<DeletedRangeBlogPostResponse> Handle(
            DeleteRangeBlogPostCommand request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<BlogPost>? blogPosts = await _blogPostService.GetListAsync(
                size: request.Ids.Count,
                predicate: b => request.Ids.Contains(b.Id),
                cancellationToken: cancellationToken,
                withDeleted: true
            );

            await _blogPostService.DeleteRangeAsync(blogPosts.Items, request.IsPermament);

            DeletedRangeBlogPostResponse response = new DeletedRangeBlogPostResponse
            {
                Ids = blogPosts.Items.Select(b => b.Id).ToList(),
                DeletedDate = DateTime.UtcNow,
                IsPermament = request.IsPermament
            };

            return response;
        }
    }
}
