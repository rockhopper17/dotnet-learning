using System;
using System.Text;

namespace RecordTypes;

// single primary constructor
// public readonly record struct DailyTemperature(double HighTemp, double LowTemp);

// this single line primary constructor is shorthand for:
// public readonly struct DailyTemperature
// {
//     public double HighTemp { get; init; }
//     public double LowTemp { get; init; }

//     public DailyTemperature(double highTemp, double lowTemp)
//     {
//         HighTemp = highTemp;
//         LowTemp = lowTemp;
//     }

//     public void Deconstruct(out double highTemp, out double lowTemp)
//     {
//         highTemp = HighTemp;
//         lowTemp = LowTemp;
//     }
// }

public readonly record struct DailyTemperature(double HighTemp, double LowTemp)
{
    public double Mean => (HighTemp + LowTemp) / 2.0;
}

public abstract record DegreeDays(double BaseTemp, IEnumerable<DailyTemperature> TempRecords)
{
    protected virtual bool PrintMembers(StringBuilder sb)
    {
        sb.Append($"BaseTemp = {BaseTemp}");
        return true;
    }
}

public sealed record HeatingDegreeDays(double BaseTemp, IEnumerable<DailyTemperature> TempRecords)
    : DegreeDays(BaseTemp, TempRecords)
{
    public double TotalDegreeDays => TempRecords
        .Where(s => s.Mean < BaseTemp)
        .Sum(s => BaseTemp - s.Mean);

    // protected override bool PrintMembers(StringBuilder sb)
    // {
    //     base.PrintMembers(sb);
    //     sb.Append($", TotalDegreeDaysTTT = {TotalDegreeDays}");
    //     return true;
    // }
}

public sealed record CoolingDegreeDays(double BaseTemp, IEnumerable<DailyTemperature> TempRecords)
    : DegreeDays(BaseTemp, TempRecords)
{
    public double TotalDegreeDays => TempRecords
        .Where(s => s.Mean > BaseTemp)
        .Sum(s => s.Mean - BaseTemp);
}