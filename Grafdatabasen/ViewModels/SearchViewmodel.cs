using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Grafdatabasen.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Grafdatabasen.ViewModels
{
    public class SearchViewModel
    {

        private readonly List<string> lista1;

        [Display(Name = " 1.")]
        public int valdId { get; set; }

        public IEnumerable<SelectListItem> Lista1
        {
            get { return new SelectList(lista1, "Id", "SearchItem1"); }
        }

        public string SearchItem1 { get; set; }
        public string SearchItem2 { get; set; }
        public string SearchItem3 { get; set; }
        public string SearchItem4 { get; set; }
        public string SearchItem5 { get; set; }

        public string Nod { get; set; }

        //public SelectListItem SearchItem2 { get; set; }

        //List<SelectListItem> lista2 = new List<SelectListItem>();
        //lista2.Add(new SelectListItem { Text = "--Välj--", Value = "0" });

    }
}