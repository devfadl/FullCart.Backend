using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;
using FullCart.Domain.Enums;
using FullCart.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace FullCart.Application.GroupPermissions.EventHandlers;

public class GroupPermissionAddedEventHandler : INotificationHandler<GroupPermissionAddedEvent>
{
    private readonly ILogger<GroupPermissionAddedEventHandler> _logger;
    private readonly IUserTransactionService _userTransactionService;

    public GroupPermissionAddedEventHandler(ILogger<GroupPermissionAddedEventHandler> logger, IUserTransactionService userTransactionService)
    {
        _logger = logger;
        _userTransactionService = userTransactionService;
    }

    public Task Handle(GroupPermissionAddedEvent notification, CancellationToken cancellationToken)
    {
        var permissionIds = string.Join(',', notification.GroupPermission.Select(p => p.PermissionId));

        var rawData = new
        {
            permissionIds,
        };

        var userTransaction = new UserTransaction();

        userTransaction.TransactionTypeId = (long)TransactionTypeEnum.GroupPermissionAdded;
        userTransaction.ObjectId = permissionIds;
        userTransaction.ParentObjectId = notification.GroupPermission.ToList().First().GroupId.ToString();
        userTransaction.ObjectData = System.Text.Json.JsonSerializer.Serialize(rawData);
        userTransaction.CreatedBy = notification.GroupPermission.ToList().First().CreatedBy;

        _userTransactionService.AddUserTransaction(userTransaction, cancellationToken);

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

