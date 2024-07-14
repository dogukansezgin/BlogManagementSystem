using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Application.Features.BlogPosts.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.BlogPosts;

public class BlogPostManager : IBlogPostService
{
    private readonly IBlogPostRepository _blogpostRepository;
    private readonly BlogPostBusinessRules _blogpostBusinessRules;

    public BlogPostManager(IBlogPostRepository blogpostRepository, BlogPostBusinessRules blogpostBusinessRules)
    {
        _blogpostRepository = blogpostRepository;
        _blogpostBusinessRules = blogpostBusinessRules;
    }

    public async Task<BlogPost?> GetAsync(
        Expression<Func<BlogPost, bool>> predicate,
        Func<IQueryable<BlogPost>, IIncludableQueryable<BlogPost, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        BlogPost? blogPost = await _blogpostRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return blogPost;
    }

    public async Task<IPaginate<BlogPost>?> GetListAsync(
        Expression<Func<BlogPost, bool>>? predicate = null,
        Func<IQueryable<BlogPost>, IOrderedQueryable<BlogPost>>? orderBy = null,
        Func<IQueryable<BlogPost>, IIncludableQueryable<BlogPost, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<BlogPost> blogpostList = await _blogpostRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return blogpostList;
    }

    public async Task<BlogPost> AddAsync(BlogPost blogPost)
    {
        await _blogpostBusinessRules.BlogPostForeignKeysShouldExist(blogPost);
        await _blogpostBusinessRules.BlogPostShouldNotExist(blogPost);

        BlogPost addedBlogPost = await _blogpostRepository.AddAsync(blogPost);

        return addedBlogPost;
    }

    public async Task<BlogPost> UpdateAsync(BlogPost blogPost)
    {
        await _blogpostBusinessRules.BlogPostForeignKeysShouldExist(blogPost);
        await _blogpostBusinessRules.BlogPostIdShouldExistWhenSelected(blogPost.Id);
        await _blogpostBusinessRules.BlogPostShouldNotExist(blogPost);

        BlogPost updatedBlogPost = await _blogpostRepository.UpdateAsync(blogPost);

        return updatedBlogPost;
    }

    public async Task<BlogPost> DeleteAsync(BlogPost blogPost, bool permanent = false)
    {
        await _blogpostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);

        BlogPost deletedBlogPost = await _blogpostRepository.DeleteAsync(blogPost, permanent);

        return deletedBlogPost;
    }

    public async Task<ICollection<BlogPost>> DeleteRangeAsync(ICollection<BlogPost> blogposts, bool permanent = false)
    {
        foreach (BlogPost blogPost in blogposts)
        {
            await _blogpostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);
        }

        ICollection<BlogPost> deletedBlogPosts = await _blogpostRepository.DeleteRangeCustomAsync(blogposts, permanent);

        return deletedBlogPosts;
    }

    public async Task<BlogPost> RestoreAsync(BlogPost blogPost)
    {
        await _blogpostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);

        BlogPost restoredBlogPost = await _blogpostRepository.RestoreAsync(blogPost);

        return restoredBlogPost;
    }

    public async Task<ICollection<BlogPost>> RestoreRangeAsync(ICollection<BlogPost> blogposts)
    {
        foreach (BlogPost blogPost in blogposts)
        {
            await _blogpostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);
        }

        ICollection<BlogPost> deletedBlogPosts = await _blogpostRepository.RestoreRangeCustomAsync(blogposts);

        return deletedBlogPosts;
    }

    public async Task<BlogPost> GetByIdAsync(Guid id)
    {
        BlogPost? blogPost = await _blogpostRepository.GetAsync(
            x => x.Id == id,
            include: x => x.Include(x => x.User)
        );

        await _blogpostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);

        return blogPost;
    }

    public async Task<BlogPost> GetByTitleAsync(string name)
    {
        BlogPost? blogPost = await _blogpostRepository.GetAsync(
            x => EF.Functions.Collate(x.Title.ToLower(), "SQL_Latin1_General_CP1253_CI_AI").Contains(name.ToLower()),
            include: x => x.Include(x => x.User)
        );

        await _blogpostBusinessRules.BlogPostShouldExistWhenSelected(blogPost);

        return blogPost;
    }
}
