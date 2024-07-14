using NArchitecture.Core.Application.Responses;

namespace Application.Features.Comments.Commands.Delete;

public class DeletedCommentResponse : IResponse
{
    public Guid Id { get; set; }
    public DateTime DeletedDate { get; set; }
    public bool IsPermament { get; set; }
}
