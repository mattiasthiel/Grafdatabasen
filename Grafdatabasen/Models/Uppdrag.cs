using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.Models
{
    public class Uppdrag
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public string Problem { get; set; }

        public string Losning { get; set; }

        public string Resultat { get; set; }

        public float Timmar { get; set; }

        public float Kostnad { get; set; }

        public string Kund { get; set; }

        public string Bestallare { get; set; }

        public DateTime startDatum { get; set; }

        public DateTime slutDatum { get; set; }

        public virtual List<Konsult> Konsulter { get; set; }

        public virtual List<Teknik> Tekniker { get; set; }

        public virtual List<Metod> Metoder { get; set; }

        public virtual List<Omrade> Omraden { get; set; }

        public virtual List<Utvardering> Utvarderingar { get; set; }



    }
}