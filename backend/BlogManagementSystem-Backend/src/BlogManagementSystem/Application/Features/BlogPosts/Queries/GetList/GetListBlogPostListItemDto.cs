using NArchitecture.Core.Application.Dtos;

namespace Application.Features.BlogPosts.Queries.GetList;

public class GetListBlogPostListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public string UserUserName { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<CommentDtoCount> Comments { get; set; }
}

public class CommentDtoCount
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
}