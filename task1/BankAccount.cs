public abstract class BankAccount : IBankAccount
{
    private List<Transaction> transactions = new List<Transaction>(); // вот здесь создаём список

    public string Id { get; private set; }
    public string HolderName { get; set; }
    public string Currency { get; private set; }
    public decimal Balance { get; protected set; }

    public BankAccount(string id, string holderName, string currency)
    {
        Id = id;
        HolderName = holderName;
        Currency = currency;
        Balance = 0;
        transactions = new List<Transaction>(); // инициализация ещё раз в конструкторе
    }

    public virtual void Deposit(decimal amount)
    {
        Transaction tx = new Transaction
        {
            Id = Guid.NewGuid().ToString(),
            Type = "Deposit",
            Amount = amount,
            Currency = Currency,
            FromAccountId = null,
            ToAccountId = Id,
            CreatedAt = DateTime.Now,
            Status = "Pending"
        };

        if (amount <= 0)
        {
            tx.Status = "Rejected";
            tx.Reason = "Сумма должна быть положительной";
            transactions.Add(tx);
            return;
        }

        Balance += amount;
        tx.Status = "Completed";
        transactions.Add(tx);
    }

    public abstract void Withdraw(decimal amount);

    public List<Transaction> GetStatement()
    {
        return transactions;
    }

    protected void AddTransaction(Transaction tx)
    {
        transactions.Add(tx);  // теперь точно не будет null
    }
}
