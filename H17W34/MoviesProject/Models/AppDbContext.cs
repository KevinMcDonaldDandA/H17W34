/*
*  File	    Director.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd February 2019
*  Purpose	The Model class responsible for our Database Interaction
*  Notes    Any new model you wish to include in the Database should be added here as a DbSet
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/
namespace MoviesProject.Models
{
    using System.Data.Entity;

    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext") { }

        public DbSet<Director> Directors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}
//------------------------------- End of File -------------------------------?