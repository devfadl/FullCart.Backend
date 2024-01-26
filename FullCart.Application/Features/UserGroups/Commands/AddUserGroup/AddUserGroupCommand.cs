using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Entities;
using FullCart.Domain.Events;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.UserGroups.Commands.AddUserGroup;

public class AddUserGroupCommand : IRequest<Result<bool>>
{
    public Guid UserId { get; set; }
    public List<Guid> GroupIds { get; set; }
}

public class AddUserGroupCommandHandler : IRequestHandler<AddUserGroupCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly LoggedUser _loggedUser;

    public AddUserGroupCommandHandler(IApplicationDbContext context, LoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<Result<bool>> Handle(AddUserGroupCommand request, CancellationToken cancellationToken)
    {

        var _user = await _context.Users.Where(p => p.Id == request.UserId)
            .Include(p => p.UserGroups)
            .FirstOrDefaultAsync();

        if (_user == null)
        {
            return Result<bool>.NotFound(Localization.ERROR_NOT_FOUND);
        }

        _user.UserGroups.ToList().ForEach(p =>
        {
            p.IsDeleted = true;
            p.LastModified = DateTime.Now;
            p.LastModifiedBy = _loggedUser.Id;
        }
        );

        foreach (var id in request.GroupIds)
        {
            _user.UserGroups.Add(new UserGroup
            {
                GroupId = id,
                Created = DateTime.Now,
                CreatedBy = _loggedUser.Id,
            });
        }

        _user.AddDomainEvent(new UserGroupAddedEvent(_user.UserGroups));
        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);

    }
}
