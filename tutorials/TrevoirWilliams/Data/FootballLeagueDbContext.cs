using EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Data;

public class FootballLeagueDbContext : DbContext
{
    public FootballLeagueDbContext(DbContextOptions<FootballLeagueDbContext> options) : base(options)
    {}

    public DbSet<Team> Teams { get; set; }
    public DbSet<League> Leagues { get; set; }
}