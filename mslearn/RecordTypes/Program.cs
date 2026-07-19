using System;
namespace RecordTypes;

class Program
{
    private static DailyTemperature[] data = [
        new DailyTemperature(HighTemp: 57, LowTemp: 30),
        new DailyTemperature(60, 35),
        new DailyTemperature(63, 33),
        new DailyTemperature(68, 29),
        new DailyTemperature(72, 47),
        new DailyTemperature(75, 55),
        new DailyTemperature(77, 55),
        new DailyTemperature(72, 58),
        new DailyTemperature(70, 47),
        new DailyTemperature(77, 59),
        new DailyTemperature(85, 65),
        new DailyTemperature(87, 65),
        new DailyTemperature(85, 72),
        new DailyTemperature(83, 68),
        new DailyTemperature(77, 65),
        new DailyTemperature(72, 58),
        new DailyTemperature(77, 55),
        new DailyTemperature(76, 53),
        new DailyTemperature(80, 60),
        new DailyTemperature(85, 66)
    ];
    
    static void Main()
    {
        foreach (var item in data)
        {
            Console.WriteLine(item);
        }

        var heatingDays = new HeatingDegreeDays(65, data);
        Console.WriteLine(heatingDays);

        var coolingDays = new CoolingDegreeDays(65, data);
        Console.WriteLine(coolingDays);

        var growingDays = coolingDays with { BaseTemp = 41 };
        Console.WriteLine(growingDays);

        List<CoolingDegreeDays> movingAccumulation = new();
        int rangeSize = (data.Length > 5) ? 5 : data.Length;
        for (int start = 0; start < data.Length - rangeSize; start++)
        {
            var fiveDayTotal = growingDays with { TempRecords = data[start..(start + rangeSize)] };
            movingAccumulation.Add(fiveDayTotal);
        }
        Console.WriteLine();
        Console.WriteLine("total degree days in last five days");
        foreach (var item in movingAccumulation)
        {
            Console.WriteLine(item);
        }

        var growDaysCopy = growingDays with { };
    }
}