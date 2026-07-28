
//---------------------------------------------------------------------
// Inheritence
//---------------------------------------------------------------------

Stock msft = new Stock { Name = "MSFT", SharesOwned = 1000 };
// Console.WriteLine(msft.Name);
Display(msft);
Console.WriteLine(msft.SharesOwned);

// Asset a = msft;

// if (a is Stock s) Console.WriteLine(s.SharesOwned);
// if (a is Stock s && s.SharesOwned > 100000)
//     Console.WriteLine(s.SharesOwned);
// else
//     s = new Stock();

// Console.WriteLine(s.SharesOwned);

// Console.WriteLine(a == msft);
// Console.WriteLine(a.Name);
// Stock s = (Stock)a;
// Console.WriteLine(s.SharesOwned);
// Console.WriteLine(s == a);
// Console.WriteLine(s == msft);

// House h = new House();
// Asset a = h;
// Stock s = (Stock)a;  // runtime error

House mansion = new House { Name = "Mansion", Mortgage = 250000 };
// Console.WriteLine(mansion.Name);
// Display(mansion);
// Console.WriteLine(mansion.Mortgage);
// Asset a = mansion;
// Console.WriteLine(mansion.Liability);
// Console.WriteLine(a.Liability);

Asset a1 = new Asset { Name = "House" };
Asset a3 = new Asset("condo");
Asset a2 = new Asset();  // compile error

static void Display(Asset asset)
{
    Console.WriteLine(asset.Name);
}

public class Asset
{
    public required string Name;
    public virtual decimal Liability => 0;  // expression bodied property

    public Asset() { }

    public Asset(string n) => Name = n;
}

public class Stock : Asset
{
    public long SharesOwned;
}
public class House : Asset
{
    public decimal Mortgage;
    public override decimal Liability => Mortgage;
}

//---------------------------------------------------------------------
// Classes
//---------------------------------------------------------------------
// Bunny b1 = new Bunny { Name = "Bo", LikesCarrots = true, LikesHumans = false };
// Bunny b2 = new Bunny("Bo") { LikesCarrots = true, LikesHumans = false };

// Console.WriteLine("qqq");

// public class Bunny
// {
//     public string Name;
//     public bool LikesCarrots, LikesHumans;

//     public Bunny() { }
//     public Bunny(string n) => Name = n;
// }

//---------------------------------------------------------------------
// Arrays
//---------------------------------------------------------------------
// int[,] matrix = new int[3, 3];

// for (int i = 0; i < matrix.GetLength(0); i++)
//     for (int j = 0; j < matrix.GetLength(1); j++)
//         matrix[i, j] = i * 3 + j;

// int[,] matrix2 = new int[,]
// {
//     {0,1,2 },
//     {3,4,5 },
//     {6,7,8 }
// };

// int x = 7;

//---------------------------------------------------------------------
// Indices and Ranges
//---------------------------------------------------------------------
// char[] vowels = new char[] { 'a', 'e', 'i', 'o', 'u' };
// char lastElement = vowels[^1];
// char secondToLast = vowels[^2];

// Console.ReadLine();

// char[] firstTwo = vowels[..2];
// char[] lastThree = vowels[2..];
// char[] middleOne = vowels[2..3];

// Console.ReadLine();
//---------------------------------------------------------------------
// Point[] a = new Point[1000];
// int x = a[500].X;

// public struct Point { public int X, Y; }
// public class Point { public int X, Y; }  // exception

//---------------------------------------------------------------------
// int x = 0, y = 0;
// Console.WriteLine(x++);
// Console.WriteLine(++y);
// Console.WriteLine(x);
// Console.WriteLine(y);

//---------------------------------------------------------------------
// Panda p1 = new Panda("Pan Dee");
// Console.WriteLine(p1.Name);
// Console.WriteLine(Panda.Population);

// Panda p2 = new Panda("Pan Dah");
// Console.WriteLine(p2.Name);
// Console.WriteLine(Panda.Population);

// public class Panda
// {
//     public string Name;
//     public static int Population;

//     public Panda(string n)
//     {
//         Name = n;
//         Population += 1;
//     }
// }