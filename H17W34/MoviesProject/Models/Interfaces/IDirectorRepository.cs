/*
*  File	    IDirectorRepository.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     8th March 2019
*  Purpose	Interface Methods for the Director Repositories
*  Notes    
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/
using System.Linq;

namespace MoviesProject.Models
{
    public interface IDirectorRepository
    {
        IQueryable<Director> Directors { get; }
        Director Save(Director director);
        Director Find(int id);
        void Delete(Director director);
        void Dispose(bool disposing);
    }
}
//------------------------------- End of File -------------------------------​