using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.Models
{
    public class Kompetens
    {
        public string Namn { get; set; }
        public string Typ { get; set; }
        public string Beskrivning { get; set; }
        public int Niva { get; set; }
    }
}