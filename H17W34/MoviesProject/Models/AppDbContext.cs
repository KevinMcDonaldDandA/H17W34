namespace MoviesProject.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext") { }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
    }
}
