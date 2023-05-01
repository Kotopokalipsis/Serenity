using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence;

public class ApplicationContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<RefreshTokenBlacklist> RefreshTokenBlacklists { get; set; }
    public DbSet<Profile> Profiles { get; set; }

    public ApplicationContext() : base() {}

    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) {}
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Migrations.json");
            var cfg = builder.Build();

            var connectionString = cfg["ConnectionStrings:MigrationConnection"];
            optionsBuilder.UseNpgsql(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfileConfiguration());
        modelBuilder.ApplyConfiguration(new RefreshTokenBlacklistConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}