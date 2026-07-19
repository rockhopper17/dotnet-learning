using System.Net.Http.Headers;
using System.Net.Http.Json;
using WebAPIClient;

using HttpClient client = new();

client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json")
);
client.DefaultRequestHeaders.Add("User-agent", ".NET Foundation Repository Reporter");

// await ProcessRepositoriesAsync(client);
var repositories = await ProcessRepositoriesAsync(client);
foreach (var repo in repositories)
{
    Console.WriteLine($"name: {repo.Name}");
    Console.WriteLine($"homepage: {repo.Homepage}");
    Console.WriteLine($"github: {repo.GitHubHomeUrl}");
    Console.WriteLine($"description: {repo.Description}");
    Console.WriteLine($"watchers: {repo.Watchers:#,0}");
    Console.WriteLine($"{repo.LastPushUtc}");
    Console.WriteLine($"{repo.LastPush}");
    Console.WriteLine();
}

// async Task ProcessRepositoriesAsync(HttpClient client)
async Task<List<Repository>> ProcessRepositoriesAsync(HttpClient client)
{
    // var json = await client.GetStringAsync("https://api.github.com/orgs/dotnet/repos");
    var repositories = await client.GetFromJsonAsync<List<Repository>>("https://api.github.com/orgs/dotnet/repos");

    return repositories ?? new();

    // Console.Write(json);
    // foreach (var repo in repositories ?? Enumerable.Empty<Repository>())
    // {
    //     Console.WriteLine(repo.Name);
    // }
}
