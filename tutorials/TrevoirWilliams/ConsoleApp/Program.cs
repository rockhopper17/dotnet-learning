using EFCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .AddUserSecrets<Program>()  // add this
    .Build();

var connectionString = configuration.GetConnectionString("FootballLeague");

var optionsBuilder = new DbContextOptionsBuilder<FootballLeagueDbContext>();
optionsBuilder.UseSqlServer(connectionString);

using var context = new FootballLeagueDbContext(optionsBuilder.Options);

// now use context, e.g.:
var teams = context.Teams.ToList();