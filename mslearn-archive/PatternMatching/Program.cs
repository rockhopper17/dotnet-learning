using System;
using PatternMatching;
using CommercialRegistration;
using ConsumerVehicleRegistration;
using LiveryRegistration;

var tollCalc = new TollCalculator();

var car = new Car();
var car0 = new Car { Passengers = 0 };
var car1 = new Car { Passengers = 1 };
var car2 = new Car { Passengers = 2 };
var car5 = new Car { Passengers = 5 };

var taxi = new Taxi();
var taxi0 = new Taxi { Fares = 0 };
var taxi1 = new Taxi { Fares = 1 };
var taxi2 = new Taxi { Fares = 2 };
var taxi5 = new Taxi { Fares = 5 };

var bus = new Bus();
var bus0 = new Bus { Capacity = 90, Riders = 15 };
var bus1 = new Bus { Capacity = 90, Riders = 75 };
var bus5 = new Bus { Capacity = 90, Riders = 85 };

var truck = new DeliveryTruck();
var truck0 = new DeliveryTruck { GrossWeightClass = 2500 };
var truck5 = new DeliveryTruck { GrossWeightClass = 7500 };

Console.WriteLine($"toll for car is {tollCalc.CalculateToll(car)}");
Console.WriteLine($"toll for car 0 is {tollCalc.CalculateToll(car0)}");
Console.WriteLine($"toll for car 1 is {tollCalc.CalculateToll(car1)}");
Console.WriteLine($"toll for car 2 is {tollCalc.CalculateToll(car2)}");
Console.WriteLine($"toll for car 5 is {tollCalc.CalculateToll(car5)}");

Console.WriteLine($"toll for taxi is {tollCalc.CalculateToll(taxi)}");
Console.WriteLine($"toll for taxi 0 is {tollCalc.CalculateToll(taxi0)}");
Console.WriteLine($"toll for taxi 1 is {tollCalc.CalculateToll(taxi1)}");
Console.WriteLine($"toll for taxi 2 is {tollCalc.CalculateToll(taxi2)}");
Console.WriteLine($"toll for taxi 5 is {tollCalc.CalculateToll(taxi5)}");

Console.WriteLine($"toll for bus is {tollCalc.CalculateToll(bus)}");
Console.WriteLine($"toll for bus 0 is {tollCalc.CalculateToll(bus0)}");
Console.WriteLine($"toll for bus 1 is {tollCalc.CalculateToll(bus1)}");
Console.WriteLine($"toll for bus 5 is {tollCalc.CalculateToll(bus5)}");

Console.WriteLine($"toll for truck is {tollCalc.CalculateToll(truck)}");
Console.WriteLine($"toll for truck 0 is {tollCalc.CalculateToll(truck0)}");
Console.WriteLine($"toll for truck 5 is {tollCalc.CalculateToll(truck5)}");

try
{
    tollCalc.CalculateToll("this will fail");
}
catch (ArgumentException ex)
{
    Console.WriteLine("argument exception when using wrong type");
}

try
{
    tollCalc.CalculateToll(null);
}
catch (ArgumentNullException ex)
{
    Console.WriteLine("null argument exception");
}

Console.WriteLine("testing time premiums");

var testTimes = new DateTime[]
{
    new(2019, 3, 4, 8, 0, 0), // morning rush
    new(2019, 3, 6, 11, 30, 0), // daytime
    new(2019, 3, 7, 17, 15, 0), // evening rush
    new(2019, 3, 14, 03, 30, 0), // overnight

    new(2019, 3, 16, 8, 30, 0), // weekend morning rush
    new(2019, 3, 17, 14, 30, 0), // weekend daytime
    new(2019, 3, 17, 18, 05, 0), // weekend evening rush
    new(2019, 3, 16, 01, 30, 0), // weekend overnight
};

Console.WriteLine("=========================================================================");
foreach (DateTime time in testTimes)
{
    Console.WriteLine($"inbound premiium at {time} is {TollCalculator.PeakTimePremiumIfElse(time, true).ToString("C")}");
    Console.WriteLine($"outbound premiium at {time} is {TollCalculator.PeakTimePremiumIfElse(time, false).ToString("C")}");
}
Console.WriteLine("=========================================================================");
foreach (DateTime time in testTimes)
{
    Console.WriteLine($"inbound premiium at {time} is {TollCalculator.PeakTimePremiumFull(time, true)}");
    Console.WriteLine($"outbound premiium at {time} is {TollCalculator.PeakTimePremiumFull(time, false)}");
}
Console.WriteLine("=========================================================================");
foreach (DateTime time in testTimes)
{
    Console.WriteLine($"inbound premiium at {time} is {TollCalculator.PeakTimePremium(time, true).ToString("C")}");
    Console.WriteLine($"outbound premiium at {time} is {TollCalculator.PeakTimePremium(time, false).ToString("C")}");
}
Console.WriteLine("=========================================================================");