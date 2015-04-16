using Grafdatabasen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.ViewModels
{
    public class AddUppgiftViewModel
    {
        public partial class UppgiftPartialModel
        {
            public string Beskrivning { get; set; }

            public float Grad { get; set; }

            public DateTime Startdatum { get; set; }

            public DateTime Slutdatum { get; set; }

            public virtual Konsult Konsult { get; set; }

            public virtual Uppdrag Uppdrag { get; set; }

            public virtual Roll Roll { get; set; }

            public virtual List<Teknik> Tekniker { get; set; }

            public virtual List<Omrade> Omraden { get; set; }

            public virtual List<Metod> Metoder { get; set; }
        }
        public partial class UppgiftPartialModel
        {
            public List<UppgiftPartialModel> uppgiftPartialModel { get; set; }

        }
    }
}