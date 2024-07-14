using Application.Features.Users.Constants;
using Application.Services.Comments;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Comments.Commands.DeleteRange;

public class DeleteRangeCommentCommand : IRequest<DeletedRangeCommentResponse>, ISecuredRequest
{
    public List<Guid> Ids { get; set; }
    public bool IsPermament { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => new[] { "GetComments" };

    public class DeleteRangeCommentCommandHandler : IRequestHandler<DeleteRangeCommentCommand, DeletedRangeCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public DeleteRangeCommentCommandHandler(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<DeletedRangeCommentResponse> Handle(
            DeleteRangeCommentCommand request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Comment>? comments = await _commentService.GetListAsync(
                size: request.Ids.Count,
                predicate: b => request.Ids.Contains(b.Id),
                cancellationToken: cancellationToken,
                withDeleted: true
            );

            await _commentService.DeleteRangeAsync(comments.Items, request.IsPermament);

            DeletedRangeCommentResponse response = new DeletedRangeCommentResponse
            {
                Ids = comments.Items.Select(b => b.Id).ToList(),
                DeletedDate = DateTime.UtcNow,
                IsPermament = request.IsPermament
            };

            return response;
        }
    }
}
