namespace Auth.Api.Domain;

public interface ITokenService
{
    string Create(UserIdentity identity);
}
