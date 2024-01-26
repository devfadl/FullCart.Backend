using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;
using FullCart.Domain.Enums;
using FullCart.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

using System.Text.Encodings.Web;
using System.Text.Json;

namespace FullCart.Application.Group.EventHandlers;

public class GroupCreatedEventHandler : INotificationHandler<GroupCreatedEvent>
{
    private readonly ILogger<GroupCreatedEventHandler> _logger;
    private readonly IUserTransactionService _userTransactionService;

    public GroupCreatedEventHandler(ILogger<GroupCreatedEventHandler> logger, IUserTransactionService userTransactionService)
    {
        _logger = logger;
        _userTransactionService = userTransactionService;
    }

    public Task Handle(GroupCreatedEvent notification, CancellationToken cancellationToken)
    {
        var group = notification.Group;

        var rawData = new
        {
            group.Name,
            group.Description,
            group.IsActive,
            group.Created,
        };

        var userTransaction = new UserTransaction();

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        userTransaction.TransactionTypeId = (long)TransactionTypeEnum.GroupCreated;
        userTransaction.ObjectId = group.Id.ToString();
        userTransaction.ObjectData = System.Text.Json.JsonSerializer.Serialize(rawData, options);
        userTransaction.CreatedBy = group.CreatedBy;

        _userTransactionService.AddUserTransaction(userTransaction, cancellationToken);

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

