using FluentValidation;

namespace Application.Features.BlogPosts.Commands.DeleteRange;

public class DeleteRangeBlogPostCommandValidator : AbstractValidator<DeleteRangeBlogPostCommand>
{
    public DeleteRangeBlogPostCommandValidator()
    {
        RuleFor(c => c.Ids).NotEmpty();
    }
}
