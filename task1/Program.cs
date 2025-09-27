public class Program
{
    static List<IBankAccount> accounts = new List<IBankAccount>();

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Создать счёт");
            Console.WriteLine("2. Просмотреть все счета");
            Console.WriteLine("3. Пополнить счёт");
            Console.WriteLine("4. Снять со счёта");
            Console.WriteLine("5. История транзакций");
            Console.WriteLine("0. Выход");
            Console.Write("Введите номер: ");

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
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }

    static void CreateAccount()
    {
        Console.Write("Введите Id: ");
        string id = Console.ReadLine();
        Console.Write("Введите имя владельца: ");
        string name = Console.ReadLine();
        Console.Write("Введите валюту: ");
        string currency = Console.ReadLine();
        Console.Write("Тип (1 = Savings, 2 = Checking): ");
        string type = Console.ReadLine();

        if (type == "1")
        {
            accounts.Add(new SavingsAccount(id, name, currency));
        }
        else
        {
            Console.Write("Введите овердрафт лимит: ");
            decimal limit = decimal.Parse(Console.ReadLine());
            accounts.Add(new CheckingAccount(id, name, currency, limit));
        }

        Console.WriteLine("Счёт создан");
    }

    static void ShowAccounts()
    {
        foreach (var acc in accounts)
        {
            Console.WriteLine($"{acc.Id}: {acc.HolderName}, {acc.Currency}, Баланс {acc.Balance}");
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
        Console.Write("Введите Id счёта: ");
        string id = Console.ReadLine();
        IBankAccount acc = FindAccount(id);
        if (acc == null)
        {
            Console.WriteLine("Счёт не найден");
            return;
        }
        Console.Write("Введите сумму: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        acc.Deposit(amount);
        Console.WriteLine("Пополнено");
    }

    static void Withdraw()
    {
        Console.Write("Введите Id счёта: ");
        string id = Console.ReadLine();
        IBankAccount acc = FindAccount(id);
        if (acc == null)
        {
            Console.WriteLine("Счёт не найден");
            return;
        }
        Console.Write("Введите сумму: ");
        decimal amount = decimal.Parse(Console.ReadLine());
        acc.Withdraw(amount);
        Console.WriteLine("Операция выполнена");
    }

    static void ShowTransactions()
    {
        Console.Write("Введите Id счёта: ");
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