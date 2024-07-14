using Application.Features.BlogPosts.Constants;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.BlogPosts.Rules;

public class BlogPostBusinessRules : BaseBusinessRules
{
    private readonly IBlogPostRepository _blogPostRepository;
    private readonly ILocalizationService _localizationService;
    private readonly UserBusinessRules _userBusinessRules;

    public BlogPostBusinessRules(
        IBlogPostRepository blogPostRepository,
        ILocalizationService localizationService,
        UserBusinessRules userBusinessRules
    )
    {
        _blogPostRepository = blogPostRepository;
        _localizationService = localizationService;
        _userBusinessRules = userBusinessRules;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, BlogPostsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task BlogPostShouldExistWhenSelected(BlogPost? blogPost)
    {
        if (blogPost == null)
            await throwBusinessException(BlogPostsBusinessMessages.BlogPostNotExists);
    }

    public async Task BlogPostIdShouldExistWhenSelected(Guid id)
    {
        BlogPost? blogPost = await _blogPostRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        await BlogPostShouldExistWhenSelected(blogPost);
    }

    public async Task BlogPostForeignKeysShouldExist(BlogPost? blogPost)
    {
        await BlogPostShouldExistWhenSelected(blogPost);

        await _userBusinessRules.UserIdShouldBeExistsWhenSelected(blogPost.UserId);
    }

    public async Task BlogPostShouldNotExist(BlogPost? blogPost)
    {
        var isExistName = _blogPostRepository.Get(x => x.Title.Trim() == blogPost.Title.Trim() && x.Id != blogPost.Id) is not null;

        if (isExistName)
            await throwBusinessException(BlogPostsBusinessMessages.BlogPostExists);
    }
}
