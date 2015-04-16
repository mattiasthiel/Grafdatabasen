using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.Models
{
    public class Utbildning
    {
        public string Namn { get; set; }

        public string Beskrivning { get; set; }

        public string Omfattning { get; set; }

        public string Plats { get; set; }

        public string Tid { get; set; }
    }
}