using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MoviesProject.Models
{
    public class EFActorRepository : IActorRepository
    {
        AppDbContext db = new AppDbContext();

        public IQueryable<Actor> Actors { get { return db.Actors; } }

        public void Delete(Actor actor)
        {
            db.Actors.Remove(actor);
            db.SaveChanges();
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }

        public Actor Find(int? id)
        {
            return db.Actors.Find(id);
        }

        public Actor Save(Actor actor)
        {
            if(actor.ID == 0)
            {
                db.Actors.Add(actor);
            }
            else
            {
                db.Entry(actor).State = EntityState.Modified;
            }
            db.SaveChanges();

            return actor;
        }
    }
}