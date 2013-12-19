using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaBD
{
    public class ValorInsert
    {
        public string Into {get;set;}
        public string Value {get;set;}
        public bool ValueComillas { get; set; }  //Por si es una funcion el value

        /// <summary>
        /// Crea un ValorInsert que sería el par "Into" "Value" para el insert into. "valorComillas" determina si values va entre comillas (Por ej. si queres poner una función como LAST_INSERT_ID() este valor iría en false)
        /// </summary>
        /// <param name="into"></param>
        /// <param name="valor"></param>
        public ValorInsert(string into, string valor, bool valorComillas)
        {
            if (string.IsNullOrEmpty(into)) throw new ArgumentNullException("into");
            if (string.IsNullOrEmpty(valor)) throw new ArgumentNullException("valor");
            Into = into;
            Value = valor;
            ValueComillas = valorComillas;
        }
    }
}
