using EFCore.Data;
using EFCore.Domain;

var context = new FootballLeagueDbContext();

context.Leagues.Add(new League { Name = "Red Stripe Premiere League"});
await context.SaveChangesAsync();