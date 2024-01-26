using FullCart.Domain.Entities;

namespace FullCart.Application.Common.Interfaces;

public interface IUserTransactionService
{
    void AddUserTransaction(UserTransaction userTransaction, CancellationToken cancellationToken);
}
