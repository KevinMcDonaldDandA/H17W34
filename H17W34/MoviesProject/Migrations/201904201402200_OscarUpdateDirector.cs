namespace MoviesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OscarUpdateDirector : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directors", "OscarWinner", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directors", "OscarWinner");
        }
    }
}
