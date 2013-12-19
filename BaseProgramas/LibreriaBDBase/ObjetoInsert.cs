using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaBD
{
    public class ObjetoInsert
    {
        public string Tabla{get;set;}
        public List<ValorInsert> ValoresInsert { get; set; }

        /// <summary>
        /// Crea un Objeto insert que contiene datos sobre el Insert de mysql. Incluye el nombre de la tabla y una lista de ValoresInserts
        /// </summary>
        /// <param name="tabla"></param>
        /// <param name="listaValoresInsert"></param>
        public ObjetoInsert(string tabla, List<ValorInsert> listaValoresInsert)
        {
            if (string.IsNullOrEmpty(tabla)) throw new ArgumentNullException("tabla");
            if (listaValoresInsert == null || listaValoresInsert.Count == 0) throw new ArgumentNullException("listaValoresInsert");
            Tabla = tabla;
            ValoresInsert = listaValoresInsert;
        }
    }
}
