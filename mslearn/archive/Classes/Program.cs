using Classes;

var account = new BankAccount("Andrew", 1000);
Console.WriteLine($"account {account.Number} was created for {account.Owner} with {account.Balance} initial balance");

account.MakeWithdrawal(500, DateTime.Now, "rent payment");
Console.WriteLine(account.Balance);
account.MakeDeposit(100, DateTime.Now, "friend paid me back");
Console.WriteLine(account.Balance);

try
{
    account.MakeWithdrawal(750, DateTime.Now, "overdraw attempt");
}
catch (InvalidOperationException e)
{
    Console.WriteLine("exception caught trying to overdraw");
    Console.WriteLine(e.ToString());
}

BankAccount invalidAccount;
try
{
    invalidAccount = new BankAccount("invalid", -55);
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine("exception caught, negative balance");
    Console.WriteLine(e.ToString());
    // return;
}

Console.WriteLine(account.GetAccountHistory());

var giftCard = new GiftCardAccount("gift card", 100, 50);
giftCard.MakeWithdrawal(20, DateTime.Now, "get expensive coffee");
giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");
giftCard.PerformMonthEndTransactions();
// can make additional deposits
giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending money");
Console.WriteLine(giftCard.GetAccountHistory());

var savings = new InterestEarningAccount("savings account", 10000);
savings.MakeDeposit(750, DateTime.Now, "save some money");
savings.MakeDeposit(1250, DateTime.Now, "add more savings");
savings.MakeWithdrawal(250, DateTime.Now, "monthly bills");
savings.PerformMonthEndTransactions();
Console.WriteLine(savings.GetAccountHistory());

var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
// how much is too much to borrow?
lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "take out monthly advance");
lineOfCredit.MakeDeposit(50m, DateTime.Now, "pay back small amount");
lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "emergency funds for repairs");
lineOfCredit.MakeDeposit(150m, DateTime.Now, "partial restoration on repairs");
lineOfCredit.PerformMonthEndTransactions();
Console.WriteLine(lineOfCredit.GetAccountHistory());

// book inheritance tutorial
var book = new Book("Leviathan Wakes", "9780316129084", "James S. A. Corey", "Orbit books");
ShowPublicationInfo(book);
book.Publish(new DateTime(2011, 6, 15));
ShowPublicationInfo(book);

var book2 = new Book("Leviathan Wakes", "Public Domain Press", "James S. A. Corey");
Console.WriteLine($"{book.Title} and {book2.Title} are the same publication: " +
    $"{((Publication)book).Equals(book2)}");

static void ShowPublicationInfo(Publication pub)
{
    string pubDate = pub.GetPublicationDate();
    Console.WriteLine($"{pub.Title}, {(pubDate == "NYP" ? "Not Yet Published" : "published on " + pubDate):d} by {pub.Publisher}");
}

// shapes
Shape[] shapes = { new Rectangle(10, 12), new Square(5), new Circle(3) };
foreach (Shape shape in shapes)
{
    Console.WriteLine($"{shape}: area = {Shape.GetArea(shape)}; perimeter = {Shape.GetPerimeter(shape)}");

    if (shape is Rectangle rect)
    {
        Console.WriteLine($"   Is Square: {rect.IsSquare()}, Diagonal: {rect.Diagonal}");
        continue;
    }
    if (shape is Square sq)
    {
        Console.WriteLine($"   Diagonal: {sq.Diagonal}");
        continue;
    }
}