﻿/*
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
        public ActionResult Create(Movie movie, HttpPostedFileBase file)
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
                    if (file != null && file.ContentLength > 0)
                    {
                        SaveFile(movie, file);
                    }
                    movieRepository.Save(movie);
                    return RedirectToAction("Index");
                }
            }

            return View();
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
        public ActionResult Edit(Movie movie, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if(file != null && file.ContentLength > 0)
                {
                    DeleteFile(movie);
                    SaveFile(movie, file);
                }
                
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