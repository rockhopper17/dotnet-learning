using System.Diagnostics;
using System.Diagnostics.Metrics;

public static class ChatDiagnostics
{
    public const string ServiceName = "SignalRWebpack.Chat";

    // trace source for custom spans
    public static readonly ActivitySource ChatActivitySource = new(ServiceName, "1.0.0");

    // metrics for message counting
    public static readonly Meter ChatMeter = new(ServiceName, "1.0.0");

    public static readonly Counter<int> MessageCounter = ChatMeter.CreateCounter<int>(
        "chat.messages.count",
        description: "total number of chat messages saved");
}