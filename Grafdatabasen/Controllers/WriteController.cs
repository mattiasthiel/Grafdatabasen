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
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(uppdrag:Uppdrag)")
                .OptionalMatch("(uppdrag:Uppdrag)-[:DEL_AV]-(uppgift:Uppgift)-[:UTFÖR]-(konsult:Konsult)")
                .OptionalMatch("(uppgift:Uppgift)-[:I_ROLL]-(roll:Roll)")
                .Return((konsult, uppdrag, roll) => new
                {
                    Uppdrag = uppdrag.As<Uppdrag>(),
                    Konsult = konsult.CollectAs<Konsult>(),
                    Roll = roll.CollectAs<Roll>()
                })
                .OrderBy("Uppdrag.Namn")
                .Results;
            List<AddUppdragViewModel> listaUppdrag = new List<AddUppdragViewModel>();
            //AddKonsultViewModel vm = new AddKonsultViewModel();
            foreach (var item in result)
            {
                List<AddUppgiftViewModel> listaUppgifter = new List<AddUppgiftViewModel>();
                AddUppdragViewModel uppdrag = new AddUppdragViewModel();
                uppdrag.Namn = item.Uppdrag.Namn;
                uppdrag.Problem = item.Uppdrag.Problem;
                List<Roll> tempRollLista = new List<Roll>();
                foreach (var n in item.Roll){
                    Roll tempRoll = new Roll();
                    tempRoll.Namn = n.Data.Namn;
                    tempRollLista.Add(tempRoll);
                }
                List<Konsult> tempKonsultLista = new List<Konsult>();
                foreach (var n in item.Konsult)
                {
                    Konsult tempKonsult = new Konsult();
                    tempKonsult.Namn = n.Data.Namn;
                    tempKonsultLista.Add(tempKonsult);
                }
                for (var i = 0; i < tempKonsultLista.Count(); ++i)
                {
                    AddUppgiftViewModel uppgift = new AddUppgiftViewModel();
                    uppgift.Roll = tempRollLista[i].Namn;
                    uppgift.Konsult = tempKonsultLista[i].Namn;
                    listaUppgifter.Add(uppgift);
                }
                uppdrag.Uppgifter = listaUppgifter;
                listaUppdrag.Add(uppdrag);

            }

            return View(listaUppdrag);
        }

        public ActionResult EditUppdrag(string Namn)
        {
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(uppdrag:Uppdrag)")
                .Where((Uppdrag uppdrag) => uppdrag.Namn == Namn)
                .OptionalMatch("(uppdrag:Uppdrag)-[:DEL_AV]-(uppgift:Uppgift)-[:UTFÖR]-(konsult:Konsult)")
                .OptionalMatch("(uppgift:Uppgift)-[:I_ROLL]-(roll:Roll)")
                .OptionalMatch("(uppdrag:Uppdrag)-[:BESTÄLLER]-(bestallare:Bestallare)-[:BESTÄLLER_ÅT]-(kund:Kund)")
                .Return((konsult, uppdrag, roll, bestallare, kund) => new
                {
                    Uppdrag = uppdrag.As<Uppdrag>(),
                    Konsult = konsult.CollectAs<Konsult>(),
                    Roll = roll.CollectAs<Roll>(), 
                    Bestallare = bestallare.As<Bestallare>(),
                    Kund = kund.As<Kund>()
                })
                .Results;
            AddUppdragViewModel editUppdrag = new AddUppdragViewModel();
            foreach (var item in result)
            {
                List<AddUppgiftViewModel> listaUppgifter = new List<AddUppgiftViewModel>();
                editUppdrag.Namn = item.Uppdrag.Namn;
                editUppdrag.Problem = item.Uppdrag.Problem;
                editUppdrag.Losning = item.Uppdrag.Losning;
                editUppdrag.Resultat = item.Uppdrag.Resultat;
                editUppdrag.Timmar = item.Uppdrag.Timmar;
                editUppdrag.Beskrivning = item.Uppdrag.Beskrivning;
                editUppdrag.Kund = item.Kund.Namn;
                editUppdrag.Bestallare = item.Bestallare.Namn;
                List<Roll> tempRollLista = new List<Roll>();
                foreach (var n in item.Roll)
                {
                    Roll tempRoll = new Roll();
                    tempRoll.Namn = n.Data.Namn;
                    tempRollLista.Add(tempRoll);
                }
                List<Konsult> tempKonsultLista = new List<Konsult>();
                foreach (var n in item.Konsult)
                {
                    Konsult tempKonsult = new Konsult();
                    tempKonsult.Namn = n.Data.Namn;
                    tempKonsultLista.Add(tempKonsult);
                }
                for (var i = 0; i < tempKonsultLista.Count(); ++i)
                {
                    AddUppgiftViewModel uppgift = new AddUppgiftViewModel();
                    uppgift.Roll = tempRollLista[i].Namn;
                    uppgift.Konsult = tempKonsultLista[i].Namn;
                    listaUppgifter.Add(uppgift);
                }
                editUppdrag.Uppgifter = listaUppgifter;
            }
            AllaKunder(editUppdrag.Kund);
            AllaBestallare(editUppdrag.Bestallare);

            return View(editUppdrag);
        }

        public ActionResult showAddModalUppgift(string Konsult, string Roll, string Uppdrag)
        {
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(uppdrag:Uppdrag)-[:DEL_AV]-(uppgift:Uppgift)-[:UTFÖR]-(konsult:Konsult)")
                .Where((Uppdrag uppdrag) => uppdrag.Namn == Uppdrag)
                .AndWhere((Konsult konsult) => konsult.Namn == Konsult)
                .OptionalMatch("(uppgift:Uppgift)-[:I_ROLL]-(roll:Roll)")
                .Where((Roll roll) => roll.Namn == Roll)
                .Return((konsult, uppdrag, roll) => new
                {
                    Uppdrag = uppdrag.As<Uppdrag>(),
                    Konsult = konsult.As<Konsult>(),
                    Roll = roll.As<Roll>()
                })
                .Results;

            AddUppgiftViewModel vm = new AddUppgiftViewModel();
            foreach (var item in result)
            {
                vm.Konsult = item.Konsult.Namn;
                vm.Roll = item.Roll.Namn;
            }

            return PartialView("_EditUppgiftModal", vm);
        }

        public ActionResult RemoveUppgift(string Konsult, string Roll, string Uppdrag)
        {
            WebApiConfig.GraphClient.Cypher
                .Match("(uppdrag:Uppdrag)")
                .Where((Uppdrag uppdrag) => uppdrag.Namn == Uppdrag)
                .OptionalMatch("(uppdrag:Uppdrag)-[r1:DEL_AV]-(uppgift:Uppgift)-[r2:UTFÖR]-(konsult:Konsult)")
                .Where((Konsult konsult) => konsult.Namn == Konsult)
                .OptionalMatch("(uppgift:Uppgift)-[r3:I_ROLL]-(roll:Roll)")
                .Where((Roll roll) => roll.Namn == Roll)
                .Delete("r1, r2, r3")
                .Delete("uppgift")
                .ExecuteWithoutResults();

            return RedirectToAction("EditUppdrag", "Write", routeValues: new { Namn = Uppdrag });

        }

        public ActionResult kund()
        {
            ViewBag.Message = "Lägg till/ändra kund";

            return View();
        }

        public ActionResult konsult()
        {
            ViewBag.Message = "Lägg till/ändra konsult";
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(konsult:Konsult)")
                .OptionalMatch("(konsult:Konsult)-[:KAN]-(kompetens:Kompetens)")
                //.Where((Konsult konsult) => konsult.Namn == "")
                //.AndWhere((Kompetens kompetens) => kompetens.Namn == "")
                .Return((konsult, kompetens) => new
                {
                    Konsult = konsult.As<Konsult>(),
                    Kompetens = kompetens.CollectAs<Kompetens>()
                })
                .OrderBy("Konsult.Namn")
                .Results;
            List<AddKonsultViewModel> listaKonsulter = new List<AddKonsultViewModel>();
            //AddKonsultViewModel vm = new AddKonsultViewModel();
            foreach (var item in result)
            {
                List<AddKompetensViewModel> listaKompetenser = new List<AddKompetensViewModel>();
                AddKonsultViewModel konsult = new AddKonsultViewModel();
                konsult.Namn = item.Konsult.Namn;
                konsult.Epost = item.Konsult.Epost;
                
                foreach (var n in item.Kompetens)
                {
                    AddKompetensViewModel kompetens = new AddKompetensViewModel();
                    kompetens.Namn = n.Data.Namn;
                    kompetens.Beskrivning = n.Data.Beskrivning;
                    listaKompetenser.Add(kompetens);
                    //konsult.Kompetens.Add(kompetens);
                }
                konsult.Kompetens = listaKompetenser;
                listaKonsulter.Add(konsult);
            }
            //vm = konsulter;
            //ViewBag.Konsulter = new SelectList(konsulter, "Namn", "Namn", Id);
            return View(listaKonsulter);
        }

        public ActionResult EditKonsult(string Namn)
        {
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(konsult:Konsult)")
                .Where((Konsult konsult) => konsult.Namn == Namn)
                .OptionalMatch("(konsult:Konsult)-[r:KAN]-(kompetens:Kompetens)")
                .Return((konsult, kompetens, r) => new
                {
                    Konsult = konsult.As<Konsult>(),
                    Kompetens = kompetens.CollectAs<Kompetens>(),
                    Niva = r.As<Kompetens>().Niva

                })
                .Results;
            AddKonsultViewModel editKonsult = new AddKonsultViewModel();
            List<AddKompetensViewModel> listaKompetenser = new List<AddKompetensViewModel>();

            foreach (var item in result)
            {
                editKonsult.Namn = item.Konsult.Namn;
                editKonsult.Kontor = item.Konsult.Kontor;
                editKonsult.Telefon = item.Konsult.Telefonnummer;
                editKonsult.Epost = item.Konsult.Epost;
                editKonsult.Beskrivning = item.Konsult.Beskrivning;
                editKonsult.Titel = item.Konsult.Titel;
                foreach (var n in item.Kompetens)
                {
                    AddKompetensViewModel kompetens = new AddKompetensViewModel();
                    kompetens.Namn = n.Data.Namn;
                    kompetens.Beskrivning = n.Data.Beskrivning;
                    kompetens.Niva = item.Niva;
                    kompetens.Konsult = item.Konsult.Namn;
                    listaKompetenser.Add(kompetens);
                    //konsult.Kompetens.Add(kompetens);
                }
            }
            AllaKontor(editKonsult.Kontor);

            editKonsult.Kompetens = listaKompetenser;
            return View(editKonsult);
        }

        public ActionResult EditKompetens(string Namn, string Konsult, int Niva)
        {
            WebApiConfig.GraphClient.Cypher
                .OptionalMatch("(konsult:Konsult)-[r]->(kompetens:Kompetens)")
                .Where((Kompetens kompetens) => kompetens.Namn == Namn)
                .AndWhere((Konsult konsult) => konsult.Namn == Konsult)
                .Set("r.Niva = {Niva} ")
                .WithParam("Niva", Niva)
                .ExecuteWithoutResults();
            return RedirectToAction("EditKonsult", "Write", routeValues: new { Namn = Konsult });
        }


        public ActionResult RemoveKompetens(string Kompetens, string Konsult)
        {
            WebApiConfig.GraphClient.Cypher
                .OptionalMatch("(konsult:Konsult)-[r]->(kompetens:Kompetens)")
                .Where((Kompetens kompetens) => kompetens.Namn == Kompetens)
                .AndWhere((Konsult konsult) => konsult.Namn == Konsult)
                .Delete("r")
                .ExecuteWithoutResults();
            return RedirectToAction("EditKonsult", "Write", routeValues: new { Namn = Konsult });
        }

        public void AllaKontor(string Id)
        {
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(kontor:Kontor)")
                .Return((kontor) => new
                {
                    Kontor = kontor.As<Kontor>()
                })
                .Results;
            List<Kontor> listaKontor = new List<Kontor>();
            foreach (var item in result)
            {
                Kontor kontor = new Kontor();
                kontor.Namn = item.Kontor.Namn;
                listaKontor.Add(kontor);
            }

            ViewBag.Kontor = new SelectList(listaKontor, "Namn", "Namn", Id);
        }

        public void AllaKunder(string Id)
        {
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(kund:Kund)")
                .Return((kund) => new
                {
                    Kund = kund.As<Kund>()
                })
                .Results;

            List<Kund> listaKunder = new List<Kund>();
            foreach (var item in result)
            {
                Kund kund = new Kund();
                kund.Namn = item.Kund.Namn;
                listaKunder.Add(kund);
            }
            ViewBag.Kunder = new SelectList(listaKunder, "Namn", "Namn", Id);
        }

        public void AllaBestallare(string Id)
        {
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(bestallare:Bestallare)")
                .Return((bestallare) => new
                {
                    Bestallare = bestallare.As<Bestallare>()
                })
                .Results;

            List<Bestallare> listaBestallare = new List<Bestallare>();
            foreach (var item in result)
            {
                Bestallare bestallare = new Bestallare();
                bestallare.Namn = item.Bestallare.Namn;
                listaBestallare.Add(bestallare);
            }
            ViewBag.Bestallare = new SelectList(listaBestallare, "Namn", "Namn", Id);
        }

        /*ANVÄNDS INTE !?!?!?!?!
         * 
        public JsonResult GeKonsultInfo(string Namn)
        {

            var result = WebApiConfig.GraphClient.Cypher
                .Match("(konsult:Konsult)")
                .Where((Konsult konsult) => konsult.Namn == Namn)
                .OptionalMatch("(konsult:Konsult)-[r:KAN]-(kompetens:Kompetens)")
                .Return((konsult, kompetens, r) => new
                {
                    Konsult = konsult.As<Konsult>(),
                    Kompetens = kompetens.As<Kompetens>(),
                    Niva = r.As<Konsult>()
                })
                .Results;

            return Json(new { success = true, result });
        }
         * 
         */
        public JsonResult GetAllKompetens(string searchTerm)
        {
            var results = WebApiConfig.GraphClient.Cypher
                .Match("(kompetens:Kompetens)")
                .Return((kompetens) => new
                {
                    Kompetens = kompetens.As<Kompetens>()
                })
                .Results;

            List<Kompetens> kompetenser = new List<Kompetens>();
            foreach (var item in results)
            {
                Kompetens komp = new Kompetens();
                komp.Namn = item.Kompetens.Namn;
                komp.Beskrivning = item.Kompetens.Beskrivning;
                komp.Typ = item.Kompetens.Typ;
                kompetenser.Add(komp);
            }
            kompetenser = kompetenser.Where(m => m.Namn.Contains(searchTerm)).ToList();

           return Json(kompetenser, JsonRequestBehavior.AllowGet);
   }
        
        [HttpPost]
        public ActionResult AddKonsult(AddKonsultViewModel vm)
        {
            var nyKonsult = new Konsult
            {
                Namn = vm.Namn,
                Titel = vm.Titel,
                Epost = vm.Epost,
                Telefonnummer = vm.Telefon,
                //Link = vm.Link,
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
                .Match("(konsult:Konsult)-[r:ARBETAR_I]-(kontor:Kontor)")
                .Where((Konsult konsult) => konsult.Namn == vm.Namn)
                .Delete("r")
                .ExecuteWithoutResults();

            WebApiConfig.GraphClient.Cypher
                .Match("(konsult:Konsult)", "(kontor:Kontor)")
                .Where((Konsult konsult) => konsult.Namn == vm.Namn)
                .AndWhere((Kontor kontor) => kontor.Namn == vm.Kontor)
                .CreateUnique("konsult-[:ARBETAR_I]->kontor")
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

        public ActionResult showAddKonsultModal()
        {
            AddKonsultViewModel vm = new AddKonsultViewModel();
            AllaKontor("");
            return PartialView("_AddKonsultModal", vm);
        }

        [HttpPost]
        public ActionResult SkapaNyKund(AddKundViewModel vm)
        {
            var nyKund = new Kund
            {
                Namn = vm.Namn,
                Adress = vm.Adress
                ,
                Postadress = vm.Postadress,
                Kategori = vm.Kategori
                ,
                Marknadssegment = vm.Segment,
                Telefon = vm.Telefon
            };
            var nyBestallare = new Bestallare
            {
                Namn = vm.Kontakt,
                Avdelning = vm.Avdelning
                ,
                Epost = vm.Epost,
                Konto = vm.Konto,
                Telefon = vm.Telefon
            };
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

        public ActionResult showAddModalKompetens(string Konsult)
        {
            AddKompetensViewModel vm = new AddKompetensViewModel();
            vm.Konsult = Konsult;

            return PartialView("_AddKompetensModal", vm);

        }

        public ActionResult showEditModalKompetens(string Kompetens, string Konsult)
        {
            var result = WebApiConfig.GraphClient.Cypher
                .Match("(konsult:Konsult)-[r:KAN]-(kompetens:Kompetens)")
                .Where((Konsult konsult) => konsult.Namn == Konsult)
                .AndWhere((Kompetens kompetens) => kompetens.Namn == Kompetens)
                .Return((konsult, kompetens, r) => new
                {
                    Konsult = konsult.As<Konsult>(),
                    Kompetens = kompetens.As<Kompetens>(),
                    Niva = r.As<Konsult>()
                })
                .Results;

            AddKompetensViewModel vm = new AddKompetensViewModel();
            foreach (var item in result)
            {
                vm.Konsult = item.Konsult.Namn;
                vm.Namn = item.Kompetens.Namn;
                vm.Kompetenstyp = item.Kompetens.Typ;
                vm.Beskrivning = item.Kompetens.Beskrivning;
                vm.Niva = item.Niva.Niva;
            }
            return PartialView("_EditKompetensModal", vm);
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

            return RedirectToAction("EditKonsult", "Write", routeValues: new { Namn = vm.Konsult });
        }
    }
}