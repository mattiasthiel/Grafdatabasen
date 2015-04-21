using Grafdatabasen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grafdatabasen.Models;

namespace Grafdatabasen.Controllers
{
    public class SearchController : Controller
    {
        // GET: Write
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult createSearchString(SearchViewModel vm)
        {

            return RedirectToAction("Search");
        }


    }
}
