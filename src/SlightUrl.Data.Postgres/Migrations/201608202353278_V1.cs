namespace SlightUrl.Data.Postgres.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class V1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "public.ConfigurationProperties",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "public.ShortenedLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Alias = c.String(),
                        Url = c.String(nullable: false),
                        CreatedOn = c.DateTimeOffset(precision: 7),
                        ModifiedOn = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Alias)
                .Index(t => t.Url);
            
        }
        
        public override void Down()
        {
            DropIndex("public.ShortenedLinks", new[] { "Url" });
            DropIndex("public.ShortenedLinks", new[] { "Alias" });
            DropTable("public.ShortenedLinks");
            DropTable("public.ConfigurationProperties");
        }
    }
}
