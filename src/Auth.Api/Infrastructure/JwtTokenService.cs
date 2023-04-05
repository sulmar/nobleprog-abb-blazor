using Auth.Api.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Auth.Api.Infrastructure;

public class JwtTokenService : ITokenService
{
    private static Claim[] GetRoleClaims(string[] roles) => roles
            .Select(role => new Claim(ClaimTypes.Role, role))
            .ToArray();

    private static Claim[] GetClaims(UserIdentity userIdentity)
    {
        Claim[] claims =
        {
            new Claim("kat", "B"),
            new Claim("kat", "C"),

            new Claim(ClaimTypes.Name, userIdentity.Username),
            new Claim(ClaimTypes.MobilePhone, userIdentity.Phone),
            new Claim(ClaimTypes.Email, userIdentity.Email),

            new Claim("license", userIdentity.License)
        };

        return claims;
    }

    // dotnet add package System.IdentityModel.Tokens.Jwt
    public string Create(UserIdentity userIdentity)
    {
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        var claims = GetClaims(userIdentity);
        
        var identity = new ClaimsIdentity(claims);
        identity.AddClaims(GetRoleClaims(userIdentity.Roles));

        var secretKey = "your-256-bit-secret";
        var key = Encoding.ASCII.GetBytes(secretKey);

        var tokenHandler = new JwtSecurityTokenHandler();
        var credentials = new SymmetricSecurityKey(key);
        var singingCredentials = new SigningCredentials(credentials, SecurityAlgorithms.HmacSha256Signature);

        var tokenDesciptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = singingCredentials,
        };

        var securityToken = tokenHandler.CreateToken(tokenDesciptor);

        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}
