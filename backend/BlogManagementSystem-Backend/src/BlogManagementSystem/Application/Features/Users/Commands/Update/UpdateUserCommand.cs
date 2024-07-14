using Application.Features.Users.Constants;
using Application.Features.Users.Rules;
using Application.Services.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;

namespace Application.Features.Users.Commands.Update;

public class UpdateUserCommand : IRequest<UpdatedUserResponse>, ISecuredRequest
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    //public string Password { get; set; }

    public UpdateUserCommand()
    {
        UserName = string.Empty;
        Email = string.Empty;
        //Password = string.Empty;
    }

    public UpdateUserCommand(Guid id, string userName, string lastName, string email/*, string password*/)
    {
        Id = id;
        UserName = userName;
        Email = email;
        //Password = password;
    }

    public string[] Roles => new[] { UsersOperationClaims.Admin };

    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdatedUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public UpdateUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userService = userService;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<UpdatedUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, user.Email);
            user = _mapper.Map(request, user);

            //HashingHelper.CreatePasswordHash(
            //    request.Password,
            //    passwordHash: out byte[] passwordHash,
            //    passwordSalt: out byte[] passwordSalt
            //);
            //user!.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            await _userService.UpdateAsync(user);

            UpdatedUserResponse response = _mapper.Map<UpdatedUserResponse>(user);
            return response;
        }
    }
}
