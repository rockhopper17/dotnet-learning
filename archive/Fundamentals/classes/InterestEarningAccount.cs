namespace Classes;

public class InterestEarningAccount : BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance)
        : base(name, initialBalance) { }

    public override void PerformMonthEndTransactions()
    {
        // base.PerformMonthEndTransactions();

        if (Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, "apply monthly interest");
        }
    }
}