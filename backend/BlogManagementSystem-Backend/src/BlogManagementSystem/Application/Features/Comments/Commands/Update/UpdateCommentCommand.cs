using Application.Features.Users.Constants;
using Application.Services.Comments;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.Comments.Commands.Update;

public class UpdateCommentCommand : IRequest<UpdatedCommentResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid BlogPostId { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetComments"];

    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, UpdatedCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public UpdateCommentCommandHandler(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<UpdatedCommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? comment = await _commentService.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken
            );

            comment = _mapper.Map(request, comment);

            await _commentService.UpdateAsync(comment!);

            UpdatedCommentResponse response = _mapper.Map<UpdatedCommentResponse>(comment);
            return response;
        }
    }
}
