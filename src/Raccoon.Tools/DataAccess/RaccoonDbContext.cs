using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Raccoon.Tools.Entities;

namespace Raccoon.Tools.DataAccess;

public class RaccoonDbContext(DbContextOptions<RaccoonDbContext> options) : DbContext(options)
{
    public DbSet<ChatLog> ChatLogs { get; set; }

    public override int SaveChanges()
    {
        ModifiedEntity();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ModifiedEntity();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChatLog>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.FromModel);

            entity.Property(x => x.Extend)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions()),
                    v => JsonSerializer.Deserialize<Dictionary<string, string>>(v, new JsonSerializerOptions())
                );
        });
    }

    public void ModifiedEntity()
    {
        // 拦截IUpdatable和ICreatable俩个接口
        var entities = ChangeTracker.Entries().Where(x => x.Entity is IUpdatable || x.Entity is ICreatable);
        foreach (var entity in entities)
        {
            switch (entity.State)
            {
                case EntityState.Added:
                    ((ICreatable)entity.Entity).CreatedAt = DateTime.Now;
                    break;
                case EntityState.Modified:
                    ((IUpdatable)entity.Entity).UpdatedAt = DateTime.Now;
                    break;
                case EntityState.Detached:
                    break;
                case EntityState.Unchanged:
                    break;
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}