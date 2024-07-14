using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Commands.Update;

public class UpdatedCommentResponse : IResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid BlogPostId { get; set; }
}
