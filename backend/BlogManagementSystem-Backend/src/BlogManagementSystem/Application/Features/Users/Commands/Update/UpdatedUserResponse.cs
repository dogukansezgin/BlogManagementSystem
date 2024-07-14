using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Commands.Update;

public class UpdatedUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public UpdatedUserResponse()
    {
        UserName = string.Empty;
        Email = string.Empty;
    }

    public UpdatedUserResponse(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }
}
