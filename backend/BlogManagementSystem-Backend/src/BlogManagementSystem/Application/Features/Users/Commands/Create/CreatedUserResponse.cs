using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Commands.Create;

public class CreatedUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }

    public CreatedUserResponse()
    {
        UserName = string.Empty;
        Email = string.Empty;
    }

    public CreatedUserResponse(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }
}
