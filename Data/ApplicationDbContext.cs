using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrlShortner.Models.Entities;

namespace UrlShortner.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<UrlRecord> UrlRecord { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ApplyCreatedAndModifiedDates();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyCreatedAndModifiedDates()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
                }
                if (entity.State == EntityState.Modified)
                {
                    ((BaseEntity)entity.Entity).UpdatedAt = DateTime.UtcNow;
                }
            }
        }
    }
}