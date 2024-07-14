using NArchitecture.Core.Application.Responses;

namespace Application.Features.BlogPosts.Commands.Update;

public class UpdatedBlogPostResponse : IResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }
}
