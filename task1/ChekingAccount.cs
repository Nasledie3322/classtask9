public class CheckingAccount : BankAccount
{
    public decimal OverdraftLimit { get; private set; }

    public CheckingAccount(string id, string holderName, string currency, decimal overdraftLimit)
        : base(id, holderName, currency)
    {
        OverdraftLimit = overdraftLimit;
    }

    public override void Withdraw(decimal amount)
    {
        Transaction tx = new Transaction
        {
            Id = Guid.NewGuid().ToString(),
            Type = "Withdraw",
            Amount = amount,
            Currency = Currency,
            FromAccountId = Id,
            ToAccountId = null,
            CreatedAt = DateTime.Now,
            Status = "Pending"
        };

        if (amount <= 0)
        {
            tx.Status = "Rejected";
            tx.Reason = "Сумма должна быть положительной";
            AddTransaction(tx);
            return;
        }

        if (Balance - amount < -OverdraftLimit)
        {
            tx.Status = "Rejected";
            tx.Reason = "Превышен лимит овердрафта";
            AddTransaction(tx);
            return;
        }

        Balance -= amount;
        tx.Status = "Completed";
        AddTransaction(tx);
    }
}