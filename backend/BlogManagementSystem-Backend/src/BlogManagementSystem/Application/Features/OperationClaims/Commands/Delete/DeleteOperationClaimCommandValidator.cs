using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OperationClaims.Commands.Delete;

public class DeleteOperationClaimCommandValidator : AbstractValidator<DeleteOperationClaimCommand>
{
    public DeleteOperationClaimCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
