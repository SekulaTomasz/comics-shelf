using comics_shelf_api.core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace comics_shelf_api.core.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users{ get; set; }

		public override int SaveChanges()
		{
			UpdateAuditEntities();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			UpdateAuditEntities();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateAuditEntities();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
		{
			UpdateAuditEntities();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private void UpdateAuditEntities()
		{
			var modifiedEntries = ChangeTracker.Entries()
				.Where(x => x.Entity is Entity && (x.State == EntityState.Added || x.State == EntityState.Modified));

			var entityEntries = modifiedEntries as IList<EntityEntry> ?? modifiedEntries.ToList();
			var random = new Random();
			foreach (var entry in entityEntries)
			{
				var entity = (Entity)entry.Entity;
				DateTime now = DateTime.UtcNow;

				if (entry.State == EntityState.Added)
				{
					if (entity.GetType() == typeof(User))
					{
						((User)entity).Coins = random.Next(1, 100);
					}

					entity.Id = Guid.NewGuid();
					entity.CreatedAt = now;
				}
				if (entry.State == EntityState.Modified)
				{
					entity.UpdatedAt = now;
				}

				
			}
		}
	}
}
