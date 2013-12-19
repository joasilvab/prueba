using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class ColumnaValor
    {
        public ColumnaValor(string columna, string valor)
        {
            NombreColumna = columna;
            Valor = valor;
        }
        public string NombreColumna;
        public string Valor;
    }
}
