using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Formularios
{
    public class RenglonProducto
    {
        public string Articulo { get; set; }
        public string Talles { get; set; }
        public decimal? Precio { get; set; }
        public decimal? Iva { get; set; }
        public decimal? Total { get; set; }
    }
}
