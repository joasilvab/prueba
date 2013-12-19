using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaBD
{
    public class ObjetoDelete
    {
        public string Tabla { get; set; }
        public List<ValorWhere> ListaValoresWhere { get; set; }
        public ObjetoDelete(string tabla, List<ValorWhere> listaValoresWhere)
        {
            if (string.IsNullOrEmpty(tabla)) throw new ArgumentNullException("tabla");
            if (listaValoresWhere == null || listaValoresWhere.Count == 0) throw new ArgumentNullException("listaValoresWhere");
            Tabla = tabla;
            ListaValoresWhere = listaValoresWhere;
        }
    }
}
