﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Grafdatabasen.ViewModels
{
    public class AddKompetensViewModel
    {
        public string Konsult { get; set; }
        public string Kompetenstyp { get; set; }

        public string Namn { get; set; }

        public string Beskrivning { get; set; }
        public int Niva { get; set; }

    }
}