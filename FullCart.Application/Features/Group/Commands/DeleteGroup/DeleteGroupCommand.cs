using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Events;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Group.Commands.CreateGroup;

public record DeleteGroupCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
}

public class DeleteGroupCommandHandler : IRequestHandler<DeleteGroupCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly LoggedUser _loggedUser;

    public DeleteGroupCommandHandler(IApplicationDbContext context, LoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<Result<bool>> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
    {
        var _group = await _context.Groups.Where(p => p.Id == request.Id).FirstOrDefaultAsync();

        if (_group == null)
        {
            return Result<bool>.NotFound(Localization.ERROR_NOT_FOUND);
        }

        if (_context.UserGroups.Any(p => p.GroupId == _group.Id))
        {
            var err = string.Format(Localization.RELATED_DATA, _group.Name);
            return Result<bool>.Failure(new string[] { err });
        }

        _group.IsDeleted = true;
        _group.LastModifiedBy = _loggedUser.Id;
        _group.LastModified = DateTime.Now;

        _group.AddDomainEvent(new GroupDeletedEvent(_group));
        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);

    }
}
