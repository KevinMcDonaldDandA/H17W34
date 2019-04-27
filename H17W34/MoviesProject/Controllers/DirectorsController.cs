/*
*  File	    DirectorsController.cs
*  Author	Kevin McDonald, Dundee & Angus College, kevin.mcdonald@dundeeandangus.ac.uk
*  Date     22nd February 2019
*  Purpose	The Controller class for our Director Entity
*  Notes    This class is responsible for the Actions that occur in response to URL Requests.
*
*       Copyright © Kevin McDonald 2019. All rights reserved.
*/

using MoviesProject.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace MoviesProject.Controllers
{
    public class DirectorsController : Controller
    {
        private IDirectorRepository context;

        public DirectorsController()
        {
            var appDbContext = new AppDbContext();
            context = new EFDirectorRepository(appDbContext);
        }

        // GET: Directors
        public ActionResult Index()
        {
            var directors = context.Directors.ToList();
            return View(directors);
        }

        // GET: Directors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = context.Find((int)id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // GET: Directors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Director director)
        {
            if (ModelState.IsValid)
            {
                bool directorExists = context.Directors.Where(d => d.FName.Equals(director.FName, System.StringComparison.InvariantCultureIgnoreCase) && d.LName.Equals(director.LName, System.StringComparison.InvariantCultureIgnoreCase)).Count() > 0;
                if (directorExists)
                {
                    ModelState.AddModelError(string.Empty, "Director Already Exists");
                    return View(director);
                }
                else
                {
                    context.Save(director);
                    return RedirectToAction("Index");
                }
                
            }

            return View();
        }

        // GET: Directors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = context.Find((int)id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // POST: Directors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Director director)
        {
            if (ModelState.IsValid)
            {
                context.Save(director);
                return RedirectToAction("Index");
            }
            return View(director);
        }

        // GET: Directors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Director director = context.Find((int)id);
            if (director == null)
            {
                return HttpNotFound();
            }
            return View(director);
        }

        // POST: Directors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Director director = context.Find((int)id);
            context.Delete(director);
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