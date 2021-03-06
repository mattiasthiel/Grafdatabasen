﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.Models
{
    public class Konsult
    {
        public string Namn { get; set; }

        public string Titel { get; set; }

        public string Beskrivning { get; set; }

        public string Telefonnummer { get; set; }

        public string Epost { get; set; }

        //public string Link { get; set; }

        public string Kontor { get; set; }
        
        public int Niva { get; set; }

        public virtual Konsult Konsulter { get; set; }

        public virtual List<Kompetens> Kompetens { get; set; }

        //public virtual List<TidigareAnst> TidigareAnstl { get; set; }

        //public virtual List<Utbildning> Utbildningar { get; set; }

        //public virtual List<Roll> Roller { get; set; }

        //public virtual List<Uppgift> Uppgifter { get; set; }


    }
}