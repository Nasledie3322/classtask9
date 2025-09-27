public class SavingsAccount : BankAccount
{
    public SavingsAccount(string id, string holderName, string currency)
        : base(id, holderName, currency) {}

    public override void Withdraw(decimal amount)
    {
        Transaction a = new Transaction
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
            a.Status = "Rejected";
            a.Reason = "Сумма должна быть положительной";
            AddTransaction(a);
            return;
        }

        if (amount > Balance)
        {
            a.Status = "Rejected";
            a.Reason = "Недостаточно средств";
            AddTransaction(a);
            return;
        }

        Balance -= amount;
        a.Status = "Completed";
        AddTransaction(a);
    }
}