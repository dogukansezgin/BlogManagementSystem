using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IBlogPostRepository : IAsyncRepository<BlogPost, Guid>, IRepository<BlogPost, Guid> 
{
    Task<BlogPost> RestoreAsync(BlogPost blogPost);
    Task<ICollection<BlogPost>> DeleteRangeCustomAsync(
        ICollection<BlogPost> entities,
        bool permanent = false,
        CancellationToken cancellationToken = default
    );
    Task<ICollection<BlogPost>> RestoreRangeCustomAsync(ICollection<BlogPost> entities);
}
