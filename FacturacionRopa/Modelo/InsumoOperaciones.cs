using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class InsumoOperaciones: OperacionesBase<Insumo>
    {
        public InsumoOperaciones(Conexion conexion)
            : base(conexion, "insumos", "Insumos")
        {
            AgregarCampoColumna("ins_id", Id);
            AgregarCampoColumna("ins_desc", Descripcion);
            AgregarCampoColumna("ins_cant", Cantidad);
        }

        public static string Id = "Id";
        public static string Descripcion = "Descripcion";
        public static string Cantidad = "Cantidad";
    }
}
