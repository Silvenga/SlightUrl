namespace SlightUrl.Data.Postgres
{
    using System.Data.Entity;
    using System.Data.Entity.Migrations;

    using SlightUrl.Data.Postgres.Migrations;

    public class PostgresSlightContext : SlightContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }

        public override void Migrate()
        {
            var migratorConfig = new Configuration();
            var dbMigrator = new DbMigrator(migratorConfig);
            dbMigrator.Update();
        }
    }
}