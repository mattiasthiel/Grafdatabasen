using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.Models
{
    public class Utvardering
    {
        public string Beskrivning { get; set; }

        public string Betyg { get; set; }

        public string Utvarderare { get; set; }

        public virtual Uppdrag Uppdrag { get; set; }
    }
}