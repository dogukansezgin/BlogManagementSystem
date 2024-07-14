using System.Linq.Expressions;
using Application.Features.Comments.Rules;
using Application.Features.Comments.Rules;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Services.Comments;

public class CommentManager : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly CommentBusinessRules _commentBusinessRules;

    public CommentManager(ICommentRepository commentRepository, CommentBusinessRules commentBusinessRules)
    {
        _commentRepository = commentRepository;
        _commentBusinessRules = commentBusinessRules;
    }

    public async Task<Comment?> GetAsync(
        Expression<Func<Comment, bool>> predicate,
        Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Comment? comment = await _commentRepository.GetAsync(
            predicate,
            include,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return comment;
    }

    public async Task<IPaginate<Comment>?> GetListAsync(
        Expression<Func<Comment, bool>>? predicate = null,
        Func<IQueryable<Comment>, IOrderedQueryable<Comment>>? orderBy = null,
        Func<IQueryable<Comment>, IIncludableQueryable<Comment, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Comment> commentList = await _commentRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return commentList;
    }

    public async Task<Comment> AddAsync(Comment comment)
    {
        await _commentBusinessRules.CommentForeignKeysShouldExist(comment);

        Comment addedComment = await _commentRepository.AddAsync(comment);

        return addedComment;
    }

    public async Task<Comment> UpdateAsync(Comment comment)
    {
        await _commentBusinessRules.CommentForeignKeysShouldExist(comment);
        await _commentBusinessRules.CommentIdShouldExistWhenSelected(comment.Id);

        Comment updatedComment = await _commentRepository.UpdateAsync(comment);

        return updatedComment;
    }

    public async Task<ICollection<Comment>> UpdateRangeAsync(ICollection<Comment> comments)
    {
        foreach (Comment comment in comments)
        {
            await _commentBusinessRules.CommentShouldExistWhenSelected(comment);
            await _commentBusinessRules.CommentForeignKeysShouldExist(comment);
        }

        ICollection<Comment> updatedComments = await _commentRepository.UpdateRangeAsync(comments);

        return updatedComments;
    }

    public async Task<Comment> DeleteAsync(Comment comment, bool permanent = false)
    {
        await _commentBusinessRules.CommentShouldExistWhenSelected(comment);

        Comment deletedComment = await _commentRepository.DeleteAsync(comment, permanent);

        return deletedComment;
    }

    public async Task<ICollection<Comment>> DeleteRangeAsync(
        ICollection<Comment> comments,
        bool permanent = false
    )
    {
        foreach (Comment comment in comments)
        {
            await _commentBusinessRules.CommentShouldExistWhenSelected(comment);
        }

        ICollection<Comment> deletedComments = await _commentRepository.DeleteRangeCustomAsync(
            comments,
            permanent
        );

        return deletedComments;
    }

    public async Task<Comment> RestoreAsync(Comment comment)
    {
        await _commentBusinessRules.CommentShouldExistWhenSelected(comment);

        Comment restoredComment = await _commentRepository.RestoreAsync(comment);

        return restoredComment;
    }

    public async Task<ICollection<Comment>> RestoreRangeAsync(ICollection<Comment> comments)
    {
        foreach (Comment comment in comments)
        {
            await _commentBusinessRules.CommentShouldExistWhenSelected(comment);
        }

        ICollection<Comment> deletedComments = await _commentRepository.RestoreRangeCustomAsync(comments);

        return deletedComments;
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        Comment? comment = await _commentRepository.GetAsync(x => x.Id == id);

        await _commentBusinessRules.CommentShouldExistWhenSelected(comment);

        return comment;
    }
}
