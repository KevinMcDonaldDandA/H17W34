/*
*  File	    IMovieRepository.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     8th March 2019
*  Purpose	Interface Methods for the Movie Repositories
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/
using System.Linq;

namespace MoviesProject.Models
{
    public interface IMovieRepository
    {
        IQueryable<Movie> Movies { get; }
        IQueryable<Director> Directors { get; }
        Movie Save(Movie movie);
        Movie Find(int id);
        void Delete(Movie movie);
        void Dispose(bool disposing);
        Movie GetMovieDetails(int? id);
        bool CheckMovieExits(string title);
    }
}
//------------------------------- End of File -------------------------------​