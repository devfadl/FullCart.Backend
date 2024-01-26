namespace FullCart.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateToken(FullCart.Domain.Entities.User user, List<int> permissions);
}