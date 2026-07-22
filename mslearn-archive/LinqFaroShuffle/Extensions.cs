using System;
using System.IO;

namespace LinqFaroShuffle;

public static class Extensions
{
    public static IEnumerable<T> InterleaveSequenceWith<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        var firstIter = first.GetEnumerator();
        var secondIter = second.GetEnumerator();

        while (firstIter.MoveNext() && secondIter.MoveNext())
        {
            yield return firstIter.Current;
            yield return secondIter.Current;
        }
    }

    public static bool SequenceEquals<T>(this IEnumerable<T> first, IEnumerable<T> second)
    {
        var firstIter = first.GetEnumerator();
        var secondIter = second.GetEnumerator();

        while ((firstIter?.MoveNext() == true) && secondIter.MoveNext())
        {
            if ((firstIter.Current is not null) && !firstIter.Current.Equals(secondIter.Current))
            {
                return false;
            }
        }

        return true;
    }

    public static IEnumerable<T> LogQuery<T>(this IEnumerable<T> sequence, string tag)
    {
        using (var writer = File.AppendText("debug.log"))
        {
            writer.WriteLine($"executing query {tag}");
        }

        return sequence;
    }
}
