
// ---------------------
// LINQ
// ---------------------

City[] cities = [
    new City("Tokyo", 37_833_000),
    new City("Delhi", 30_290_000),
    new City("Shanghai", 27_110_000),
    new City("São Paulo", 22_043_000),
    new City("Mumbai", 20_412_000),
    new City("Beijing", 20_384_000),
    new City("Cairo", 18_772_000),
    new City("Dhaka", 17_598_000),
    new City("Osaka", 19_281_000),
    new City("New York-Newark", 18_604_000),
    new City("Karachi", 16_094_000),
    new City("Chongqing", 15_872_000),
    new City("Istanbul", 15_029_000),
    new City("Buenos Aires", 15_024_000),
    new City("Kolkata", 14_850_000),
    new City("Lagos", 14_368_000),
    new City("Kinshasa", 14_342_000),
    new City("Manila", 13_923_000),
    new City("Rio de Janeiro", 13_374_000),
    new City("Tianjin", 13_215_000)
];

Country[] countries = [
    new Country ("Vatican City", 0.44, 526, [new City("Vatican City", 826)]),
    new Country ("Monaco", 2.02, 38_000, [new City("Monte Carlo", 38_000)]),
    new Country ("Nauru", 21, 10_900, [new City("Yaren", 1_100)]),
    new Country ("Tuvalu", 26, 11_600, [new City("Funafuti", 6_200)]),
    new Country ("San Marino", 61, 33_900, [new City("San Marino", 4_500)]),
    new Country ("Liechtenstein", 160, 38_000, [new City("Vaduz", 5_200)]),
    new Country ("Marshall Islands", 181, 58_000, [new City("Majuro", 28_000)]),
    new Country ("Saint Kitts & Nevis", 261, 53_000, [new City("Basseterre", 13_000)])
];

// query syntax
// IEnumerable<City> cityQuery =
//     from city in cities
//     where city.Population > 30_000_000
//     select city;

// method syntax
// IEnumerable<City> cityQuery = cities.Where(c => c.Population > 30_000_000);

// foreach (City city in cityQuery)
// {
//     Console.WriteLine(city);
// }

// var largeCitiesList = (
//     from country in countries
//     from city in country.Cities
//     where city.Population > 10000
//     select city
// ).ToList();

IEnumerable<City> largeCitiesQuery =
    from country in countries
    from city in country.Cities
    where city.Population > 10000
    select city;
var largeCitiesList = largeCitiesQuery.ToList();

largeCitiesList.ForEach(Console.WriteLine);
// foreach (var item in largeCitiesList)
// {
//     Console.WriteLine(item);
// }

var cityGroups =
    from city in cities
    group city by city.Name[0] into g
    orderby g.Key
    select g;

foreach (var cityGroup in cityGroups)
{
    Console.WriteLine($"cities that start with letter '{cityGroup.Key}'");
    foreach (var city in cityGroup)
    {
        Console.WriteLine(city);
    }
}

var queryNameAndPop =
    from country in countries
    select new
    {
        Name = country.Name,
        Pop = country.Population
    };
var clist = queryNameAndPop.ToList();
clist.ForEach(Console.WriteLine);

int[] scores = [90, 71, 82, 93, 75, 82];

// var highestScore = (
//     from score in scores
//     select score
// ).Max();

IEnumerable<int> scoreQuery =
    from score in scores
    select score;
var highestScore = scoreQuery.Max();

Console.WriteLine(highestScore);

// IEnumerable<int> scoreQuery =
//     from score in scores
//     where score > 80
//     orderby score descending
//     select score;

// foreach (var testScore in scoreQuery)
// {
//     Console.Write(testScore + " ");
// }
// Console.WriteLine();

record City(string Name, long Population);
record Country(string Name, double Area, long Population, List<City> Cities);
record Product(string Name, string Category);

// ---------------------
// converting types
// ---------------------
// var g = new Giraffe();
// var a = new Animal();
// FeedMammals(g);
// FeedMammals(a);

// SuperNova sn = new SuperNova();
// TestForMammals(g);
// TestForMammals(sn);

// Giraffe g = new();
// UseIsOperator(g);
// UseAsOperator(g);
// UsePatternMatchingIs(g);

