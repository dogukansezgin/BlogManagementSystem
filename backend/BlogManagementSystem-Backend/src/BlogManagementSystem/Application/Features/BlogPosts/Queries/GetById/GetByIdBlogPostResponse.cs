using Application.Features.BlogPosts.Queries.GetList;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.BlogPosts.Queries.GetById;

public class GetByIdBlogPostResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public string UserUserName { get; set; }
    public DateTime CreatedDate { get; set; }
    public List<CommentDto> Comments { get; set; }
}

public class CommentDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public string UserUserName { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid? ParentId { get; set; }
    public List<CommentDto> Replies { get; set; }
}
