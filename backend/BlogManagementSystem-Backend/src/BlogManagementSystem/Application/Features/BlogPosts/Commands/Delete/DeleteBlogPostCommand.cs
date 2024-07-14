using Application.Features.Users.Constants;
using Application.Services.BlogPosts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.BlogPosts.Commands.Delete;

public class DeleteBlogPostCommand : IRequest<DeletedBlogPostResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public bool IsPermament { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetBlogPosts"];

    public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand, DeletedBlogPostResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBlogPostService _blogPostService;

        public DeleteBlogPostCommandHandler(IMapper mapper, IBlogPostService blogPostService)
        {
            _mapper = mapper;
            _blogPostService = blogPostService;
        }

        public async Task<DeletedBlogPostResponse> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            BlogPost? blogPost = await _blogPostService.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken,
                withDeleted: true
            );

            await _blogPostService.DeleteAsync(blogPost!, request.IsPermament);

            DeletedBlogPostResponse response = _mapper.Map<DeletedBlogPostResponse>(blogPost);
            response.IsPermament = request.IsPermament;
            response.DeletedDate = request.IsPermament ? DateTime.UtcNow : response.DeletedDate;
            return response;
        }
    }
}
