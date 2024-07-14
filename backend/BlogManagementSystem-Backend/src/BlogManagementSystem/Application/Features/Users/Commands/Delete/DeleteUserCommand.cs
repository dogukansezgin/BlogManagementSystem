using Application.Features.Users.Constants;
using Application.Features.Users.Rules;
using Application.Services.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.Users.Commands.Delete;

public class DeleteUserCommand : IRequest<DeletedUserResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => new[] { UsersOperationClaims.Admin };

    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeletedUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public DeleteUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userService = userService;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<DeletedUserResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            await _userService.DeleteAsync(user!);

            DeletedUserResponse response = _mapper.Map<DeletedUserResponse>(user);
            return response;
        }
    }
}
