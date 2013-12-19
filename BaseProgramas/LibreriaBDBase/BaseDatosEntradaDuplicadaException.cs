using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaBD
{
    public class BaseDatosEntradaDuplicadaException: BaseDatosException
    {
        public string ValorDuplicado { get; set; }
        public string CampoDuplicado { get; set; }
        public BaseDatosEntradaDuplicadaException(string valor, string campo)
        {
            ValorDuplicado = valor;
            CampoDuplicado = campo;
        }

    }
}
