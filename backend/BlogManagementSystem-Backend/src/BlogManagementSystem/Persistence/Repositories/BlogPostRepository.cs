using Application.Services.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class BlogPostRepository : EfRepositoryBase<BlogPost, Guid, BaseDbContext>, IBlogPostRepository
{
    public BlogPostRepository(BaseDbContext context) 
        : base(context) { }
}
