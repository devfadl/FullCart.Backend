using FullCart.Application.Common.Dto;
using FullCart.Application.Common.Interfaces;
using FullCart.Application.Common.Shared;
using FullCart.Domain.Events;
using FullCart.Domain.ValueObjects;

using MediatR;

namespace FullCart.Application.Group.Commands.CreateGroup;

public class CreateGroupCommand : IRequest<Result<Guid>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}

public class CreateGroupCommandHandler : IRequestHandler<CreateGroupCommand, Result<Guid>>
{
    private readonly IApplicationDbContext _context;
    private readonly LoggedUser _loggedUser;

    public CreateGroupCommandHandler(IApplicationDbContext context, LoggedUser loggedUser)
    {
        _context = context;
        _loggedUser = loggedUser;
    }

    public async Task<Result<Guid>> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
    {

        if (_context.Groups.Any(p => p.Name == request.Name.Trim()))
        {
            var err = string.Format(Localization.ALREADY_EXISTS, request.Name.Trim());
            return Result<Guid>.Failure(new string[] { err });
        }

        var group = new Domain.Entities.Group();

        group.Id = Guid.NewGuid();
        group.Name = request.Name.Trim();
        group.IsActive = request.IsActive;
        group.Description = request.Description.Trim();
        group.CreatedBy = _loggedUser.Id;
        group.Created = DateTime.Now;


        await _context.Groups.AddAsync(group);

        group.AddDomainEvent(new GroupCreatedEvent(group));
        await _context.SaveChangesAsync(cancellationToken);

        return Result<Guid>.Success(group.Id);

    }
}
