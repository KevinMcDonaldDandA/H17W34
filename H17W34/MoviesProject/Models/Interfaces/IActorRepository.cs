using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoviesProject.Models
{
    public interface IActorRepository
    {
        IQueryable<Actor> Actors { get; }
        Actor Save(Actor actor);
        Actor Find(int? id);
        void Delete(Actor actor);
        void Dispose(bool disposing);
    }
}
