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
    public DbSet<MedicalCard> MedicalCards { get; set; }
    public DbSet<Record> Records { get; set; }
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<ServiceType> ServiceTypes { get; set; }
    public DbSet<Tag> Tags { get; set; }

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
        modelBuilder.ApplyConfiguration(new MedicalCardConfiguration());
        modelBuilder.ApplyConfiguration(new RecordConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceCategoryConfiguration());
        modelBuilder.ApplyConfiguration(new ServiceTypeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}