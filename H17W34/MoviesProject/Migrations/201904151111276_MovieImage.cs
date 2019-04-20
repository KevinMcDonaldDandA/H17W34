namespace MoviesProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Movies", "ImagePath");
        }
    }
}
