using System;
using ConsumerVehicleRegistration;
using CommercialRegistration;
using LiveryRegistration;

namespace PatternMatching;

public class TollCalculator
{
    private enum TimeBand
    {
        MorningRush,
        Daytime,
        EveningRush,
        Overnight
    }

    private static bool IsWeekDay(DateTime timeOfToll) =>
        timeOfToll.DayOfWeek switch
        {
            DayOfWeek.Saturday => false,
            DayOfWeek.Sunday => false,
            _ => true
        };

    private static TimeBand GetTimeBand(DateTime timeOfToll) =>
        timeOfToll.Hour switch
        {
            < 6 or > 19 => TimeBand.Overnight,
            < 10 => TimeBand.MorningRush,
            < 16 => TimeBand.Daytime,
            _ => TimeBand.EveningRush
        };

    public decimal CalculateToll(object vehicle) =>
        vehicle switch
        {
            // Car { Passengers: 0 } => 2.00m + 0.50m,
            // Car { Passengers: 1 } => 2.0m,
            // Car { Passengers: 2 } => 2.0m - 0.50m,
            // Car => 2.00m - 1.0m,
            Car c => c.Passengers switch
            {
                0 => 2.00m + 0.5m,
                1 => 2.0m,
                2 => 2.0m - 0.5m,
                _ => 2.00m - 1.0m
            },
            // Car c => 2.00m,

            // Taxi { Fares: 0 } => 3.50m + 1.00m,
            // Taxi { Fares: 1 } => 3.50m,
            // Taxi { Fares: 2 } => 3.50m - 0.50m,
            // Taxi => 3.50m - 1.00m,
            Taxi t => t.Fares switch
            {
                0 => 3.50m + 1.00m,
                1 => 3.50m,
                2 => 3.50m - 0.50m,
                _ => 3.50m - 1.00m
            },
            // Taxi t => 3.50m,

            Bus b when ((double)b.Riders / (double)b.Capacity) < 0.50 => 5.00m + 2.00m,
            Bus b when ((double)b.Riders / (double)b.Capacity) > 0.90 => 5.00m - 1.00m,
            Bus => 5.00m,
            // Bus b => 5.00m,

            DeliveryTruck t when (t.GrossWeightClass > 5000) => 10.00m + 5.00m,
            DeliveryTruck t when (t.GrossWeightClass < 3000) => 10.00m - 2.00m,
            DeliveryTruck => 10.00m,
            // DeliveryTruck t => 10.00m,

            { } => throw new ArgumentException(message: "unknown vehicle type", paramName: nameof(vehicle)),
            null => throw new ArgumentNullException(nameof(vehicle))
        };

    public static decimal PeakTimePremiumIfElse(DateTime timeOfToll, bool inbound)
    {
        if ((timeOfToll.DayOfWeek == DayOfWeek.Saturday) || (timeOfToll.DayOfWeek == DayOfWeek.Sunday))
        {
            return 1.0m;
        }

        int hour = timeOfToll.Hour;
        if (hour < 6) { return 0.75m; }
        if (hour < 10) { return inbound ? 2.0m : 1.0m; }
        if (hour < 16) { return 1.5m; }
        if (hour < 20) { return inbound ? 1.0m : 2.0m; }

        return 0.75m; // overnight
    }

    public static decimal PeakTimePremiumFull(DateTime timeOfToll, bool inbound) =>
        (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
        {
            (true, TimeBand.MorningRush, true) => 2.00m,
            (true, TimeBand.MorningRush, false) => 1.00m,
            (true, TimeBand.Daytime, true) => 1.50m,
            (true, TimeBand.Daytime, false) => 1.50m,
            (true, TimeBand.EveningRush, true) => 1.00m,
            (true, TimeBand.EveningRush, false) => 2.00m,
            (true, TimeBand.Overnight, true) => 0.75m,
            (true, TimeBand.Overnight, false) => 0.75m,
            (false, TimeBand.MorningRush, true) => 1.00m,
            (false, TimeBand.MorningRush, false) => 1.00m,
            (false, TimeBand.Daytime, true) => 1.00m,
            (false, TimeBand.Daytime, false) => 1.00m,
            (false, TimeBand.EveningRush, true) => 1.00m,
            (false, TimeBand.EveningRush, false) => 1.00m,
            (false, TimeBand.Overnight, true) => 1.00m,
            (false, TimeBand.Overnight, false) => 1.00m,
        };

    public static decimal PeakTimePremium(DateTime timeOfToll, bool inbound) =>
        (IsWeekDay(timeOfToll), GetTimeBand(timeOfToll), inbound) switch
        {
            (true, TimeBand.Overnight, _) => 0.75m,
            (true, TimeBand.Daytime, _) => 1.5m,
            (true, TimeBand.MorningRush, true) => 2.0m,
            (true, TimeBand.EveningRush, false) => 2.0m,
            _ => 1.0m
        };
}
