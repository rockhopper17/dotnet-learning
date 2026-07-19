namespace Classes;

public class BankAccount
{
    private static int s_accountNumberSeed = 1234567890;
    private List<Transaction> _allTransactions = new List<Transaction>();
    private readonly decimal _minimumBalance;

    public string Number { get; }
    public string Owner { get; set; }
    // public decimal Balance { get; }
    public decimal Balance
    {
        get
        {
            decimal balance = 0;

            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }

            return balance;
        }
    }

    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }

    public BankAccount(string name, decimal initialBalance, decimal minimumBalance)
    {
        Number  = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;
        
        Owner = name;
        // this.Balance = initialBalance;
        _minimumBalance = minimumBalance;
        if (initialBalance > 0)
            MakeDeposit(initialBalance, "Initial balance");
    }

    public void MakeDeposit(decimal amount, string note, DateTime? date = null)
    {
        if (amount <= 0 )
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be positive");
        }

        // DateTime ldate = date ?? DateTime.Now;

        var deposit = new Transaction(amount, date ?? DateTime.Now, note);
        _allTransactions.Add(deposit);
    }

    public void MakeWithdrawal(decimal amount, string note, DateTime? date = null)
    {
        if (amount <= 0 )
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of withdrawal must be positive");
        }

        // if (Balance - amount < _minimumBalance )
        // {
        //     throw new InvalidOperationException("Not sufficient funds for this withdrawal");
        // }
        //
        // var withdrawal = new Transaction(-amount, date ?? DateTime.Now, note);
        // _allTransactions.Add(withdrawal);

        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date ?? DateTime.Now, note);

        _allTransactions.Add(withdrawal);

        if (overdraftTransaction != null)
            _allTransactions.Add(overdraftTransaction);
    }

    protected virtual Transaction? CheckWithdrawalLimit(bool isOverdrawn)
    {
        if (isOverdrawn)
        {
            throw new InvalidOperationException("not sufficient funds for this withdrawal");
        }
        else
        {
            return default;
        }
    }

    public string GetAccountHistory()
    {
        var report  = new System.Text.StringBuilder();

        decimal balance = 0;
        report.AppendLine("Date\t\tAmount\tBalance\tNote");
        foreach (var item  in _allTransactions)
        {
            balance += item.Amount;
            report.AppendLine($"{item.Date.ToShortDateString()}\t{item.Amount}\t{balance}\t{item.Notes}");
        }

        return report.ToString();
    }

    public virtual void PerformMonthEndTransactions() { }
}