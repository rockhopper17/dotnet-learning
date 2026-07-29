using System.Globalization;

int x  = 8;
Foo(ref x);
Console.WriteLine(x);

static void Foo (ref int p)
{
    p++;
    Console.WriteLine(p);
}

// ====================================================
// int[,] matrix = new int[3,3];
// for (int i = 0; i < matrix.GetLength(0); i++)
// {
//     for (int j = 0; j < matrix.GetLength(1); j++)
//     {
//         matrix[i,j] = i * 3 + j;
//     }
// }

// int[,] matrix = new int[,] { {0,1,2},{3,4,5},{6,7,8} };

// for (int i = 0; i < matrix.GetLength(0); i++)
// {
//     for (int j = 0; j < matrix.GetLength(1); j++)
//         Console.Write(matrix[i,j] + " ");
//     Console.Write('\n');
// }

// int[] a = new int[1000];
// Console.WriteLine(a[123]);

// char[] vowels = new char[5];
// vowels[0] = 'a';
// vowels[1] = 'e';
// vowels[2] = 'i';
// vowels[3] = 'o';
// vowels[4] = 'u';

// char[] vowels = {'a','e','i','o','u'};
// Console.WriteLine(vowels[^0]);  // error
// char[] vowels = ['a','e','i','o','u'];
// Index first = 0;
// Index last = ^1;
// Range firstTwo = 0..2;
// Range lastThree = 2..;
// Console.WriteLine(vowels[first]);
// Console.WriteLine(vowels[last]);
// Console.WriteLine(vowels[^2]);
// Console.WriteLine(vowels[firstTwo]);
// Console.WriteLine(vowels[lastThree]);
// Console.WriteLine(vowels[2..3]);

// for (int i = 0; i < vowels.Length; i++)
// {
//     Console.WriteLine(vowels[i]);
// }

// ====================================================
// Point p1 = new Point();
// p1.X = 7;

// Point p2 = p1; // copy
// p2.X = 17;

// Console.WriteLine(p1.X);
// Console.WriteLine(p2.X);

// public struct Point { public int X, Y; }

// ====================================================
// Panda p1 = new Panda("pan dee");
// Panda p2 = new Panda("pan dah");

// Console.WriteLine(p1.Name);
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