using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MoviesProject.Models
{
    public class EFMovieRepository : IMovieRepository
    {
        AppDbContext db = new AppDbContext();

        public IQueryable<Movie> Movies { get { return db.Movies; } }

        public IQueryable<Director> Directors { get { return db.Directors; } }

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
            return db.Movies.Include(m => m.Director).Where(m => m.ID == id).SingleOrDefault();
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