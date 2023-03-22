namespace Auth.Api.Domain;

public interface IAuthService
{
    bool TryAuthorize(string username, string password, out UserIdentity identity);
}
