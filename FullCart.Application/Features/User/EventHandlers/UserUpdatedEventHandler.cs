using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;
using FullCart.Domain.Enums;
using FullCart.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

using System.Text.Encodings.Web;
using System.Text.Json;

namespace FullCart.Application.User.EventHandlers;

public class UserUpdatedEventHandler : INotificationHandler<UserUpdatedEvent>
{
    private readonly ILogger<UserUpdatedEventHandler> _logger;
    private readonly IUserTransactionService _userTransactionService;

    public UserUpdatedEventHandler(ILogger<UserUpdatedEventHandler> logger, IUserTransactionService userTransactionService)
    {
        _logger = logger;
        _userTransactionService = userTransactionService;
    }

    public Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        var user = notification.User;

        var rawData = new
        {
            user.IsActive,
            user.LastModified
        };

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        };

        var userTransaction = new UserTransaction();

        userTransaction.TransactionTypeId = (long)TransactionTypeEnum.UserUpdated;
        userTransaction.ObjectId = user.Id.ToString();
        userTransaction.ObjectData = System.Text.Json.JsonSerializer.Serialize(rawData, options);
        userTransaction.CreatedBy = (Guid)user.LastModifiedBy;

        _userTransactionService.AddUserTransaction(userTransaction, cancellationToken);

        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}

