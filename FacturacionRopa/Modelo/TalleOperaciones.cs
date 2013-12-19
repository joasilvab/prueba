using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class TalleOperaciones: OperacionesBase<Talle>
    {
        public static string Id = "Id";
        public static string Sigla = "Sigla";

        public TalleOperaciones(Conexion _con)
            : base(_con, "talles", "Talles")
        {
            AgregarCampoColumna("talle_id", Id);
            AgregarCampoColumna("talle_descripcion", Sigla);
        }
    }
}
