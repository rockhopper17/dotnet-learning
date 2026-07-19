using System;
using System.Text.Json.Serialization;

namespace WebAPIClient;

// public record class Repository(string Name);

public record class Repository(
    string Name,
    string Description,
    Uri GitHubHomeUrl,
    Uri Homepage,
    int Watchers,
    [property: JsonPropertyName("pushed_at")] DateTime LastPushUtc
)
{
    public DateTime LastPush => LastPushUtc.ToLocalTime();
}