using Application.Features.Users.Constants;
using Application.Services.Comments;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentCommand : IRequest<CreatedCommentResponse>, ISecuredRequest
{
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid BlogPostId { get; set; }
    public Guid? ParentId { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetComments"];

    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CreatedCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public CreateCommentCommandHandler(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<CreatedCommentResponse> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment comment = _mapper.Map<Comment>(request);

            await _commentService.AddAsync(comment);

            comment.CreatedDate = DateTime.Now;

            CreatedCommentResponse response = _mapper.Map<CreatedCommentResponse>(comment);
            return response;
        }
    }
}
