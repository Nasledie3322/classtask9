public class Program
{
    static List<IBankAccount> accounts = new List<IBankAccount>();

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Sozdat schot");
            Console.WriteLine("2. Prosmotret vse scheta");
            Console.WriteLine("3. Popolnit schot");
            Console.WriteLine("4. Snyat so schota");
            Console.WriteLine("5. Istoriya tranzaksii");
            Console.WriteLine("0. Vihod");
            Console.Write("Vedite nomer: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateAccount();
                    break;
                case "2":
                    ShowAccounts();
                    break;
                case "3":
                    Deposit();
                    break;
                case "4":
                    Withdraw();
                    break;
                case "5":
                    ShowTransactions();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Neverniy vibor");
                    break;
            }
        }
    }

    static void CreateAccount()
    {
        Console.Write("Vedite ID: ");
        string id = Console.ReadLine();
        Console.Write("Vedite imya vladelsa: ");
        string name = Console.ReadLine();
        Console.Write("Vedite valutu: ");
        string currency = Console.ReadLine();
        Console.Write("Tip (1 = Savings, 2 = Checking): ");
        string type = Console.ReadLine();

        if (type == "1")
        {
            accounts.Add(new SavingsAccount(id, name, currency));
        }
        else
        {
            Console.Write("Vedite overdraft limit: ");
            decimal limit = decimal.Parse(Console.ReadLine());
            accounts.Add(new CheckingAccount(id, name, currency, limit));
        }

        Console.WriteLine("Schot sozdan");
    }

    static void ShowAccounts()
    {
        foreach (var acc in accounts)
        {
            Console.WriteLine($"{acc.Id}: {acc.HolderName}, {acc.Currency}, Balance {acc.Balance}");
        }
    }

    static IBankAccount FindAccount(string id)
    {
        foreach (var acc in accounts)
        {
            if (acc.Id == id) return acc;
        }
        return null;
    }

    static void Deposit()
    {
        Console.Write("Vedite ID schota: ");
        string id = Console.ReadLine();
        IBankAccount acc = FindAccount(id);
        if (acc == null)
        {
            Console.WriteLine("Schet ne nayden");
            return;
        }
        Console.Write("Vedite summu: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        acc.Deposit(amount);
        Console.WriteLine("Popolneno");
    }

    static void Withdraw()
    {
        Console.Write("Vedite ID schota: ");
        string id = Console.ReadLine();
        IBankAccount acc = FindAccount(id);
        if (acc == null)
        {
            Console.WriteLine("Schet ne nayden");
            return;
        }
        Console.Write("Введите сумму: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        acc.Withdraw(amount);
        Console.WriteLine("Operatsiya vipolnena");
    }

    static void ShowTransactions()
    {
        Console.Write("Vedite ID schota: ");
        string id = Console.ReadLine();
        IBankAccount acc = FindAccount(id);
        if (acc == null)
        {
            Console.WriteLine("Schet ne nayden");
            return;
        }
        var list = acc.GetStatement();
        foreach (var tx in list)
        {
            Console.WriteLine($"[{tx.CreatedAt}] {tx.Type} {tx.Amount} {tx.Currency} {tx.Status} {tx.Reason}");
        }
    }
}