using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Comments;

public interface ICommentService
{
    Task<Comment?> GetAsync(
        Expression<Func<Comment, bool>> predicate,
        Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Comment>?> GetListAsync(
        Expression<Func<Comment, bool>>? predicate = null,
        Func<IQueryable<Comment>, IOrderedQueryable<Comment>>? orderBy = null,
        Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Comment> AddAsync(Comment comment);
    Task<Comment> UpdateAsync(Comment comment);
    Task<ICollection<Comment>> UpdateRangeAsync(ICollection<Comment> comments);
    Task<Comment> DeleteAsync(Comment comment, bool permanent = false);
    Task<ICollection<Comment>> DeleteRangeAsync(ICollection<Comment> comments, bool permanent = false);
    Task<Comment> RestoreAsync(Comment comment);
    Task<ICollection<Comment>> RestoreRangeAsync(ICollection<Comment> comments);
    Task<Comment> GetByIdAsync(Guid id);
}
