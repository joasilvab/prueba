using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaBD
{
    public class ValorUpdate
    {
        public string Campo { get; set; }
        public string ValorNuevo { get; set; }
        public bool ValorComillas { get; set; }
        public ValorUpdate(string campo, string valornuevo, bool comillas)
        {
            Campo = campo;
            ValorNuevo = valornuevo;
            ValorComillas = comillas;
        }
    }
}
