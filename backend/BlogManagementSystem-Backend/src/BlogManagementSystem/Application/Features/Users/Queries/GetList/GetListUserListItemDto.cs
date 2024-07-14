using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Users.Queries.GetList;

public class GetListUserListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }

    public GetListUserListItemDto()
    {
        UserName = string.Empty;
        Email = string.Empty;
    }

    public GetListUserListItemDto(
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
