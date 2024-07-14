using Application.Features.Users.Constants;
using Application.Services.Comments;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.Comments.Commands.Delete;

public class DeleteCommentCommand : IRequest<DeletedCommentResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public bool IsPermament { get; set; }

    public string[] Roles => [UsersOperationClaims.Admin, UsersOperationClaims.User];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetComments"];

    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, DeletedCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public DeleteCommentCommandHandler(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<DeletedCommentResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            Comment? comment = await _commentService.GetAsync(
                predicate: b => b.Id == request.Id,
                cancellationToken: cancellationToken,
                withDeleted: true
            );

            await _commentService.DeleteAsync(comment!, request.IsPermament);

            DeletedCommentResponse response = _mapper.Map<DeletedCommentResponse>(comment);
            response.IsPermament = request.IsPermament;
            response.DeletedDate = request.IsPermament ? DateTime.UtcNow : response.DeletedDate;
            return response;
        }
    }
}
