using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Events;
using FullCart.Domain.ValueObjects;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FullCart.Application.Group.Commands.UpdateGroup;

public class UpdateGroupCommand : IRequest<Result<bool>>
{
    public Guid id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

}

public class UpdateGroupCommandHandler : IRequestHandler<UpdateGroupCommand, Result<bool>>
{
    private readonly IApplicationDbContext _context;
    private readonly LoggedUser _loggedUser;

    public UpdateGroupCommandHandler(IApplicationDbContext context, LoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<Result<bool>> Handle(UpdateGroupCommand request, CancellationToken cancellationToken)
    {
        var _group = await _context.Groups.Where(p => p.Id == request.id).FirstOrDefaultAsync();

        if (_group == null)
        {
            return Result<bool>.NotFound(Localization.ERROR_NOT_FOUND);
        }

        if (_context.Groups.Any(p => p.Name == request.Name.Trim() && p.Id != request.id))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.Name.Trim());
            return Result<bool>.Failure(new string[] { err });
        }

        _group.Name = request.Name.Trim();
        _group.Description = request.Description.Trim();
        _group.IsActive = request.IsActive;
        _group.LastModifiedBy = _loggedUser.Id;
        _group.LastModified = DateTime.Now;

        _group.AddDomainEvent(new GroupUpdatedEvent(_group));
        await _context.SaveChangesAsync(cancellationToken);

        return Result<bool>.Success(true);

    }
}
