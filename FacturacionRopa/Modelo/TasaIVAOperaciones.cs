using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class TasaIVAOperaciones : OperacionesBase<ProductoIVA>
    {
        public TasaIVAOperaciones(Conexion conexion)
            : base(conexion, "tasasiva", "TasasIVA")
        {
            AgregarCampoColumna("tasa_id",Id);
            AgregarCampoColumna("tasa_desc", Descripcion);
            AgregarCampoColumna("tasa_porciento", Tasa);
        }
        public static string Id { get { return "Id"; } }
        public static string Descripcion { get { return "Descripcion"; } }
        public static string Tasa { get { return "Tasa"; } }
    }
}