// SuperNova sn = new();
// UseAsOperator(sn);

// int i = 5;
// UseAsWithNullable(i);

// double d = 9.87654;
// UseAsWithNullable(d);

// static void UseIsOperator(Animal a)
// {
//     if (a is Mammal)
//     {
//         Mammal m = (Mammal)a;
//         m.Eat();
//     }
// }

// static void UsePatternMatchingIs(Animal a)
// {
//     if (a is Mammal m) m.Eat();
// }

// static void UseAsOperator(object o)
// {
//     Mammal? m = o as Mammal;
//     if (m is not null)
//         Console.WriteLine(m.ToString());
//     else
//         Console.WriteLine($"{o.GetType().Name} is not a mammal");
// }

// static void UseAsWithNullable(ValueType val)
// {
//     int? j = val as int?;
//     if (j is not null)
//         Console.WriteLine(j);
//     else
//         Console.WriteLine($"could not convert {val.ToString()}");
// }

// static void FeedMammals(Animal a)
// {
//     if (a is Mammal m)
//         m.Eat();
//     else
//         Console.WriteLine($"{a.GetType().Name} is not a mammal");
// }

// static void TestForMammals(object o)
// {
//     var m = o as Mammal;
//     if (m != null)
//         Console.WriteLine(m.ToString());
//     else
//         Console.WriteLine($"{o.GetType().Name} is not a mammal");
// }

// class Animal
// {
//     public void Eat() => Console.WriteLine("eating");
//     public override string ToString() => "i am an animal";
// }
// class Mammal : Animal { }
// class Giraffe : Mammal { }
// class SuperNova { }

// --------------------------------------------
// int i = 5;
// PatternMatchingNullable(i);

// int? j = null;
// PatternMatchingNullable(j);

// double d = 9.78654;
// PatternMatchingNullable(d);

// PatternMatchingSwitch(i);
// PatternMatchingSwitch(j);
// PatternMatchingSwitch(d);

// static void PatternMatchingNullable(ValueType? val)
// {
//     if (val is int j)
//         Console.WriteLine(j);
//     else if (val is null)
//         Console.WriteLine("val is nullable type with null value");
//     else
//         Console.WriteLine("could not convert " + val.ToString());
// }

// static void PatternMatchingSwitch(ValueType? val) =>
//     Console.WriteLine(val switch
//     {
//         int num => num,
//         long num => num,
//         decimal num => num,
//         float num => num,
//         double num => num,
//         null => "val is nullable type with null value",
//         _ => $"could not convert {val.ToString()}"
//     });

// ---------------------
// functional techniques
// ---------------------
// using System.Globalization;

// object?[] objects = [CultureInfo.CurrentCulture, CultureInfo.CurrentCulture.DateTimeFormat,
//                     CultureInfo.CurrentCulture.NumberFormat, new ArgumentException(), null];

// foreach (var obj in objects)
// {
//     ProvidesFormatInfo(obj);
// }

// static void ProvidesFormatInfo(object? obj) =>
//     Console.WriteLine(obj switch
//     {
//         IFormatProvider fmt => $"{fmt.GetType()} object",
//         null => "null obj and null ref except",
//         _ => "some obj without format info"
//     });

// ---------------------
// inheritance
// ---------------------
// using System.Reflection;

// public class SimpleClass { }

// public class SimpleClassExample
// {
//     public static void Main()
//     {
//         Type t = typeof(SimpleClass);
//         BindingFlags flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
//                              BindingFlags.NonPublic | BindingFlags.FlattenHierarchy;
//         MemberInfo[] members = t.GetMembers(flags);
//         Console.WriteLine($"type {t.Name} has {members.Length} members: ");
//         foreach (MemberInfo member in members)
//         {
//             string access = "";
//             string stat = "";
//             var method = member as MethodBase;
//             if (method != null)
//             {
//                 if (method.IsPublic)
//                     access = " public";
//                 else if (method.IsPrivate)
//                     access = " private";
//                 else if (method.IsFamily)
//                     access = " protected";
//                 else if (method.IsAssembly)
//                     access = " internal";
//                 else if (method.IsFamilyOrAssembly)
//                     access = " protected internal";

