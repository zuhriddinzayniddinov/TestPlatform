using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TestPlatform.Domain.Entities.Authentication;
using TestPlatform.Domain.Entities.Quizzes;
using TestPlatform.Domain.Entities.Sciences;
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
    public DbSet<ScienceTypes> Sciences { get; set; }
    public DbSet<Science> ScienceTypes { get; set; }
    public DbSet<Quiz> Quizzes { get; set; }
}