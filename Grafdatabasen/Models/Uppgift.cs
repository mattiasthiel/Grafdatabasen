using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.Models
{
    public class Uppgift
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public float Grad { get; set; }

        public DateTime startDatum { get; set; }

        public DateTime slutDatum { get; set; }

        public virtual Konsult Konsult { get; set; }

        public virtual Uppdrag Uppdrag { get; set; }

        public virtual Roll Roll { get; set; }

        public virtual List<Teknik> Tekniker { get; set; }

        public virtual List<Omrade> Omraden { get; set; }

        public virtual List<Metod> Metoder { get; set; }

    }
}