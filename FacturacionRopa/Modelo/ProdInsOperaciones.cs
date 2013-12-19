using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class ProdInsOperaciones: OperacionesBase<ProdIns>
    {
        public ProdInsOperaciones(Conexion conexion)
            : base(conexion, "prod_ins")
        {
            AgregarCampoColumna("prod_ins_prod_id","ProdId");
            AgregarCampoColumna("prod_ins_ins_id", "InsId");
            //AgregarCampoColumna("
        }

    }
}
