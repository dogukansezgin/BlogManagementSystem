using Application.Features.Users.Rules;
using Application.Services.Auth;
using Application.Services.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;

namespace Application.Features.Users.Commands.UpdatePasswordFromAuth;

public class UpdateUserPasswordFromAuthCommand : IRequest<UpdatedUserPasswordFromAuthResponse>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string? NewPassword { get; set; }

    public UpdateUserPasswordFromAuthCommand()
    {
        UserName = string.Empty;
        Password = string.Empty;
    }

    public UpdateUserPasswordFromAuthCommand(Guid id, string userName, string password)
    {
        Id = id;
        UserName = userName;
        Password = password;
    }

    public class UpdateUserPasswordFromAuthCommandHandler : IRequestHandler<UpdateUserPasswordFromAuthCommand, UpdatedUserPasswordFromAuthResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateUserPasswordFromAuthCommandHandler(
            IUserService userService,
            IMapper mapper,
            UserBusinessRules userBusinessRules,
            IAuthService authService
        )
        {
            _userService = userService;
            _mapper = mapper;
            _userBusinessRules = userBusinessRules;
            _authService = authService;
        }

        public async Task<UpdatedUserPasswordFromAuthResponse> Handle(
            UpdateUserPasswordFromAuthCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _userBusinessRules.UserPasswordShouldBeMatched(user: user!, request.Password);

            user = _mapper.Map(request, user);
            if (request.NewPassword != null && !string.IsNullOrWhiteSpace(request.NewPassword))
            {
                HashingHelper.CreatePasswordHash(
                    request.Password,
                    passwordHash: out byte[] passwordHash,
                    passwordSalt: out byte[] passwordSalt
                );
                user!.PasswordHash = passwordHash;
                user!.PasswordSalt = passwordSalt;
            }

            User updatedUser = await _userService.UpdateAsync(user!);

            UpdatedUserPasswordFromAuthResponse response = _mapper.Map<UpdatedUserPasswordFromAuthResponse>(updatedUser);
            response.AccessToken = await _authService.CreateAccessToken(user!);
            return response;
        }
    }
}
