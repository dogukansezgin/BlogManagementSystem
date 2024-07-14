using FluentValidation;

namespace Application.Features.BlogPosts.Commands.Create;

public class CreateBlogPostCommandValidator : AbstractValidator<CreateBlogPostCommand>
{
    public CreateBlogPostCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.UserId).NotEmpty();
    }
}
