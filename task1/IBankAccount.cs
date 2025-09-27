public interface IBankAccount
{
    string Id { get; }
    string HolderName { get; set; }
    string Currency { get; }
    decimal Balance { get; }

    void Deposit(decimal amount);
    void Withdraw(decimal amount);
    List<Transaction> GetStatement();
}