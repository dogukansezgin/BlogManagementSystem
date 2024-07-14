using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Comments.Queries.GetList;

public class GetListCommentListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid BlogPostId { get; set; }
    public DateTime CreatedDate { get; set; }
}
