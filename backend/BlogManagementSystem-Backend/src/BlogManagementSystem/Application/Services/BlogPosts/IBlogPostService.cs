using System.Linq.Expressions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.BlogPosts;

public interface IBlogPostService
{
    Task<BlogPost?> GetAsync(
        Expression<Func<BlogPost, bool>> predicate,
        Func<IQueryable<BlogPost>, IIncludableQueryable<BlogPost, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<BlogPost>?> GetListAsync(
        Expression<Func<BlogPost, bool>>? predicate = null,
        Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>>? orderBy = null,
        Func<IQueryable<BlogPost>, IIncludableQueryable<BlogPost, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<BlogPost> AddAsync(BlogPost blogPost);
    Task<BlogPost> UpdateAsync(BlogPost blogPost);
    Task<BlogPost> DeleteAsync(BlogPost blogPost, bool permanent = false);
    Task<ICollection<BlogPost>> DeleteRangeAsync(ICollection<BlogPost> bootcamps, bool permanent = false);
    Task<BlogPost> RestoreAsync(BlogPost blogPost);
    Task<ICollection<BlogPost>> RestoreRangeAsync(ICollection<BlogPost> bootcamps);
    Task<BlogPost> GetByIdAsync(Guid id);
    Task<BlogPost> GetByTitleAsync(string name);
}
