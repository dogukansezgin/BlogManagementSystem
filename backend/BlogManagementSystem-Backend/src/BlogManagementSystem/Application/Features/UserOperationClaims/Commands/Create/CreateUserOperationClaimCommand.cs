using Application.Features.UserOperationClaims.Rules;
using Application.Features.Users.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.UserOperationClaims.Commands.Create;

public class CreateUserOperationClaimCommand : IRequest<CreatedUserOperationClaimResponse>, ISecuredRequest
{
    public Guid UserId { get; set; }
    public int OperationClaimId { get; set; }

    public string[] Roles => new[] { UsersOperationClaims.Admin };

    public class CreateUserOperationClaimCommandHandler
        : IRequestHandler<CreateUserOperationClaimCommand, CreatedUserOperationClaimResponse>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IMapper _mapper;
        private readonly UserOperationClaimBusinessRules _userOperationClaimBusinessRules;

        public CreateUserOperationClaimCommandHandler(
            IUserOperationClaimRepository userOperationClaimRepository,
            IMapper mapper,
            UserOperationClaimBusinessRules userOperationClaimBusinessRules
        )
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _mapper = mapper;
            _userOperationClaimBusinessRules = userOperationClaimBusinessRules;
        }

        public async Task<CreatedUserOperationClaimResponse> Handle(
            CreateUserOperationClaimCommand request,
            CancellationToken cancellationToken
        )
        {
            await _userOperationClaimBusinessRules.UserShouldNotHasOperationClaimAlreadyWhenInsert(
                request.UserId,
                request.OperationClaimId
            );
            UserOperationClaim mappedUserOperationClaim = _mapper.Map<UserOperationClaim>(request);

            UserOperationClaim createdUserOperationClaim = await _userOperationClaimRepository.AddAsync(mappedUserOperationClaim);

            CreatedUserOperationClaimResponse createdUserOperationClaimDto = _mapper.Map<CreatedUserOperationClaimResponse>(
                createdUserOperationClaim
            );
            return createdUserOperationClaimDto;
        }
    }
}
