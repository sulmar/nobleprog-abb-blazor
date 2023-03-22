using Auth.Api.Domain;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Infrastructure;

public class FakeUserIdentityRepository : IUserIdentityRepository
{
    private readonly IDictionary<string, UserIdentity> _users;

    public FakeUserIdentityRepository(IPasswordHasher<UserIdentity> passwordHasher)
    {
        _users = new Dictionary<string, UserIdentity>
        {
            ["john"] = new UserIdentity
            {
                Username = "john", Email = "john@domain.com", Phone = "555001110"
            },
            ["kate"] = new UserIdentity
            {
                Username = "kate", Email = "kate@domain.com", Phone = "555002222",
                Roles = new[] {"developer" }
            },
            ["bob"] = new UserIdentity {Username = "bob", Email = "bob@domain.com", Phone = "555111111"},
        };

        foreach (var user in _users.Values)
        {
            user.HashedPassword = passwordHasher.HashPassword(user, "123");
        }
    }

    public UserIdentity GetByUsername(string username)
    {
        if (_users.TryGetValue(username, out var identity))
            return identity;
        else
            return null;
    }
}