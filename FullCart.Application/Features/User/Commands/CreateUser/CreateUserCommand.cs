using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Events;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;

using System.Security.Cryptography;

namespace FullCart.Application.User.Commands.CreateUser;

public record CreateUserCommand : IRequest<Result<Guid>>
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
    public string ThirdName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public string Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly LoggedUser _loggedUser;

    public CreateUserCommandHandler(IApplicationDbContext context, LoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (_context.Users.Any(p => p.Email == request.Email.Trim()))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.Email.Trim());
            return Result<Guid>.Failure(new string[] { err });
        }

        if (_context.Users.Any(p => p.Username == request.Username.Trim()))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.Username.Trim());
            return Result<Guid>.Failure(new string[] { err });
        }

        if (_context.Users.Any(p => p.PhoneNumber == request.PhoneNumber.Trim()))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.PhoneNumber.Trim());
            return Result<Guid>.Failure(new string[] { err });
        }

        var user = new Domain.Entities.User();

        user.Id = Guid.NewGuid();
        user.Username = request.Username.Trim();
        user.FirstName = request.FirstName.Trim();
        user.SecondName = request.SecondName.Trim();
        user.ThirdName = request.ThirdName.Trim();
        user.LastName = request.LastName.Trim();
        user.PhoneNumber = request.PhoneNumber.Trim();
        user.IsActive = request.IsActive;
        user.Email = request.Email.Trim();
        user.CreatedBy = _loggedUser.Id;
        user.Created = DateTime.Now;
        // Generate a new salt.
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Hash the password with the salt.
        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: request.Password.Trim(),
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8
        ));

        // Store the user's password hash and salt in the database.
        user.PasswordHash = hashedPassword;
        user.PasswordSalt = salt;
        await _context.Users.AddAsync(user);
        user.AddDomainEvent(new UserCreatedEvent(user));
        await _context.SaveChangesAsync(cancellationToken);
        return Result<Guid>.Success(user.Id);
    }
}