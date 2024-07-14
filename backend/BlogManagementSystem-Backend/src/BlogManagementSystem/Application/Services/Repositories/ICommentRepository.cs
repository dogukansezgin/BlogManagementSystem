using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICommentRepository : IAsyncRepository<Comment, Guid>, IRepository<Comment, Guid> 
{
    Task<Comment> RestoreAsync(Comment comment);
    Task<ICollection<Comment>> DeleteRangeCustomAsync(
        ICollection<Comment> entities,
        bool permanent = false,
        CancellationToken cancellationToken = default
    );
    Task<ICollection<Comment>> RestoreRangeCustomAsync(ICollection<Comment> entities);
}