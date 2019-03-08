using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesProject.Models
{
    public class EFDirectorRepository : IDirectorRepository
    {
        AppDbContext db = new AppDbContext();

        public IQueryable<Director> Directors { get { return db.Directors; } }

        public void Delete(Director director)
        {
            db.Directors.Remove(director);
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        public Director Find(int id)
        {
            return db.Directors.Find(id);
        }

        public Director Save(Director director)
        {
            if (director.ID == 0)
            {
                db.Directors.Add(director);
            }
            else
            {
                db.Entry(director).State = EntityState.Modified;
            }
            db.SaveChanges();
            return director;
        }
    }
}