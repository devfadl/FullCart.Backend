using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;
using FullCart.Domain.Enums;
using FullCart.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace FullCart.Application.UserGroups.EventHandlers;

public class UserGroupAddedEventHandler : INotificationHandler<UserGroupAddedEvent>
{
    private readonly ILogger<UserGroupAddedEventHandler> _logger;
    private readonly IUserTransactionService _userTransactionService;

    public UserGroupAddedEventHandler(ILogger<UserGroupAddedEventHandler> logger, IUserTransactionService userTransactionService)
    {
        _logger = logger;
        _userTransactionService = userTransactionService;
    }

    public Task Handle(UserGroupAddedEvent notification, CancellationToken cancellationToken)
    {
        var groupIds = string.Join(',', notification.UserGroup.Select(p => p.GroupId));

        var rawData = new
        {
            groupIds,
        };

        var userTransaction = new UserTransaction();

        userTransaction.TransactionTypeId = (long)TransactionTypeEnum.UserGroupAdded;
        userTransaction.ObjectId = groupIds;
        userTransaction.ParentObjectId = notification.UserGroup.ToList().First().UserId.ToString();
        userTransaction.ObjectData = System.Text.Json.JsonSerializer.Serialize(rawData);
        userTransaction.CreatedBy = notification.UserGroup.ToList().First().CreatedBy;

        _userTransactionService.AddUserTransaction(userTransaction, cancellationToken);

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

