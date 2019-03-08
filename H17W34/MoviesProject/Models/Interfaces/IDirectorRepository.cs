using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
