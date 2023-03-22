using Auth.Api.Domain;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Infrastructure;

public class AuthService : IAuthService
{
    private readonly IUserIdentityRepository userIdentityRepository;
    private readonly IPasswordHasher<UserIdentity> passwordHasher;

    public AuthService(IUserIdentityRepository userIdentityRepository, IPasswordHasher<UserIdentity> passwordHasher)
    {
        this.userIdentityRepository = userIdentityRepository;
        this.passwordHasher = passwordHasher;
    }

    public bool TryAuthorize(string username, string password, out UserIdentity identity)
    {
         identity = userIdentityRepository.GetByUsername(username);

        return identity != null 
            && passwordHasher.VerifyHashedPassword(identity, identity.HashedPassword, password) == PasswordVerificationResult.Success;
    }
}
