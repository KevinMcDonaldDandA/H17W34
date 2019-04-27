using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MoviesProject.Models;

namespace MoviesProject.Controllers
{
    public class ActorsController : Controller
    {
        private IActorRepository actorRepository;

        public ActorsController()
        {
            var appDbContext = new AppDbContext();
            actorRepository = new EFActorRepository(appDbContext);
        }

        public ActorsController(IActorRepository mockRepository)
        {
            actorRepository = mockRepository;
        }

        // GET: Actors
        public ActionResult Index()
        {
            return View(actorRepository.Actors.ToList());
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = actorRepository.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // GET: Actors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Actor actor)
        {
            if (ModelState.IsValid)
            {
                actorRepository.Save(actor);
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        // GET: Actors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = actorRepository.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Actor actor)
        {
            if (ModelState.IsValid)
            {
                actorRepository.Save(actor);
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: Actors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = actorRepository.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
        }

        // POST: Actors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Actor actor = actorRepository.Find(id);
            actorRepository.Delete(actor);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            actorRepository.Dispose(disposing);
            base.Dispose(disposing);
        }
    }
}
