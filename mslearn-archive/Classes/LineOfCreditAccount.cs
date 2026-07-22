using System;

namespace Classes;

public class LineOfCreditAccount : BankAccount
{
    public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit)
        : base(name, initialBalance, -creditLimit)
    {
    }

    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
        isOverdrawn ? new Transaction(-20, DateTime.Now, "apply overdraft fee") : default;

    public override void PerformMonthEndTransactions()
    {
        if (Balance < 0)
        {
            // negate balance to get positive interest charge
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.Now, "charge monthly interest");
        }
    }
}
