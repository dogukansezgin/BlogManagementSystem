using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Commands.DeleteRange;

public class DeletedRangeCommentResponse : IResponse
{
    public ICollection<Guid> Ids { get; set; }
    public DateTime DeletedDate { get; set; }
    public bool IsPermament { get; set; }
}
