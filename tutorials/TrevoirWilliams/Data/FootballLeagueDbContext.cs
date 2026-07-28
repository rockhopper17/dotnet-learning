using EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EFCore.Data;

public class FootballLeagueDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            // .SetBasePath(Directory.GetCurrentDirectory())
            // .AddJsonFile("appsettings.json")
            .AddUserSecrets<FootballLeagueDbContext>()
            .Build();

        var connectionString = configuration.GetConnectionString("FootballLeague");

        optionsBuilder.UseSqlServer(connectionString)
            .LogTo(Console.WriteLine, new[] {DbLoggerCategory.Database.Command.Name}, LogLevel.Information)
            .EnableSensitiveDataLogging();
    }

    public DbSet<Team> Teams { get; set; }
    public DbSet<League> Leagues { get; set; }
}