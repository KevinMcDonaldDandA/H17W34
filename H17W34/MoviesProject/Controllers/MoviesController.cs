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
        private IMovieRepository context;

        public MoviesController()
        {
            context = new EFMovieRepository();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = context.Movies.Include(m => m.Director);
            return View(movies.ToList());
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            //  Using a ViewModel
            MovieDirectors md = new MovieDirectors
            {
                Movie = new Movie(),
                Directors = context.Directors.ToList()
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
            Movie movie = context.Movies.Include(m => m.Director).Where(m => m.ID == id).SingleOrDefault();
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
                bool movieExists = context.Movies.Where(m => m.Title.Equals(movie.Title, System.StringComparison.InvariantCultureIgnoreCase)).Count() > 0;
                if (movieExists)
                {
                    ModelState.AddModelError(string.Empty, "Movie already exists in Database");
                    return View(movie);
                }
                else
                {
                    context.Save(movie);
                    return RedirectToAction("Index");
                }
            }

            MovieDirectors md = new MovieDirectors
            {
                Movie = movie,
                Directors = context.Directors.ToList()
            };
            return View(md);
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movie movie = context.Find((int)id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            MovieDirectors md = new MovieDirectors
            {
                Movie = movie,
                Directors = context.Directors.ToList()
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
                context.Save(movie);
                return RedirectToAction("Index");
            }
            MovieDirectors md = new MovieDirectors
            {
                Movie = movie,
                Directors = context.Directors.ToList()
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
            Movie movie = context.Find((int)id);
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
            Movie movie = context.Find(id);
            context.Delete(movie);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            context.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
//------------------------------- End of File -------------------------------​