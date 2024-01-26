using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Auth.Login;

public record LoginCommand : IRequest<Result<AuthBrief>>
{
    public string UserName { get; set; }
    public string Password { get; set; }
}

public class LoginQueryHandler : IRequestHandler<LoginCommand, Result<AuthBrief>>
{
    private readonly IApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public LoginQueryHandler(IApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    public async Task<Result<AuthBrief>> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var user = await GetUser(command.UserName, command.Password);
        if (user is null)
        {
            return Result<AuthBrief>.Unauthorized(Localization.USER_UNAUTHORIZED);
        }
        var permissions = await GetUserPermissions(user.Id);

        if (permissions.Count == 0 || permissions is null)
        {
            return Result<AuthBrief>.Unauthorized(Localization.USER_UNAUTHORIZED);
        }

        var token = _tokenService.GenerateToken(user, permissions);
        var authBriedDto = new AuthBrief() { Token = token };
        return Result<AuthBrief>.Success(authBriedDto);
    }

    private async Task<Domain.Entities.User> GetUser(string userName, string password)
    {
        // Find the user with the given username.
        var user = await _context.Users.SingleOrDefaultAsync(u => u.Username == userName);

        if (user == null)
        {
            // If the user doesn't exist, return null.
            return null;
        }

        // Hash the given password with the user's salt.
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: user.PasswordSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        ));
        // If the hashed password matches the stored hash, return the user.
        if (hashedPassword == user.PasswordHash)
        {
            return await _context.Users
                             .Where(u => u.IsActive && u.Username.Equals(userName))
                             .Include(x => x.UserGroups)
                             .ThenInclude(x => x.Group)
                             .SingleOrDefaultAsync();
        }
        else
        {
            // If the password doesn't match, return null.
            return null;
        }
    }
    private async Task<List<int>> GetUserPermissions(Guid userId)
    {
        var groupPermission = await _context.UserGroups.Include(x => x.Group).ThenInclude(x => x.GroupPermissions)
             .Where(x => x.UserId.Equals(userId))
             .SelectMany(x => x.Group.GroupPermissions.Select(y => y.PermissionId)).ToListAsync();

        return groupPermission;
    }
}
