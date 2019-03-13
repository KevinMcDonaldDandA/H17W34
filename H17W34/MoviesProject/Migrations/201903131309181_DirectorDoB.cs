namespace MoviesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DirectorDoB : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Directors", "DoB", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Directors", "DoB");
        }
    }
}
