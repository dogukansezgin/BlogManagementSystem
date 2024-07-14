namespace Domain.Entities;

public class User : NArchitecture.Core.Security.Entities.User<Guid>
{
    public string UserName { get; set; }

    public virtual ICollection<UserOperationClaim> UserOperationClaims { get; set; } = default!;
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = default!;

    public User()
    {
        UserName = string.Empty;
    }

    public User(Guid id, string userName)
    {
        Id = id;
        UserName = userName;
    }
}
