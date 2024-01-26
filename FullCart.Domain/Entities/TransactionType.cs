﻿namespace FullCart.Domain.Entities;

public class TransactionType
{
    public long Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }
    public virtual ICollection<UserTransaction> UserTransactions { get; set; }
}
