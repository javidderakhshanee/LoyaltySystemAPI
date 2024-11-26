using LoyaltySystemApi.Domain.Base;
using LoyaltySystemApi.Domain.Entities.Points;
using LoyaltySystemApi.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LoyaltySystemApi.Infrastructure.EF;

public sealed class LoyaltySystemApiDbContext : DbContext
{
    public LoyaltySystemApiDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(),
            t => t.GetInterfaces().Any(x =>
                                        x.IsGenericType &&
                                        x.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>) &&
                                        typeof(BaseEntity).IsAssignableFrom(x.GenericTypeArguments[0])
            ));
    }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<PointEntity> Points { get; set; }
    public DbSet<UserPointEarnEntity> EarnedUserPoints { get; set; }
}
