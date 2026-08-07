using Microsoft.EntityFrameworkCore;
using VideoGameCharacterApi.Entities;
using VideoGameCharacterApi.Models;

namespace VideoGameCharacterApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> opt) : DbContext(opt)
{
    public DbSet<Character> Characters => Set<Character>();

    public DbSet<User> Users { get; set; }
}