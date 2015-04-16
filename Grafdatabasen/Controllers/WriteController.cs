using Grafdatabasen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Grafdatabasen.Controllers
{
    public class WriteController : Controller
    {
        // GET: Write
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult kompetens()
        {
            ViewBag.Message = "Lägg till/ändra kompetens";

            return View();
        }

        public ActionResult uppdrag()
        {
            ViewBag.Message = "Lägg till/ändra uppdrag";

            return View();
        }

        public ActionResult kund()
        {
            ViewBag.Message = "Lägg till/ändra kund";

            return View();
        }

        public ActionResult konsult()
        {
            ViewBag.Message = "Lägg till/ändra konsult";

            return View();
        }

        public ActionResult showPartialUppgift()
        {
            return PartialView("_AddUppgiftPartial");
        }

        [HttpPost]
        public ActionResult AddUppgift()
        {
            // TODO:  Spara data.
            return RedirectToAction("uppdrag");
        }

        public ActionResult showPartialKund()
        {
            return PartialView("_AddKundPartial");
        }

        [HttpPost]
        public ActionResult AddKund()
        {
            // TODO:  Spara data.
            return RedirectToAction("uppdrag");
        }

        public ActionResult showPartialKompetens()
        {
            return PartialView("_AddKompetensPartial");
        }

        [HttpPost]
        public ActionResult AddKompetens()
        {
            // TODO:  Spara data.
            return RedirectToAction("konsult");
        }

    }
}