//                 if (method.IsStatic)
//                     stat = " static";
//             }
//             string output = $"{member.Name} ({member.MemberType}): {access}{stat}, declared by {member.DeclaringType}";
//             Console.WriteLine(output);
//         }
//     }
// }

// ---------------------
// object-oriented programming 
// ---------------------
// using System;

// // public class Person
// public struct Person
// {
//     public string Name { get; set; }
//     public int Age { get; set; }
//     public Person(string name, int age)
//     {
//         Name = name;
//         Age = age;
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         Person p1 = new Person("Homer", 35);
//         Console.WriteLine($"p1 name={p1.Name} age={p1.Age}");

//         Person p2 = p1;
//         p2.Name = "Molly";
//         p2.Age = 16;

//         Console.WriteLine($"p1 name={p1.Name} age={p1.Age}");
//         Console.WriteLine($"p2 name={p2.Name} age={p2.Age}");

//         Person p3 = new Person("Wallace", 75);
//         Console.WriteLine(p3.ToString());
//         Person p4 = new Person("", 42);
//         Console.WriteLine(p4.ToString());
//         p4.Name = "Wallace";
//         p4.Age = 75;
//         Console.WriteLine(p4.ToString());

//         if (p4.Equals(p3))
//             Console.WriteLine("p3 and p4 have same values");
//     }
// }

// ---------------------
// List collections
// ---------------------
// List<string> names = ["Drew", "Ana", "Felipe"];
// foreach (var name in names)
// {
//     Console.WriteLine($"Hello {name.ToUpper()}");
// }

// names.Add("Maria");
// names.Add("Bill");
// names.Remove("Ana");
// names.Sort();

// foreach (var name in names)
// {
//     Console.WriteLine($"Hello {name.ToUpper()}");
// }

// Console.WriteLine($"My name is {names[0]}");
// Console.WriteLine($"I've added {names[2]} and {names[3]} to the list");
// Console.WriteLine($"Ths list has {names.Count} people in it");

// var index = names.IndexOf("Felipe");
// if (index == -1)
// {
//     Console.WriteLine($"item not found, IndexOf returns {index}");
// }
// else
// {
//     Console.WriteLine($"name {names[index]} is at index {index}");
// }

// index = names.IndexOf("Not Found");
// if (index == -1)
// {
//     Console.WriteLine($"item not found, IndexOf returns {index}");
// }
// else
// {
//     Console.WriteLine($"name {names[index]} is at index {index}");
// }

// List<int> fibNums = [1, 1];

// // for (int i = 2; i < 20; i++)
// while(fibNums.Count < 20)
// {
//     fibNums.Add(fibNums[fibNums.Count - 1] + fibNums[fibNums.Count - 2]);
// }

// foreach (var item in fibNums)
// {
//     Console.WriteLine(item);
// }

// ---------------------
// record types
// ---------------------
// public record Point(int X, int Y)
// {
//     public double Slope() => (double)Y / (double)X;
// }

// public class Program
// {
//     public static void Main()
//     {
//         Point pt = new Point(1, 1);
//         var pt2 = pt with { Y = 10 };
//         Console.WriteLine($"the two points are {pt} and {pt2}");

//         double slope = pt.Slope();
//         Console.WriteLine($"the slope of {pt} is {slope}");
//     }
// }

// ---------------------
// tuples
// ---------------------

// var pt = (X: 1, Y: 2);

// var slope = (double)pt.Y / (double)pt.X;

// Console.WriteLine($"A line from the origin to the point {pt} has a slope of {slope}.");

// pt.X += 5;
// Console.WriteLine($"the point is now at {pt}.");

// var pt2 = pt with { Y = 10 };
// Console.WriteLine($"the point pt2 is at {pt2}.");

// var s = (A: 0, B: 0);
// s = pt;
// Console.WriteLine(s);

// var namedData = (Name: "morning observation", Temp: 17, Wind: 4);
// var person = (FirstName: "Homer", LastName: "Simpson");
// var order = (Product: "guitar picks", style: "triangle", quantity: 500, UnitPrice: 0.10m);

// Console.WriteLine(order.GetType());
// Console.WriteLine(namedData);

// using System;
// namespace TourOfCsharp;

// class Program
// {
//     static void Main()
//     {
//         Console.WriteLine("hello world");
//     }
// }
