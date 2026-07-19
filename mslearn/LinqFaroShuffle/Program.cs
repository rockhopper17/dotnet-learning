using System;
using System.Collections.Generic;
using System.Linq;

using LinqFaroShuffle;

static IEnumerable<string> Suits()
{
    yield return "clubs";
    yield return "diamonds";
    yield return "hearts";
    yield return "spades";
}

static IEnumerable<string> Ranks()
{
    yield return "two";
    yield return "three";
    yield return "four";
    yield return "five";
    yield return "six";
    yield return "seven";
    yield return "eight";
    yield return "nine";
    yield return "ten";
    yield return "jack";
    yield return "queen";
    yield return "king";
    yield return "ace";
}

// var startingDeck =
//     from s in Suits()
//     from r in Ranks()
//     select new { Suit = s, Rank = r };
var startingDeck = Suits().SelectMany(suit =>
    Ranks().Select(rank => new { Suit = suit, Rank = rank }))
    .LogQuery("starting deck")
    .ToArray();

// startingDeck.ToList().ForEach(Console.WriteLine);

var top = startingDeck.Take(26);
var bottom = startingDeck.Skip(26);
var shuffle = top.InterleaveSequenceWith(bottom);

// shuffle.ToList().ForEach(Console.WriteLine);
foreach (var c in shuffle)
{
    Console.WriteLine(c);
}

var times = 0;
shuffle = startingDeck;

do
{
    // shuffle = shuffle.Take(26).InterleaveSequenceWith(shuffle.Skip(26));
    shuffle = shuffle.Skip(26).LogQuery("bottom half")
        .InterleaveSequenceWith(shuffle.Take(26).LogQuery("top half"))
        .LogQuery("shuffle")
        .ToArray();

    foreach (var card in shuffle)
    {
        Console.WriteLine(card);
    }
    Console.WriteLine();
    times++;
} while (!startingDeck.SequenceEqual(shuffle));

Console.WriteLine(times);
