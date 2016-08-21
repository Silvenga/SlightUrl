namespace SlightUrl.Data.Postgres.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class V2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("public.ShortenedLinks", new[] { "Alias" });
            CreateIndex("public.ShortenedLinks", "Alias", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("public.ShortenedLinks", new[] { "Alias" });
            CreateIndex("public.ShortenedLinks", "Alias");
        }
    }
}
