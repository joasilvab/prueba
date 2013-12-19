using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibreriaBD;

namespace Modelo
{
    public class ProdTalleOperaciones: OperacionesBase<ProdTalle>
    {
        public ProdTalleOperaciones(Conexion con)
            : base(con, "prod_talles", "ProdTalle")
        {
            AgregarCampoColumna("prtll_prod_id", ProdId);
            AgregarCampoColumna("prtll_talle_id", TalleId);
            AgregarCampoColumna("prtll_precio_venta", PrecioVenta);
            AgregarCampoColumna("prtll_renglon", Renglon);
        }
        public static string ProdId { get { return "ProdId"; } }
        public static string TalleId { get { return "TalleId"; } }
        public static string PrecioVenta { get { return "PrecioVenta"; } }
        public static string Renglon { get { return "Renglon"; } }

        public List<ProdTalleDescripcion> ObtenerProdTallesDescripciones(Conexion conexTalles, List<ModeloWhere> condiciones)
        {
            TalleOperaciones top = new TalleOperaciones(conexTalles);
            List<string> campos = new List<string>();
            string campoProdID = ObtenerCampo(ProdId);
            string campoPrecioVenta = ObtenerCampo(PrecioVenta);
            string campoRenglon = ObtenerCampo(Renglon);
            string campoTalleDesc = top.ObtenerCampo(TalleOperaciones.Sigla);
            campos.Add(campoProdID);
            campos.Add(campoPrecioVenta);
            campos.Add(campoRenglon);
            campos.Add(campoTalleDesc);
            List<string> tablas = new List<string>(){ this.Tabla, top.Tabla};
            ValorWhere vw = new ValorWhere(ObtenerCampo(TalleId),false,top.ObtenerCampo(TalleOperaciones.Id),false, Utilidades.Signos.Igual);
            List<ProdTalleDescripcion> lprtd = new List<ProdTalleDescripcion>();
            List<ValorWhere> lvw = new List<ValorWhere>();
            if (condiciones != null)
            {
                foreach (ModeloWhere mw in condiciones)
                {
                    lvw.Add(ConvertirWhereModeloABase(mw));
                }
            }
            lvw.Add(vw);
            foreach (Dictionary<string, object> dict in _libbd.Select(true, campos, tablas, lvw))
            {
                ProdTalleDescripcion ptd = new ProdTalleDescripcion();
                ptd.PrecioVenta = (decimal)dict[campoPrecioVenta];
                ptd.ProdId = (int)dict[campoProdID];
                //Type tipo = (dict[campoRenglon]).GetType();
                ptd.Renglon = (sbyte)dict[campoRenglon];

                ptd.TalleDesc = dict[campoTalleDesc].ToString();
                lprtd.Add(ptd);
            }
            return lprtd;
        }

        public List<ProdTalleDescripcion> OrdenarTallesDescripcion(List<ProdTalleDescripcion> talles)
        {
            List<ProdTalleDescripcion> tallesNumeros = new List<ProdTalleDescripcion>();
            List<ProdTalleDescripcion> tallesEses = new List<ProdTalleDescripcion>();
            List<ProdTalleDescripcion> tallesEmes = new List<ProdTalleDescripcion>();
            List<ProdTalleDescripcion> tallesEles = new List<ProdTalleDescripcion>();
            List<ProdTalleDescripcion> tallesOtros = new List<ProdTalleDescripcion>();
            foreach (ProdTalleDescripcion t in talles)
            {
                int salida;
                if (int.TryParse(t.TalleDesc, out salida))
                {
                    tallesNumeros.Add(t);
                }
                else
                {
                    if (t.TalleDesc.ToLower().Contains('s'))
                    {
                        tallesEses.Add(t);
                    }
                    else
                        if (t.TalleDesc.ToLower().Contains('m'))
                        {
                            tallesEmes.Add(t);
                        }
                        else
                            if (t.TalleDesc.ToLower().Contains('l'))
                            {
                                tallesEles.Add(t);
                            }
                            else
                                tallesOtros.Add(t);
                }
            }
            List<ProdTalleDescripcion> devolver = new List<ProdTalleDescripcion>();
            tallesNumeros = tallesNumeros.OrderBy(p => int.Parse(p.TalleDesc)).ToList();
            tallesEses = tallesEses.OrderByDescending(t => t.TalleDesc).ToList();
            tallesEmes = tallesEmes.OrderByDescending(t => t.TalleDesc).ToList();
            tallesEles = tallesEles.OrderBy(t => t.TalleDesc).ToList();
            devolver.AddRange(tallesNumeros);
            devolver.AddRange(tallesEses);
            devolver.AddRange(tallesEmes);
            devolver.AddRange(tallesEles);
            devolver.AddRange(tallesOtros);
            return devolver;
        }
    }
}
