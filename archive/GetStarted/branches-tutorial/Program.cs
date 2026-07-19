// ExploreIf();
// ExploreLoops();

int sum = 0;
for (int i = 1; i < 21; i++)
{
    if ((i % 3) == 0)
    {
        sum += i;
    }
}
Console.WriteLine($"Sum of all integers 1 to 20 divisible by 3 is {sum}");

void ExploreLoops()
{
    int counter = 0;
    do
    {
        Console.WriteLine($"Hello World! The counter is {counter}");
        counter++;
    } while (counter < 10);

    for (int index = 7; index <= 17; index++)
    {
        Console.WriteLine($"Hello World! The index is {index}");
    }

    for (int row = 1; row < 11; row++)
    {
        for (char column = 'a'; column < 'k'; column++)
        {
            Console.WriteLine($"The cell is ({row}, {column})");
        }
    }
}

void ExploreIf()
{
    int a = 5;
    int b = 3;
    if (a + b > 10)
    {
        Console.WriteLine("The answer is greater than 10.");
    }
    else
    {
        Console.WriteLine("The answer is not greater than 10.");
    }

    int c = 4;
    if ((a + b + c > 10) && (a == b))
    {
        Console.WriteLine("The answer is greater than 10");
        Console.WriteLine("And the first number is equal to the second");
    }
    else
    {
        Console.WriteLine("The answer is not greater than 10");
        Console.WriteLine("Or the first number is not equal to the second");
    }

    if ((a + b + c > 10) || (a == b))
    {
        Console.WriteLine("The answer is greater than 10");
        Console.WriteLine("Or the first number is equal to the second");
    }
    else
    {
        Console.WriteLine("The answer is not greater than 10");
        Console.WriteLine("And the first number is not equal to the second");
    }
}