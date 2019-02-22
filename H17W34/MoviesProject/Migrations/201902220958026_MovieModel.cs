namespace MoviesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Summary = c.String(),
                        UserRating = c.Double(nullable: false),
                        DirectorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Directors", t => t.DirectorId, cascadeDelete: true)
                .Index(t => t.DirectorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "DirectorId", "dbo.Directors");
            DropIndex("dbo.Movies", new[] { "DirectorId" });
            DropTable("dbo.Movies");
        }
    }
}
