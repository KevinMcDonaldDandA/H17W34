/*
*  File	    EFMovieRepository.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     8th March 2019
*  Purpose	Concrete Implementation for the Movie Repository using Entity Framework
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/
using System.Data.Entity;
using System.Linq;

namespace MoviesProject.Models
{
    public class EFMovieRepository : IMovieRepository
    {
        AppDbContext db = new AppDbContext();

        public IQueryable<Movie> Movies { get { return db.Movies; } }

        public bool CheckMovieExits(string title)
        {
            return db.Movies.Where(m => m.Title.Equals(title, System.StringComparison.InvariantCultureIgnoreCase)).Count() > 0;
        }

        public void Delete(Movie movie)
        {
            db.Movies.Remove(movie);
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        public Movie Find(int id)
        {
            return db.Movies.Find(id);
        }

        public Movie GetMovieDetails(int? id)
        {
            return db.Movies.Include(m => m.Director).Include(m => m.Actors).Where(m => m.ID == id).SingleOrDefault();
        }

        public Movie Save(Movie movie)
        {
            if(movie.ID == 0){
                db.Movies.Add(movie);
            }
            else
            {
                db.Entry(movie).State = EntityState.Modified;
            }
            db.SaveChanges();
            return movie;
        }
    }
}
//------------------------------- End of File -------------------------------​