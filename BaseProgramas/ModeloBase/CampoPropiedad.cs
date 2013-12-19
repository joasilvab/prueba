using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    internal class CampoPropiedad
    {
        public CampoPropiedad(string campo, string columna)
        {
            NombreCampo = campo;
            NombrePropiedad = columna;
            ClavePrimaria = false;
        }

        public CampoPropiedad(string campo, string columna, bool esclaveprimaria)
            : this(campo, columna)
        {
            ClavePrimaria = esclaveprimaria;
        }

        public string NombreCampo { get; set; }
        public string NombrePropiedad { get; set; }
        public bool ClavePrimaria { get; set; }
    }
}
