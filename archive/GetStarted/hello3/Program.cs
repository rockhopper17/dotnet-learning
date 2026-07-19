// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

class Hello
{
	static void Main()
	{
		Console.WriteLine("Hello World");

		(double,int) t1 = (4.5,3);

		Console.WriteLine($"Tuple with elements {t1.Item1} and {t1.Item2}");

		var ys = new[] {-9,0,67,100};
		var (min,max) = FindMinMax(ys);
		Console.WriteLine($"Limits of [{string.Join(" ",ys)}] are {min} and {max}");

		// FindMinMax
		(int min, int max) FindMinMax(int[] input)
		{
			if (input is null || input.Length == 0)
			{
				throw new ArgumentException("Cannot find minimum and maximum of a null or empty array.");
			}

			var min = int.MaxValue;
			var max = int.MinValue;

			foreach (var i in input)
			{
				if (i < min)
				{
					min = i;
				}
				if (i > max)
				{
					max = i;
				}
			}

			return (min, max);
		}
	}
}
