using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grafdatabasen.Controllers
{
    public class GrafController : Controller
    {
        // GET: Graf
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search()
        {
            ViewBag.Message = "Detta är då söksidan...";

            return View();
        }

        public ActionResult Write()
        {
            ViewBag.Message = "Här är inmatningssida 1.";

            return View();
        }

    }
}