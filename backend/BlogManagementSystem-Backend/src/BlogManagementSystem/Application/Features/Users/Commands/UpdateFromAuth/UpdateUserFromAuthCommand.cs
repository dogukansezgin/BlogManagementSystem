using Application.Features.Users.Rules;
using Application.Services.Auth;
using Application.Services.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Users.Commands.UpdateFromAuth;

public class UpdateUserFromAuthCommand : IRequest<UpdatedUserFromAuthResponse>
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public UpdateUserFromAuthCommand()
    {
        UserName = string.Empty;
        Email = string.Empty;
    }

    public UpdateUserFromAuthCommand(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public class UpdateUserFromAuthCommandHandler : IRequestHandler<UpdateUserFromAuthCommand, UpdatedUserFromAuthResponse>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly IAuthService _authService;

        public UpdateUserFromAuthCommandHandler(
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

        public async Task<UpdatedUserFromAuthResponse> Handle(
            UpdateUserFromAuthCommand request,
            CancellationToken cancellationToken
        )
        {
            User? user = await _userService.GetAsync(
                predicate: u => u.Id.Equals(request.Id),
                cancellationToken: cancellationToken
            );
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);
            await _userBusinessRules.UserEmailShouldNotExistsWhenUpdate(user!.Id, request.Email);

            user = _mapper.Map(request, user);

            User updatedUser = await _userService.UpdateAsync(user!);

            UpdatedUserFromAuthResponse response = _mapper.Map<UpdatedUserFromAuthResponse>(updatedUser);
            response.AccessToken = await _authService.CreateAccessToken(user!);
            return response;
        }
    }
}
