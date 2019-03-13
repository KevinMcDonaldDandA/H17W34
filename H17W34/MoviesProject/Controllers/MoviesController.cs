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
        private IMovieRepository movieRepository;
        private IDirectorRepository directorRepository;

        public MoviesController()
        {
            movieRepository = new EFMovieRepository();
            directorRepository = new EFDirectorRepository();
        }

        public MoviesController(IMovieRepository mockMovieRepo, IDirectorRepository mockDirectorRepo)
        {
            movieRepository = mockMovieRepo;
            directorRepository = mockDirectorRepo;
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = movieRepository.Movies.Include(m => m.Director);
            return View(movies.ToList());
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            //  Using a ViewModel
            MovieDirectors md = new MovieDirectors
            {
                Movie = new Movie(),
                Directors = directorRepository.Directors.ToList()
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
            Movie movie = movieRepository.GetMovieDetails(id);
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
                    Directors = directorRepository.Directors.ToList()
                };

                bool movieExists = movieRepository.CheckMovieExits(movie.Title);
                if (movieExists)
                {
                    ModelState.AddModelError(string.Empty, "Movie already exists in Database");
                    return View(md);
                }
                else
                {
                    movieRepository.Save(movie);
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
            Movie movie = movieRepository.Find((int)id);
            if (movie == null)
            {
                return HttpNotFound();
            }
            MovieDirectors md = new MovieDirectors
            {
                Movie = movie,
                Directors = directorRepository.Directors.ToList()
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
                movieRepository.Save(movie);
                return RedirectToAction("Index");
            }
            MovieDirectors md = new MovieDirectors
            {
                Movie = movie,
                Directors = directorRepository.Directors.ToList()
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
            Movie movie = movieRepository.Find((int)id);
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
            Movie movie = movieRepository.Find(id);
            movieRepository.Delete(movie);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            movieRepository.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
//------------------------------- End of File -------------------------------​