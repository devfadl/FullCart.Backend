using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;
using FullCart.Domain.Enums;
using FullCart.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

using System.Text.Encodings.Web;
using System.Text.Json;

namespace FullCart.Application.Group.EventHandlers;

public class GroupUpdatedEventHandler : INotificationHandler<GroupUpdatedEvent>
{
    private readonly ILogger<GroupUpdatedEventHandler> _logger;
    private readonly IUserTransactionService _userTransactionService;

    public GroupUpdatedEventHandler(ILogger<GroupUpdatedEventHandler> logger, IUserTransactionService userTransactionService)
    {
        _logger = logger;
        _userTransactionService = userTransactionService;
    }

    public Task Handle(GroupUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var group = notification.Group;

        var rawData = new
        {
            group.Name,
            group.Description,
            group.IsActive,
            group.LastModified,
        };

        var userTransaction = new UserTransaction();

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        userTransaction.TransactionTypeId = (long)TransactionTypeEnum.GroupUpdated;
        userTransaction.ObjectId = group.Id.ToString();
        userTransaction.ObjectData = System.Text.Json.JsonSerializer.Serialize(rawData, options);
        userTransaction.CreatedBy = (Guid)group.LastModifiedBy;

        _userTransactionService.AddUserTransaction(userTransaction, cancellationToken);

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

