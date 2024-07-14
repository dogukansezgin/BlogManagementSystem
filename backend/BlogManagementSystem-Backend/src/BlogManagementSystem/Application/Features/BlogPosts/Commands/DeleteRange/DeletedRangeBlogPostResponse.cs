using NArchitecture.Core.Application.Responses;

namespace Application.Features.BlogPosts.Commands.DeleteRange;

public class DeletedRangeBlogPostResponse : IResponse
{
    public ICollection<Guid> Ids { get; set; }
    public DateTime DeletedDate { get; set; }
    public bool IsPermament { get; set; }
}
