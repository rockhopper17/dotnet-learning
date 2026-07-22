namespace Classes;

public class BankAccount
{
    // ---------------------------------
    // constants
    // ---------------------------------
    private static int s_accountNumberSeed = 1234567890;

    // ---------------------------------
    // fields
    // ---------------------------------
    private List<Transaction> _allTransacctions = new List<Transaction>();
    private readonly decimal _minimumBalance;

    // ---------------------------------
    // properties
    // ---------------------------------
    public string Number { get; }
    public string Owner { get; set; }
    // public decimal Balance { get; }
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransacctions)
            {
                balance += item.Amount;
            }

            return balance;
        }
    }

    // ---------------------------------
    // constructors
    // ---------------------------------
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }

    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        // this.Owner = name;
        // this.Balance = initialBalance;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        Owner = name;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
            MakeDeposit(initialBalance, DateTime.Now, "initial balance");
    }

    // ---------------------------------
    // methods
    // ---------------------------------

    // public abstract void PerformMonthEndTransactions();
    public virtual void PerformMonthEndTransactions() { }

    public void MakeDeposit(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "amount of deposit must be greater than zero");
        }
        var deposit = new Transaction(amount, date, note);
        _allTransacctions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, DateTime date, string note)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "amount of withdrawal must be greater than zero");
        }
        // if (Balance - amount < 0)
        // if (Balance - amount < _minimumBalance)
        // {
        //     throw new InvalidOperationException("insufficient funds");
        // }

        // var withdrawal = new Transaction(-amount, date, note);
        // _allTransacctions.Add(withdrawal);

        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note);
        _allTransacctions.Add(withdrawal);
        if (overdraftTransaction != null)
            _allTransacctions.Add(overdraftTransaction);
    }

    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("insufficient funds");
        }
        else
        {
            return default;
        }
    }

    public string GetAccountHistory()
    {
        var report = new System.Text.StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item in _allTransacctions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }
}