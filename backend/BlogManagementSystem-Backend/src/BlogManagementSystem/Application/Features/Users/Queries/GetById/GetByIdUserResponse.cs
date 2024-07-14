using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }

    public GetByIdUserResponse()
    {
        UserName = string.Empty;
        Email = string.Empty;
    }

    public GetByIdUserResponse(
        Guid id,
        string email,
        string userName
    )
    {
        Id = id;
        Email = email;
        UserName = userName;

    }
}
