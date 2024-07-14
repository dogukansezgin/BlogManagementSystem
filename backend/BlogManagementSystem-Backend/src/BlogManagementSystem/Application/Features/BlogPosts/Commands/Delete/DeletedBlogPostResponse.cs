using NArchitecture.Core.Application.Responses;

namespace Application.Features.BlogPosts.Commands.Delete;

public class DeletedBlogPostResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime DeletedDate { get; set; }
    public bool IsPermament { get; set; }
}
