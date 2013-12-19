using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class ProdTalle: ElementoBase
    {
        public int ProdId { get; set; }
        public int TalleId { get; set; }
        public decimal PrecioVenta{get;set;}
        public int Renglon { get; set; }
        public Talle Talle(Conexion con)
        {
            TalleOperaciones top = new TalleOperaciones(con);
            return top.Obtener(new ModeloWhere(TalleOperaciones.Id, TalleId.ToString(), true, Utilidades.Signos.Igual));
        }
    }
}
