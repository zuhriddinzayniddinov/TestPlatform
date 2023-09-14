using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Domain.Entities.Authentication;
using TestPlatform.Domain.Entities.Users;

namespace TestPlatform.Infrastructure.Contexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            Assembly.GetExecutingAssembly());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Token> Tokens { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}