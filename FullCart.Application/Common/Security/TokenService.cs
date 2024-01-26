using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FullCart.Application.Common.Security;

public class TokenService : ITokenService
{

    private readonly AppSetting _appSetting;
    public TokenService(AppSetting appSetting)
    {
        _appSetting = appSetting;
    }

    public string GenerateToken(Domain.Entities.User user, List<int> permissions)
    {
        var key = _appSetting.APISecurity.Key;

        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim("UserId", user.Id.ToString()));
        claims.Add(new Claim("Full Name", $"{user.FirstName} {user.SecondName} {user.ThirdName} {user.LastName}"));
        claims.Add(new Claim("userName", user.Username));
        claims.Add(new Claim("First Name", user.FirstName));
        claims.Add(new Claim("Last Name", user.LastName));
        claims.Add(new Claim("Group", user.UserGroups.Select(x => x.Group.Name).FirstOrDefault()));

        foreach (var permissionId in permissions)
        {
            claims.Add(new Claim(ClaimTypes.Role, permissionId.ToString()));
        }

        SigningCredentials signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)), SecurityAlgorithms.HmacSha256);

        // Create the JWT and write it to a string

        JwtSecurityToken jwt = new JwtSecurityToken(
            issuer: _appSetting.APISecurity.Issuer,
            audience: _appSetting.APISecurity.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromHours(_appSetting.APISecurity.Expiration)),
            signingCredentials: signingCredentials);
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);
        return token;

    }
}



