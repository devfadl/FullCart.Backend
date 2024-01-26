using FullCart.Application.Common.Interfaces;
using FullCart.Domain.Entities;

namespace FullCart.Application.Common.Services;

public class UserTransactionService : IUserTransactionService
{
    private readonly IApplicationDbContext _context;

    public UserTransactionService(IApplicationDbContext context)
    {
        _context = context;
    }
    public async void AddUserTransaction(UserTransaction userTransaction, CancellationToken cancellationToken)
    {
        await _context.UserTransaction.AddAsync(userTransaction);
    }
}
