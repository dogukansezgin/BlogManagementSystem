using FluentValidation;

namespace Application.Features.BlogPosts.Commands.Delete;

public class DeleteBlogPostCommandValidator : AbstractValidator<DeleteBlogPostCommand>
{
    public DeleteBlogPostCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
