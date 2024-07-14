using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Users.Commands.UpdatePasswordFromAuth;

public class UpdatedUserPasswordFromAuthResponse : IResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public AccessToken AccessToken { get; set; }

    public UpdatedUserPasswordFromAuthResponse()
    {
        UserName = string.Empty;
        Email = string.Empty;
        AccessToken = null!;
    }

    public UpdatedUserPasswordFromAuthResponse(Guid id, string userName, string email, AccessToken accessToken)
    {
        Id = id;
        UserName = userName;
        Email = email;
        AccessToken = accessToken;
    }
}
