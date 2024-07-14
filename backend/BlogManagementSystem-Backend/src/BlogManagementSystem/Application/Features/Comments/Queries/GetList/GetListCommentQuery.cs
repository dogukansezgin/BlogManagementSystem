using Application.Services.Comments;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Comments.Queries.GetList;

public class GetListCommentQuery : IRequest<GetListResponse<GetListCommentListItemDto>>
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListComments({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetComments";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListCommentQueryHandler : IRequestHandler<GetListCommentQuery, GetListResponse<GetListCommentListItemDto>>
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public GetListCommentQueryHandler(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<GetListResponse<GetListCommentListItemDto>> Handle(
            GetListCommentQuery request,
            CancellationToken cancellationToken
        )
        {
            IPaginate<Comment>? comments = await _commentService.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken,
                orderBy: x => x.OrderByDescending(y => y.CreatedDate),
                include: x => x.Include(x => x.User)
            );

            GetListResponse<GetListCommentListItemDto> response = _mapper.Map<GetListResponse<GetListCommentListItemDto>>(
                comments
            );
            return response;
        }
    }
}
