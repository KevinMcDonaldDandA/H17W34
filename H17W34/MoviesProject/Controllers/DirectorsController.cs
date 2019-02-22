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
using System.Linq;
using System.Web.Mvc;

namespace MoviesProject.Controllers
{
    public class DirectorsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Directors
        public ActionResult Index()
        {
            var directors = db.Directors.ToList();
            return View(directors);
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