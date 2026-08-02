using FinShark.api.Models;
using Microsoft.EntityFrameworkCore;

namespace FinShark.api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions opt) : base(opt)
    {
        
    }

    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
}