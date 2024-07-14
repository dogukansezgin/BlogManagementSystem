using FluentValidation;

namespace Application.Features.Comments.Commands.DeleteRange;

public class DeleteRangeCommentCommandValidator : AbstractValidator<DeleteRangeCommentCommand>
{
    public DeleteRangeCommentCommandValidator()
    {
        RuleFor(c => c.Ids).NotEmpty();
    }
}
