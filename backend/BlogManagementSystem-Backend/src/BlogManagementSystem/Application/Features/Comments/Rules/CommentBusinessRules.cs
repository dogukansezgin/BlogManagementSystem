using Application.Features.Comments.Constants;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;

namespace Application.Features.Comments.Rules;

public class CommentBusinessRules : BaseBusinessRules
{
    private readonly ICommentRepository _commentRepository;
    private readonly ILocalizationService _localizationService;
    private readonly UserBusinessRules _userBusinessRules;

    public CommentBusinessRules(
        ICommentRepository commentRepository,
        ILocalizationService localizationService,
        UserBusinessRules userBusinessRules
    )
    {
        _commentRepository = commentRepository;
        _localizationService = localizationService;
        _userBusinessRules = userBusinessRules;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, CommentsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task CommentShouldExistWhenSelected(Comment? comment)
    {
        if (comment == null)
            await throwBusinessException(CommentsBusinessMessages.CommentNotExists);
    }

    public async Task CommentIdShouldExistWhenSelected(Guid id)
    {
        Comment? comment = await _commentRepository.GetAsync(predicate: b => b.Id == id, enableTracking: false);
        await CommentShouldExistWhenSelected(comment);
    }

    public async Task CommentForeignKeysShouldExist(Comment? comment)
    {
        await CommentShouldExistWhenSelected(comment);

        await _userBusinessRules.UserIdShouldBeExistsWhenSelected(comment.UserId);
    }

    //public async Task CommentShouldNotExist(Comment? comment)
    //{
    //    var isExistName = _commentRepository.Get(x => x.Title.Trim() == comment.Title.Trim() && x.Id != comment.Id) is not null;

    //    if (isExistName)
    //        await throwBusinessException(CommentsBusinessMessages.CommentExists);
    //}
}
