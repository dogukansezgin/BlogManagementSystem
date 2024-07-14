using FluentValidation;

namespace Application.Features.Comments.Commands.Create;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.BlogPostId).NotEmpty();
    }
}
