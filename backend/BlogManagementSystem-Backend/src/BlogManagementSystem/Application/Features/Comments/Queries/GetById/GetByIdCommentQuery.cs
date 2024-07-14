using Application.Services.Comments;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Comments.Queries.GetById;

public class GetByIdCommentQuery : IRequest<GetByIdCommentResponse>
{
    public Guid Id { get; set; }

    public string[] Roles => [];

    public class GetByIdCommentQueryHandler : IRequestHandler<GetByIdCommentQuery, GetByIdCommentResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICommentService _commentService;

        public GetByIdCommentQueryHandler(IMapper mapper, ICommentService commentService)
        {
            _mapper = mapper;
            _commentService = commentService;
        }

        public async Task<GetByIdCommentResponse> Handle(GetByIdCommentQuery request, CancellationToken cancellationToken)
        {
            Comment? comment = await _commentService.GetByIdAsync(request.Id);

            GetByIdCommentResponse response = _mapper.Map<GetByIdCommentResponse>(comment);
            return response;
        }
    }
}
