using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Events;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.User.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? ThirdName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool? IsActive { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly LoggedUser _loggedUser;

    public UpdateUserCommandHandler(IApplicationDbContext context, LoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<Result<bool>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var _user = await _context.Users.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        if (_user == null)
        {
            return Result<bool>.NotFound(Localization.ERROR_NOT_FOUND);
        }


        if (request.Email != null && _context.Users.Any(p => p.Email == request.Email.Trim() && p.Id != request.Id))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.Email.Trim());
            return Result<bool>.Failure(new string[] { err });
        }

        if (request.Username != null && _context.Users.Any(p => p.Username == request.Username.Trim() && p.Id != request.Id))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.Username.Trim());
            return Result<bool>.Failure(new string[] { err });
        }


        if (request.PhoneNumber != null && _context.Users.Any(p => p.PhoneNumber == request.PhoneNumber.Trim() && p.Id != request.Id))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.PhoneNumber.Trim());
            return Result<bool>.Failure(new string[] { err });
        }

        _user.IsActive = request.IsActive ?? _user.IsActive;
        _user.FirstName = request.FirstName ?? _user.FirstName;
        _user.SecondName = request.SecondName ?? _user.SecondName;
        _user.ThirdName = request.ThirdName ?? _user.ThirdName;
        _user.LastName = request.LastName ?? _user.LastName;
        _user.PhoneNumber = request.PhoneNumber ?? _user.PhoneNumber;
        _user.Username = request.Username ?? _user.Username;
        _user.Email = request.Email ?? _user.Email;
        _user.LastModified = DateTime.Now;
        _user.LastModifiedBy = _loggedUser.Id;

        _user.AddDomainEvent(new UserUpdatedEvent(_user));
        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);

    }
}