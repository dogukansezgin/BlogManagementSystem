using Application.Features.Users.Constants;
using Application.Features.Users.Rules;
using Application.Services.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Commands.Create;

public class CreateUserCommand : IRequest<CreatedUserResponse>, ISecuredRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public CreateUserCommand()
    {
        UserName = string.Empty;
        Email = string.Empty;
        Password = string.Empty;
    }

    public CreateUserCommand(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }

    public string[] Roles => new[] { UsersOperationClaims.Admin };

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreatedUserResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateUserCommandHandler(IUserService userService, IMapper mapper, UserBusinessRules userBusinessRules)
        {
            _userService = userService;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<CreatedUserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            await _userBusinessRules.UserEmailShouldNotExistsWhenInsert(request.Email);
            User user = _mapper.Map<User>(request);

            HashingHelper.CreatePasswordHash(
                request.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            User createdUser = await _userService.AddAsync(user);

            CreatedUserResponse response = _mapper.Map<CreatedUserResponse>(createdUser);
            return response;
        }
    }
}
