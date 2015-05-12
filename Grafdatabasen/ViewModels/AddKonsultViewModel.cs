using Grafdatabasen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grafdatabasen.ViewModels
{
    public class AddKonsultViewModel
    {
        public string Namn { get; set; }

        public string Titel { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Beskrivning { get; set; }

        public string Telefon { get; set; }

        public string Epost { get; set; }

        public string Link { get; set; }

        public string Kontor { get; set; }

        public virtual List<AddKompetensViewModel> Kompetens { get; set; }

        //public virtual List<Teknik> Tekniker { get; set; }

        //public virtual List<Omrade> Omraden { get; set; }

        //public virtual List<TidigareAnst> TidigareAnst { get; set; }

        //public virtual List<Utbildning> Utbildning { get; set; }

        //public virtual List<Roll> Roller { get; set; }

        //public virtual List<Uppgift> Uppgifter { get; set; }

        //public virtual List<Konsult> Konsulter { get; set; }

    }
}