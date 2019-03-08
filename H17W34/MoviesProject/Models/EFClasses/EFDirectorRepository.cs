/*
*  File	    EFMovieRepository.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     8th March 2019
*  Purpose	Concrete Implementation for the Director Repository using Entity Framework
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/
using System.Data.Entity;
using System.Linq;

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
//------------------------------- End of File -------------------------------​