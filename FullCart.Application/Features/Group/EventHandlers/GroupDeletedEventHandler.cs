using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;
using FullCart.Domain.Enums;
using FullCart.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace FullCart.Application.Group.EventHandlers;

public class GroupDeletedEventHandler : INotificationHandler<GroupDeletedEvent>
{
    private readonly ILogger<GroupDeletedEventHandler> _logger;
    private readonly IUserTransactionService _userTransactionService;
    public GroupDeletedEventHandler(ILogger<GroupDeletedEventHandler> logger, IUserTransactionService userTransactionService)
    {
        _logger = logger;
        _userTransactionService = userTransactionService;
    }

    public Task Handle(GroupDeletedEvent notification, CancellationToken cancellationToken)
    {
        var group = notification.Group;

        var rawData = new
        {
            group.IsDeleted,
            group.LastModified
        };

        var userTransaction = new UserTransaction();

        userTransaction.TransactionTypeId = (long)TransactionTypeEnum.GroupDeleted;
        userTransaction.ObjectId = group.Id.ToString();
        userTransaction.ObjectData = System.Text.Json.JsonSerializer.Serialize(rawData);
        userTransaction.CreatedBy = (Guid)group.LastModifiedBy;

        _userTransactionService.AddUserTransaction(userTransaction, cancellationToken);

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

