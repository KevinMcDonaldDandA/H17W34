/*
*  File	    MoviesController.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd February 2019
*  Purpose	The Controller class for our Movie Entity
*  Notes    This class is responsible for the Actions that occur in response to URL Requests.
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/

using MoviesProject.Models;
using MoviesProject.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MoviesProject.Controllers
{
    public class MoviesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Movies
        public ActionResult Index()
        {
            var movies = db.Movies.Include(m => m.Director);
            return View(movies.ToList());
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            //  Using a ViewModel
            MovieDirectors md = new MovieDirectors
            {
                Movie = new Movie(),
                Directors = db.Directors.ToList()
            };
            return View(md);
        }

        // GET: Movies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Include(m => m.Director).Where(m => m.ID == id).SingleOrDefault();
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                MovieDirectors md = new MovieDirectors
                {
                    Movie = movie,
                    Directors = db.Directors.ToList()
                };

                bool movieExists = db.Movies.Where(m => m.Title.Equals(movie.Title, System.StringComparison.InvariantCultureIgnoreCase)).Count() > 0;
                if (movieExists)
                {
                    ModelState.AddModelError(string.Empty, "Movie already exists in Database");
                    return View(md);
                }
                else
                {
                    db.Movies.Add(movie);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return View();
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            MovieDirectors md = new MovieDirectors
            {
                Movie = movie,
                Directors = db.Directors.ToList()
            };
            return View(md);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movie).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            MovieDirectors md = new MovieDirectors
            {
                Movie = movie,
                Directors = db.Directors.ToList()
            };
            return View(md);
        }

        // GET: Movies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = db.Movies.Find(id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Movie movie = db.Movies.Find(id);
            db.Movies.Remove(movie);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
//------------------------------- End of File -------------------------------​