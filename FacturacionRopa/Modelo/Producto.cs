using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class Producto: ElementoBase
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int TasaIVAId { get; set; }
    }
}
