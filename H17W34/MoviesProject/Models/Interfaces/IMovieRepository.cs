using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesProject.Models
{
    interface IMovieRepository
    {
        IQueryable<Movie> Movies { get; }
        IQueryable<Director> Directors { get; }
        Movie Save(Movie movie);
        Movie Find(int id);
        void Delete(Movie movie);
        void Dispose(bool disposing);
    }
}
