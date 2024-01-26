using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Entities;
using FullCart.Domain.Events;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Group.Commands.CreateGroup;

public class AddGroupPermissionCommand : IRequest<Result<bool>>
{
    public Guid GroupId { get; set; }
    public List<int> PermissionsIds { get; set; }
}

public class AddGroupPermissionCommandHandler : IRequestHandler<AddGroupPermissionCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly LoggedUser _loggedUser;

    public AddGroupPermissionCommandHandler(IApplicationDbContext context, LoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<Result<bool>> Handle(AddGroupPermissionCommand request, CancellationToken cancellationToken)
    {

        var _group = await _context.Groups.Where(p => p.Id == request.GroupId)
            .Include(p => p.GroupPermissions)
            .FirstOrDefaultAsync();

        if (_group == null)
        {
            return Result<bool>.NotFound(Localization.ERROR_NOT_FOUND);
        }

        _group.GroupPermissions.ToList().ForEach(p =>
        {
            p.IsDeleted = true;
            p.LastModified = DateTime.Now;
            p.LastModifiedBy = _loggedUser.Id;
        }
        );

        foreach (var id in request.PermissionsIds)
        {
            _group.GroupPermissions.Add(new GroupPermission
            {
                PermissionId = id,
                Created = DateTime.Now,
                CreatedBy = _loggedUser.Id,
            });
        }

        _group.AddDomainEvent(new GroupPermissionAddedEvent(_group.GroupPermissions));
        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);

    }
}
