namespace Application.Features.Users.Constants;

public static class UsersOperationClaims
{
    private const string _section = "Users";

    public const string Admin = $"Admin";
    public const string User = $"User";

    public static readonly string[] InitialRolesArray = { User };

    public static readonly string[] InitialRoles = InitialRolesArray;
}
