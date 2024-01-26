
using FullCart.Application.Common.Interfaces;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Security.Claims;

namespace FullCart.Application.Common.Services;

public class UserAuthServices : IUserAuthServices
{
    private readonly IApplicationDbContext dbContext;

    public UserAuthServices(IServiceProvider provider)
    {
        dbContext = provider.GetRequiredService<IApplicationDbContext>();
    }

    public async Task<Domain.Entities.User> GetUser(string userName)
    {
        var user = await dbContext.Users.Where(x => x.Username.Equals(userName))
            .FirstOrDefaultAsync();
        return user;
    }

    public async Task<List<string>> GetUserPermissions(Guid userId)
    {
        var groupPermission = await dbContext.UserGroups.Include(x => x.Group).ThenInclude(x => x.GroupPermissions)
             .Where(x => x.UserId.Equals(userId))
             .SelectMany(x => x.Group.GroupPermissions.Select(y => y.Permission.Code)).ToListAsync();

        return groupPermission;
    }


    public List<Claim> GenerateClaims(Domain.Entities.User user, List<int> permissions)
    {
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim("UserId", user.Id.ToString()));
        claims.Add(new Claim("Full Name", $"{user.FirstName} {user.SecondName} {user.ThirdName} {user.LastName}"));
        claims.Add(new Claim("userName", user.Username));
        claims.Add(new Claim("First Name", user.FirstName));
        claims.Add(new Claim("Last Name", user.LastName));

        foreach (var permissionId in permissions)
        {
            claims.Add(new Claim(ClaimTypes.Role, permissionId.ToString()));
        }

        return claims;
    }
}
