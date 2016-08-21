namespace SlightUrl.Data
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using SlightUrl.Data.Entities;
    using SlightUrl.Data.Interfaces;

    public class SlightContext : DbContext
    {
        public DbSet<ShortenedLink> ShortenedLinks { get; set; }
        public DbSet<ConfigurationProperty> ConfigurationProperties { get; set; }

        public SlightContext() : base(nameof(SlightContext))
        {
        }

        public SlightContext(string connectionStringOrName) : base(connectionStringOrName)
        {
        }

        public SlightContext(DbConnection connection) : base(connection, false)
        {
        }

        public virtual void Migrate()
        {
            throw new NotImplementedException();
        }

        public override int SaveChanges()
        {
            PreSave();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            PreSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void PreSave()
        {
            UpdateAuditTimestamps();
        }

        private void UpdateAuditTimestamps()
        {
            var entities = ChangeTracker.Entries()
                                         .Where(x => x.Entity is IAuditableEntity)
                                         .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified)
                                         .Select(x => new
                                         {
                                             Entity = x.Entity as IAuditableEntity,
                                             x.State
                                         });

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedOn = DateTimeOffset.Now;
                    entity.Entity.ModifiedOn = DateTimeOffset.Now;
                }

                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.ModifiedOn = DateTimeOffset.Now;
                }
            }
        }
    }
}