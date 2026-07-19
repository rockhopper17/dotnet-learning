using Microsoft.EntityFrameworkCore;

namespace SignalRWebpack.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<ChatLog> ChatLogs => Set<ChatLog>();
}