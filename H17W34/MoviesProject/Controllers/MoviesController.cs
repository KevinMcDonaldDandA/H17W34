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
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MoviesProject.Controllers
{
    public class MoviesController : Controller
    {
        private IMovieRepository movieRepository;
        private IDirectorRepository directorRepository;
        private IActorRepository actorRepository;

        public MoviesController()
        {
            var appDbContext = new AppDbContext();
            movieRepository = new EFMovieRepository(appDbContext);
            directorRepository = new EFDirectorRepository(appDbContext);
            actorRepository = new EFActorRepository(appDbContext);
        }

        public MoviesController(IMovieRepository mockMovieRepo, IDirectorRepository mockDirectorRepo)
        {
            movieRepository = mockMovieRepo;
            directorRepository = mockDirectorRepo;
        }

        // GET: Movies
        // Reference Documentation for Search Functionality: https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/introduction/adding-search
        public ActionResult Index(string search)
        {
            var movies = movieRepository.Movies.Include(m => m.Director);
            if (!String.IsNullOrEmpty(search))
            {
                movies = movies.Where(s => s.Title.Contains(search));
                ViewBag.Search = true;
            }
            return View(movies.ToList());
        }

        // GET: Movies/Create
        public ActionResult Create()
        {
            //  Using a ViewModel
            CreateMovieVM md = new CreateMovieVM
            {
                Movie = new Movie(),
                Directors = directorRepository.Directors.ToList()
            };
            var allActors = actorRepository.Actors.ToList();

            md.AllActors = allActors.Select(a => new SelectListItem
            {
                Text = a.Fullname,
                Value = a.ID.ToString()
            });

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
        public ActionResult Create(CreateMovieVM cm, HttpPostedFileBase file)
        {
            if (cm == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);



            if (ModelState.IsValid)
            {
                bool movieExists = movieRepository.CheckMovieExits(cm.Movie.Title);
                if (movieExists)
                {
                    ModelState.AddModelError(string.Empty, "Movie already exists in Database");
                    return View(cm);
                }

                var movieToCreate = cm.Movie;
                
                    //  Gets record from db - presumably to put in on the content.
                    //  var usedJobTags = _db.JobTags.Where(m => jobpostView.SelectedJobTags.Contains(m.Id)).ToList();


                    //  retrieves the jobIds from the VM
                    var selectedActors = new HashSet<int>(cm.SelectedActors);
                    //  This seems super inefficient - Basically it removes tags from the job then readds the selected ones.
                    //  Iterates through EVERY tag in the DB.
                    //  Code processing intensive - generally faster than DB operations however.
                    foreach (Actor actor in actorRepository.Actors)
                    {
                        if (!selectedActors.Contains(actor.ID))
                        {
                            if (movieToCreate.Actors.Contains(actor))
                            {
                                movieToCreate.Actors.Remove(actor);
                            }
                        }
                        else
                        {
                            if (!movieToCreate.Actors.Contains(actor))
                            {
                                movieToCreate.Actors.Add(actor);
                            }
                        }
                    if (file != null && file.ContentLength > 0)
                    {
                        SaveFile(movieToCreate, file);
                    }
                    movieRepository.Save(movieToCreate);
                    
                }

                return RedirectToAction("Index");
            }
            return View(cm);
        }

        private void SaveFile(Movie movie, HttpPostedFileBase file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var path = Path.Combine(Server.MapPath("~/Images/"), fileName);
            file.SaveAs(path);
            
            movie.ImagePath = file.FileName;
        }

        // GET: Movies/Edit/5
        public ActionResult Edit(int? id)
        {
            var cm = new CreateMovieVM
            {
                Movie = movieRepository.Find((int)id),
                Directors = directorRepository.Directors.ToList()
            };


            if (cm.Movie == null)
                return HttpNotFound();

            var allActors = actorRepository.Actors.ToList();

            cm.AllActors = allActors.Select(a => new SelectListItem {
                Text = a.Fullname,
                Value = a.ID.ToString()
            });
            
            return View(cm);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateMovieVM cm, HttpPostedFileBase file)
        {
            if (cm == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            if (ModelState.IsValid)
            {
                bool movieExists = movieRepository.CheckMovieExits(cm.Movie.Title);

                var movieToUpdate = movieRepository.Movies
                    .Include(i => i.Actors).First(i => i.ID == cm.Movie.ID);

                var selectedActors = new HashSet<int>(cm.SelectedActors);
                foreach (Actor actor in actorRepository.Actors)
                {
                    if (!selectedActors.Contains(actor.ID))
                    {
                        if (movieToUpdate.Actors.Contains(actor))
                        {
                            movieToUpdate.Actors.Remove(actor);
                        }
                    }
                    else
                    {
                        if (!movieToUpdate.Actors.Contains(actor))
                        {
                            movieToUpdate.Actors.Add(actor);
                        }
                    }                  
                }
                movieRepository.Save(movieToUpdate);
                if (file != null && file.ContentLength > 0)
                {
                    SaveFile(movieToUpdate, file);
                }

                return RedirectToAction("Index");
            }
            return View(cm);
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
            DeleteFile(movie);
            return RedirectToAction("Index");
        }

        private void DeleteFile(Movie movie)
        {
            var fullPath = Request.MapPath("~/Images/" + movie.ImagePath);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
        }

        protected override void Dispose(bool disposing)
        {
            movieRepository.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
//------------------------------- End of File -------------------------------​