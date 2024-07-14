using Application.Features.Auth.Rules;
using Application.Features.Users.Constants;
using Application.Services.Auth;
using Application.Services.OperationClaims;
using Application.Services.UserOperationClaims;
using Application.Services.Users;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public RegisterCommand()
    {
        UserForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public RegisterCommand(UserForRegisterDto userForRegisterDto, string ipAddress)
    {
        UserForRegisterDto = userForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IOperationClaimService _operationClaimService;

        public RegisterCommandHandler(IUserService userService, IAuthService authService, AuthBusinessRules authBusinessRules, IUserOperationClaimService userOperationClaimService, IOperationClaimService operationClaimService)
        {
            _userService = userService;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _userOperationClaimService = userOperationClaimService;
            _operationClaimService = operationClaimService;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.UserForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );

            User newUser =
                new()
                {
                    UserName = request.UserForRegisterDto.UserName,
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            User createdUser = await _userService.AddAsync(newUser);

            ICollection<OperationClaim> operationClaims = [];
            ICollection<UserOperationClaim> userOperationClaims = [];

            foreach (var item in UsersOperationClaims.InitialRoles)
            {
                var operationClaim = await _operationClaimService.GetListAsync(x => x.Name.Contains(item));
                if (operationClaim != null)
                    operationClaims.Add(operationClaim.Items.First());
            }

            if (operationClaims != null)
            {
                foreach (var item in operationClaims)
                {
                    userOperationClaims.Add(
                        new UserOperationClaim() { UserId = createdUser.Id, OperationClaimId = item.Id }
                    );
                }
                userOperationClaims = await _userOperationClaimService.AddRangeAsync(userOperationClaims);
            }

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
