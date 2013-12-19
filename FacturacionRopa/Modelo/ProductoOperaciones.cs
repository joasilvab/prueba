using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibreriaBD;

namespace Modelo
{
    public class ProductoOperaciones: OperacionesBase<Producto>
    {
        public ProductoOperaciones(Conexion conexion)
            :base(conexion,"productos","Productos")
        {
            AgregarCampoColumna("prod_id", Id);
            AgregarCampoColumna("prod_desc", Descripcion);
            AgregarCampoColumna("prod_tasa_id", TasaIVAId);
        }

        public static string Id = "Id";
        public static string Descripcion = "Descripcion";
        public static string TasaIVAId = "TasaIVAId";
        
        public void AgregarProductoConTalles(Producto producto, List<ProdTalle> listaProdTalles, Conexion conexProdTalle)
        {
            ProdTalleOperaciones ptop = new ProdTalleOperaciones(conexProdTalle);
            List<string> sentencias = new List<string>();
            sentencias.Add(_libbd.GenerarInsertInto(TipoAObjetoInsert(producto)));
            foreach (ProdTalle prtll in listaProdTalles)
            {
                ObjetoInsert oi = ptop.TipoAObjetoInsert(prtll);
                ValorInsert vi = oi.ValoresInsert.Find(p => p.Into == ptop.ObtenerCampo(ProdTalleOperaciones.ProdId));
                vi.Value = "LAST_INSERT_ID()";
                vi.ValueComillas = false;
                sentencias.Add(_libbd.GenerarInsertInto(oi));
            }
            try
            {
                _libbd.Transaction(sentencias);
            }
            catch (BaseDatosEntradaDuplicadaException exc)
            {
                throw new ModeloEntradaDuplicadaException(exc.ValorDuplicado, ObtenerColumna(exc.CampoDuplicado));
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public void ModificarProductoConTalles(Producto producto, List<ProdTalle> listaProdTalles, Conexion conexionProdTalle)
        {
            try
            {
                ProdTalleOperaciones ptop = new ProdTalleOperaciones(conexionProdTalle);
                List<string> sentencias = new List<string>();
                Producto prod = (Producto)producto.Copia();
                prod.Descripcion = null;
                //Modificar(prod, producto);
                sentencias.Add(_libbd.GenerarUpdate(_nombretabla,TipoAValorUpdate(producto),TipoAValorWhere(prod)));
                ModeloWhere condicioneliminar = new ModeloWhere(ProdTalleOperaciones.ProdId, producto.Id.ToString(), true, Utilidades.Signos.Igual);
                //ptop.Eliminar(condicioneliminar);
                sentencias.Add(_libbd.GenerarDeleteFrom(new ObjetoDelete(ptop.Tabla, new List<ValorWhere> { ptop.ConvertirWhereModeloABase(condicioneliminar) })));
                foreach (ProdTalle pt in listaProdTalles)
                {
                    pt.ProdId = producto.Id;
                    ObjetoInsert oi = ptop.TipoAObjetoInsert(pt);
                    sentencias.Add(_libbd.GenerarInsertInto(oi));
                }
                //ptop.AgregarVarios(listaProdTalles);
                _libbd.Transaction(sentencias);
            }
            catch (BaseDatosEntradaDuplicadaException exc)
            {
                throw new ModeloEntradaDuplicadaException(exc.ValorDuplicado, ObtenerColumna(exc.CampoDuplicado));
            }
            catch (Exception exc)//Sacar?
            {
                throw exc;
            }
        }

        public void EliminarProductoConTalles(Producto producto, Conexion conexProdTalle)
        {
            ProdTalleOperaciones pto = new ProdTalleOperaciones(conexProdTalle);
            ModeloWhere mw = new ModeloWhere(ProdTalleOperaciones.ProdId, producto.Id.ToString(), true, Utilidades.Signos.Igual);
            List<string> sentencias = new List<string>();
            ObjetoDelete od = new ObjetoDelete(pto.Tabla, new List<ValorWhere>{ pto.ConvertirWhereModeloABase(mw)});
            sentencias.Add(_libbd.GenerarDeleteFrom(od));
            mw.NombrePropiedad = ProductoOperaciones.Id;
            od = new ObjetoDelete(_nombretabla, new List<ValorWhere> { this.ConvertirWhereModeloABase(mw) });
            sentencias.Add(_libbd.GenerarDeleteFrom(od));
            _libbd.Transaction(sentencias);
        }

        public List<ProductoIVA> ObtenerConIVA(Conexion conexIVA)
        {
            TasaIVAOperaciones tivaop = new TasaIVAOperaciones(conexIVA);
            List<string> campos = new List<string>();
            string campoId = ObtenerCampo(Id);
            string campoDescripcion = ObtenerCampo(Descripcion);
            string campoTasaId = ObtenerCampo(TasaIVAId);
            string campoTasaPorciento = tivaop.ObtenerCampo(TasaIVAOperaciones.Tasa);
            campos.Add(campoId);
            campos.Add(campoDescripcion);
            campos.Add(campoTasaId);
            campos.Add(campoTasaPorciento);
            List<string> tablas = new List<string>(){ this.Tabla, tivaop.Tabla};
            ValorWhere vw = new ValorWhere(campoTasaId,false,tivaop.ObtenerCampo(TasaIVAOperaciones.Id),false, Utilidades.Signos.Igual);
            List<ProductoIVA> lpi = new List<ProductoIVA>();
            foreach (Dictionary<string,object> dict in _libbd.Select(true, campos, tablas, new List<ValorWhere> { vw }))
            {
                lpi.Add(new ProductoIVA
                {
                     Descripcion = dict[campoDescripcion].ToString(),
                      Id = (int)dict[campoId],
                       Iva = (decimal)dict[campoTasaPorciento],
                        TasaIVAId = (int)dict[campoTasaId]
                });
            }
            return lpi;
        }
    }
}
