using Grafdatabasen.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Grafdatabasen.Models;

namespace Grafdatabasen.Controllers
{
    public class WriteController : Controller
    {
        // GET: Write
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult uppdrag(AddUppdragViewModel vm)
        {
            var nyProj = new Uppdrag { Namn = vm.Namn, Problem = vm.Problem, Losning = vm.Losning, Resultat = vm.Resultat };
            WebApiConfig.GraphClient.Cypher
                .Merge("(uppdrag:Uppdrag { Namn: {Namn} })")
                .Set("uppdrag = {nyProj}")
                .WithParams(new
                {
                    Namn = nyProj.Namn,
                    nyProj
                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Match("(kund:Kund)", "(uppdrag:Uppdrag)", "(bestallare:Bestallare)")
                .Where((Kund kund) => kund.Namn == vm.Kund)
                .AndWhere((Uppdrag uppdrag) => uppdrag.Namn == vm.Namn)
                .AndWhere((Bestallare bestallare) => bestallare.Namn == vm.Bestallare)
                .CreateUnique("kund-[:BESTALLT]->uppdrag")
                .CreateUnique("bestallare-[:BESTALLT_AV]->kund")
                .ExecuteWithoutResults();

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
            var vm = new AddUppdragViewModel();

            return View(vm);
        }

        public ActionResult kund()
        {
            ViewBag.Message = "Lägg till/ändra kund";

            return View();
        }

        public ActionResult konsult()
        {
            ViewBag.Message = "Lägg till/ändra konsult";
            AddKonsultViewModel vm = new AddKonsultViewModel();
            return View(vm);
        }
        [HttpPost]
        public ActionResult konsult(AddKonsultViewModel vm)
        {
            var nyKonsult = new Konsult
            {
                Namn = vm.Namn,
                Titel = vm.Titel,
                Epost = vm.Epost,
                Telefonnummer = vm.Telefon,
                Link = vm.Link,
                Kontor = vm.Kontor,
                Beskrivning = vm.Beskrivning
            };
            var nyttKontor = new Kontor { Namn = vm.Kontor };
            
            WebApiConfig.GraphClient.Cypher
                .Merge("(konsult:Konsult { Namn: {Namn} })")
                .Set("konsult = {nyKonsult}")
                .WithParams(new
                {
                    Namn = nyKonsult.Namn,
                    nyKonsult

                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Merge("(kontor:Kontor { Namn: {Namn} })")
                .Set("kontor = {nyttKontor}")
                .WithParams(new
                {
                    Namn = nyttKontor.Namn,
                    nyttKontor

                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Match("(konsult:Konsult)", "(kontor:Kontor)")
                .Where((Konsult konsult) => konsult.Namn == vm.Namn)
                .AndWhere((Kontor kontor) => kontor.Namn == vm.Kontor)
                .CreateUnique("konsult-[:JOBBAR_På]->kontor")
                .ExecuteWithoutResults();


            return RedirectToAction("konsult");
        }
        public ActionResult showPartialUppgift()
        {
            return PartialView("_AddUppgiftPartial");
        }


        public ActionResult showPartialKund()
        {
            AddKundViewModel vm = new AddKundViewModel();
            return PartialView("_AddKundPartial", vm);
        }

        public ActionResult showPartialKonsult()
        {
            AddKonsultViewModel vm = new AddKonsultViewModel();
            return PartialView("_AddKonsultPartial", vm);
        }

        [HttpPost]
        public ActionResult SkapaNyKund(AddKundViewModel vm)
        {
            var nyKund = new Kund { Namn = vm.Namn, Adress = vm.Adress
                , Postadress = vm.Postadress, Kategori = vm.Kategori
                , Marknadssegment = vm.Segment, Telefon = vm.Telefon };
            var nyBestallare = new Bestallare { Namn = vm.Kontakt, Avdelning = vm.Avdelning
                , Epost = vm.Epost, Konto = vm.Konto, Telefon = vm.Telefon};
            WebApiConfig.GraphClient.Cypher
                .Merge("(kund:Kund { Namn: {Namn} })")
                .Set("kund = {nyKund}")
                .WithParams(new
                {
                    Namn = nyKund.Namn,
                    nyKund

                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Merge("(bestallare:Bestallare { Namn: {Namn} })")
                .Set("bestallare = {nyBestallare}")
                .WithParams(new
                {
                    Namn = nyBestallare.Namn,
                    nyBestallare

                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Match("(kund:Kund)", "(bestallare:Bestallare)")
                .Where((Kund kund) => kund.Namn == vm.Namn)
                .AndWhere((Bestallare bestallare) => bestallare.Namn == vm.Kontakt)
                .CreateUnique("kund-[:JOBBAR]->bestallare")
                .ExecuteWithoutResults();
            return RedirectToAction("uppdrag");
        }

        public ActionResult showRemoveKompetens()
        {
            AddKompetensViewModel vm = new AddKompetensViewModel();
            return PartialView("_RemoveKompetensPartial", vm);
        }


        public ActionResult showPartialKompetens()
        {
            AddKompetensViewModel vm = new AddKompetensViewModel();
            return PartialView("_AddKompetensPartial", vm);
        }

        [HttpPost]
        public ActionResult AddKompetens(AddKompetensViewModel vm)
        {
            var nyKompetens = new Kompetens { Namn = vm.Namn, Beskrivning = vm.Beskrivning, Typ = vm.Kompetenstyp };
            WebApiConfig.GraphClient.Cypher
                .Merge("(kompetens:Kompetens:" + vm.Kompetenstyp + " { Namn: {Namn} })")
                .Set("kompetens = {nyKompetens}")
                .WithParams(new
                {
                    Namn = nyKompetens.Namn,
                    nyKompetens

                })
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Match("(kompetens:Kompetens)", "(konsult:Konsult)")
                .Where((Kompetens kompetens) => kompetens.Namn == vm.Namn)
                .AndWhere((Konsult konsult) => konsult.Namn == vm.Konsult)
                .CreateUnique("konsult-[:KAN{Niva:'" + vm.Niva + "'}]->kompetens")
                .ExecuteWithoutResults();
            return RedirectToAction("konsult");
        }


        [HttpPost]
        public ActionResult RemoveKompetens(AddKompetensViewModel vm)
        {
            WebApiConfig.GraphClient.Cypher
                .Match("(kompetens:Kompetens)<-[r:KAN]-(konsult:Konsult)")
                .Where((Kompetens kompetens) => kompetens.Namn == vm.Namn)
                .AndWhere((Konsult konsult) => konsult.Namn == vm.Konsult)
                .Delete("r")
                .ExecuteWithoutResults();
            return RedirectToAction("konsult");
        }
    }
}