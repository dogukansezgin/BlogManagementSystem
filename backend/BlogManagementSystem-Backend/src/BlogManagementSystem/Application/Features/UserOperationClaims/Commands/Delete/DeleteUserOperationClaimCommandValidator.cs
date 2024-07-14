using FluentValidation;

namespace Application.Features.UserOperationClaims.Commands.Delete;

public class DeleteUserOperationClaimCommandValidator : AbstractValidator<DeleteUserOperationClaimCommand>
{
    public DeleteUserOperationClaimCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
