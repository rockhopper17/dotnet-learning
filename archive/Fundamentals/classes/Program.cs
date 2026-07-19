using Classes;

class Program
{
    static void Main(string[] args)
    {
        var account = new BankAccount("Homer Simpson", 1000);
        Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");

        account.MakeWithdrawal(500, "Rent paymeht");
        Console.WriteLine(account.Balance);

        account.MakeDeposit(100, "friend paid me back");
        Console.WriteLine(account.Balance);

        Console.WriteLine(account.GetAccountHistory());

        // ****************************************************************************************
        // test for a negative balance
        // ****************************************************************************************
        try
        {
            account.MakeWithdrawal(750, "attempt to overdraw");
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("exception caught trying to overdraw");
            Console.WriteLine(e.ToString());
            Console.WriteLine();
        }

        // ****************************************************************************************
        // test invalid account
        // ****************************************************************************************
        BankAccount invalidAccount;
        try
        {
            invalidAccount = new BankAccount("invald", -55);
        }
        catch (ArgumentOutOfRangeException e)
        {
            Console.WriteLine("exception caught creating account with negative balance");
            Console.WriteLine(e.ToString());
            Console.WriteLine();
            // return;
        }

        // ****************************************************************************************
        // test GiftCardAccount
        // ****************************************************************************************
        var giftCard = new GiftCardAccount("gift card", 100, 50);
        giftCard.MakeWithdrawal(20, "get expensive coffee");
        giftCard.MakeWithdrawal(50, "buy groceries");
        giftCard.PerformMonthEndTransactions();

        // can make additional deposits
        giftCard.MakeDeposit(27.50m, "add some additional spending money");

        Console.WriteLine(giftCard.GetAccountHistory());

        // ****************************************************************************************
        // test InterestEarningAccount
        // ****************************************************************************************
        var savings = new InterestEarningAccount("savings account", 10000);
        savings.MakeDeposit(750, "save some money");
        savings.MakeDeposit(1250, "add more savings");
        savings.MakeWithdrawal(250, "pay monthly bills");
        savings.PerformMonthEndTransactions();
        Console.WriteLine(savings.GetAccountHistory());
        
        // ****************************************************************************************
        // test LineOfCreditAccount
        // ****************************************************************************************
        var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
        lineOfCredit.MakeWithdrawal(1000m, "take out monthly advance");
        lineOfCredit.MakeDeposit(50m, "pay back small amounnt");
        lineOfCredit.MakeWithdrawal(5000m, "emergency funds for repairs");
        lineOfCredit.MakeDeposit(150m, "partial restoration on repairs");
        lineOfCredit.PerformMonthEndTransactions();
        Console.WriteLine(lineOfCredit.GetAccountHistory());

        // ****************************************************************************************
        return;
    }
}

