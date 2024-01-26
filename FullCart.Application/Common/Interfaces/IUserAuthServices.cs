using System.Security.Claims;

namespace FullCart.Application.Common.Interfaces;

public interface IUserAuthServices
{
    List<Claim> GenerateClaims(Domain.Entities.User user, List<int> permissions);
    Task<Domain.Entities.User> GetUser(string userName);
    Task<List<string>> GetUserPermissions(Guid userId);
}