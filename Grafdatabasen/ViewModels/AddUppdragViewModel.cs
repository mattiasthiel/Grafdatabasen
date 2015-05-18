using Grafdatabasen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Grafdatabasen.ViewModels
{
    public class AddUppdragViewModel
    {
        public string Namn { get; set; }

        [Required(ErrorMessage = "Vänligen ange en kort beskrivning av uppdraget.")]
        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Beskrivning { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Problem { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Losning { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Resultat { get; set; }

        public float Timmar { get; set; }

        public float Kostnad { get; set; }

        public string Kund { get; set; }

        // Id från dropdown med kunder
        public int valdKundId { get; set; }

        public string Bestallare { get; set; }

        public DateTime Startdatum { get; set; }

        public DateTime Slutdatum { get; set; }

        public virtual List<AddUppgiftViewModel> Uppgifter { get; set; }

        //public virtual List<Utvardering> Utvarderingar { get; set; }

    }
}