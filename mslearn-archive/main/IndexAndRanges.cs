string[] words = [
                // index from start     index from end
    "first",    // 0                    ^10
    "second",   // 1                    ^9
    "third",    // 2                    ^8
    "fourth",   // 3                    ^7
    "fifth",    // 4                    ^6
    "sixth",    // 5                    ^5
    "seventh",  // 6                    ^4
    "eighth",   // 7                    ^3
    "ninth",    // 8                    ^2
    "tenth"     // 9                    ^1
];              // 10 (or words.Length) ^0

// Console.WriteLine($"last word is {words[^1]}");

// string[] wordSlice = words[1..4];  // 2nd to 4th
// string[] wordSlice = words[^2..^0];  // last two
// string[] wordSlice = words[..];  // all words
// string[] wordSlice = words[..];  // all words
// string[] wordSlice = words[..4];  // 1st to 4th
// string[] wordSlice = words[6..];  // 7th to end

Index idx = ^3;
Console.WriteLine(words[idx]);
Range rng = 1..4;
string[] wordSlice = words[rng];  // using Range type

foreach (var word in wordSlice)
{
    Console.WriteLine(word);
}
// Console.WriteLine();

int[][] jagged =
[
   [0, 1, 2, 3, 4, 5, 6, 7, 8, 9],
   [10,11,12,13,14,15,16,17,18,19],
   [20,21,22,23,24,25,26,27,28,29],
   [30,31,32,33,34,35,36,37,38,39],
   [40,41,42,43,44,45,46,47,48,49],
   [50,51,52,53,54,55,56,57,58,59],
   [60,61,62,63,64,65,66,67,68,69],
   [70,71,72,73,74,75,76,77,78,79],
   [80,81,82,83,84,85,86,87,88,89],
   [90,91,92,93,94,95,96,97,98,99],
];

var selectedRows = jagged[3..^3];
foreach (var row in selectedRows)
{
    var selectedColumns = row[2..^2];
    foreach (var cell in selectedColumns)
    {
        Console.Write($"{cell}, ");
    }
    Console.WriteLine();
}

(int min, int max, double avg) MovingAverage(int[] subSequence, Range range) =>
    (subSequence[range].Min(), subSequence[range].Max(), subSequence[range].Average());

int[] Sequence(int count) => [.. Enumerable.Range(0, count).Select(x => (int)(Math.Sqrt(x) * 100))];

int[] seq = Sequence(1000);

for (int start = 0; start < seq.Length; start+=100)
{
    // Range r = start..(start + 10);
    // var (min, max, avg) = MovingAverage(seq, r);
    // Console.WriteLine($"from {r.Start} to {r.End}:  \tmin: {min},\tmax: {max},\tavg: {avg}");
    
    Range r = ^(start+10)..^start;
    var (min, max, avg) = MovingAverage(seq, r);
    Console.WriteLine($"from {r.Start.GetOffset(seq.Length)} to {r.End.GetOffset(seq.Length)}:  \tmin: {min},\tmax: {max},\tavg: {avg}");
}