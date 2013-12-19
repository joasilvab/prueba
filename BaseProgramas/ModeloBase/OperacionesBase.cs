using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using LibreriaBD;
using Utilidades;

namespace Modelo
{
    public abstract class OperacionesBase<T> where T: ElementoBase 
    {
        protected ClaseLibreriaBDMySQL _libbd;
        protected string _nombretabla;
        List<CampoPropiedad> _camposPropiedades;

        //Sacar este Constructor
        protected OperacionesBase(Conexion con, string tabla,string nombre)//string server, string bd, string user, string pass, string tabla)
        {
            _nombre = nombre;
            _nombretabla = tabla;
            _libbd = new ClaseLibreriaBDMySQL(con.Server, con.BD, con.User, con.Pass);
            _camposPropiedades = new List<CampoPropiedad>();
        }

        protected OperacionesBase(Conexion con, string tabla)
            :this(con,tabla,"")
        {
        }

        public string Tabla
        {
            get
            {
                return _nombretabla;
            }
        }

        string _nombre;
        public string Nombre { get { return _nombre; } }

        public DataColumnCollection Campos()
        {
            return ObtenerEsquema().Columns;
        }

        public List<PropertyInfo> Propiedades
        {
            get
            {
                return typeof(T).GetProperties().ToList();
            }
        }

        public List<PropertyInfo> PropiedadesNoForaneas
        {
            get
            {
                return Propiedades.FindAll(p => p.Name.Substring(p.Name.Length - 2) != "Id");//FEO
            }
        }

        public List<string> PropiedadesForaneas
        {
            get
            {
                List<string> devolver = new List<string>(); ;
                List<PropertyInfo> listaPi = Propiedades.FindAll(p=>p.Name.Substring(p.Name.Length-2)=="Id");//FEO
                foreach (PropertyInfo Pi in listaPi)
                {
                    devolver.Add(Pi.Name.Substring(Pi.Name.Length - 3));
                }
                return devolver;
            }
        }

        public List<PropertyInfo> PropiedadesNoAutoincrementales
        {
            get
            {
                DataTable dt = this.ObtenerEsquema();
                List<PropertyInfo> devolver = Propiedades;
                foreach (DataColumn dc in dt.Columns)
                {
                    if (dc.AutoIncrement == true)
                    {
                        foreach (PropertyInfo pi in devolver)
                        {
                            if (pi.Name == dc.ColumnName)
                            {
                                devolver.Remove(pi);
                                break;
                            }
                        }
                    }
                }
                return devolver;
            }
        }

        public List<string> ObtenerColumnasIdsBase()
        {
            List<string> devolver = new List<string>();
            DataTable dt = ObtenerEsquema();
            foreach (DataColumn dc in dt.PrimaryKey)
            {
                devolver.Add(dc.ColumnName);
            }
            return devolver;
        }

        public List<string> ObtenerColumnasIds()
        {
            List<string> devolver = new List<string>();
            foreach (CampoPropiedad cp in _camposPropiedades)
            {
                if (cp.ClavePrimaria)
                {
                    devolver.Add(cp.NombrePropiedad);
                }
            }
            return devolver;
        }

        protected string ObtenerColumna(string Campo)
        {
            foreach (CampoPropiedad cc in _camposPropiedades)
            {
                if (cc.NombreCampo == Campo)
                    return cc.NombrePropiedad;
            }
            return null;
        }

        public string ObtenerCampo(string Columna) //Ver lo de >
        {
            char? signo = null;
            if (Columna.Last() == '>' || Columna.Last() == '<')
            {
                signo = Columna.Last();
                Columna = Columna.Remove(Columna.Length - 1);
            }
            foreach (CampoPropiedad cc in _camposPropiedades)
            {
                if (cc.NombrePropiedad == Columna)
                {
                    if (signo != null)

                        return cc.NombreCampo + signo;
                    else
                        return cc.NombreCampo;
                }
            }
            throw new ModeloCampoNoEncontradoException(Columna, _nombre);
            //return null;
        }

        bool VerificarCampo(string campo)
        {
            DataTable nombrescampos = _libbd.ObtenerNombresCampos(_nombretabla);
            foreach (DataRow dr in nombrescampos.Rows)
            {
                if (dr.ItemArray[0].ToString() == campo)
                    return true;
            }
            return false;
        }

        //Verifica si existe una propiedad con el mismo nombre
        bool VerificarColumna(string columna)
        {
            Type type = typeof(T);
            System.Reflection.PropertyInfo[] attrs = type.GetProperties();
            foreach (System.Reflection.PropertyInfo att in attrs)
            {
                if (att.Name == columna)
                {
                    return true;
                }
            }
            return false;
        }

        void VerificarCamposPropiedades()
        {
            if (_camposPropiedades == null) 
            {                
                Type tipo = typeof(T);
                //throw new IOperacionesException("No se ha inicializado ninguna relacion entre las propiedades de la clase " + tipo.Name + " y los campos de la tabla en la BD, en el constructor de la clase "+this.GetType().Name); 
                throw new ModeloSinCamposPropiedadesException(tipo.Name, this.GetType().Name);
            }
            DataTable dt = _libbd.ObtenerNombresCampos(_nombretabla);
            foreach (DataRow dr in dt.Rows)
            {
                CampoPropiedad buscar = _camposPropiedades.Find(
                    delegate(CampoPropiedad cc)
                    {
                        return cc.NombreCampo == dr.ItemArray[0].ToString();
                    });
                if (buscar == null)
                    throw new ModeloCampoSinPropiedadException(dr.ItemArray[0].ToString(), typeof(T).Name, this.GetType().Name);
                    //throw new IOperacionesException("No se definió la relación entre el campo " + dr.ItemArray[0] + " y alguna propiedad de la clase " + typeof(T).Name+" en el constructor de la clase "+this.GetType().Name);
            }
        }

        public void AgregarCampoColumna(string campo, string columna)
        {
            AgregarCampoColumna(campo, columna, false);
        }

        public void AgregarCampoColumna(string campo, string columna, bool esclave)
        {
            //if (_camposcolumnas == null)
            //{
            //    _camposcolumnas = new List<CampoPropiedad>();
            //}
            //if (!VerificarCampo(campo))
            //    throw new Exception("No existe el campo -" + campo + "- en la tabla " + _nombretabla);
            //else
            //    if (!VerificarColumna(columna))
            //    {
            //        Type type = typeof(T);
            //        throw new Exception("No existe la propiedad -" + columna + "- en la clase " + type.Name);
            //    }
            _camposPropiedades.Add(new CampoPropiedad(campo, columna,esclave));
        }

        ///// <summary>
        ///// Devuelve las propiedades mappeadas, sin las foraneas
        ///// </summary>
        //protected List<PropertyInfo> PropiedadesMappeadas
        //{
        //    get
        //    {
        //        //List<PropertyInfo> devolver = Propiedades;
        //        return Propiedades.Except(PropiedadesNoMappeadas).Except(PropiedadesForaneas).ToList();
        //    }
        //}

        protected List<PropertyInfo> PropiedadesNoMappeadas
        {
            get
            {
                List<PropertyInfo> devolver = Propiedades;
                foreach (CampoPropiedad cc in _camposPropiedades)
                {
                    foreach (PropertyInfo propiedad in Propiedades) //Para separar las propiedades no mappeadas con la bd
                    {
                        if (cc.NombrePropiedad == propiedad.Name)
                        {
                            devolver.Remove(propiedad);
                            break;
                        }
                    }
                }
                return devolver;
            }
        }

        public List<string> ListaPropiedades
        {
            get
            {
                List<string> devolver = new List<string>();
                foreach (PropertyInfo pi in Propiedades.Except(PropiedadesNoMappeadas))
                {
                    devolver.Add(pi.Name);
                }
                return devolver;
            }
        }

        protected void CambiarEsquemaDeCamposAColumnas(ref DataTable dt)
        {
            int posicion = 0;
            foreach (DataColumn dc in dt.Columns)
            {
                dc.ColumnName = ObtenerColumna(dc.ColumnName);
            }

            //foreach (CampoPropiedad cc in _camposPropiedades)
            //{
            //    dt.Columns[cc.NombreCampo].SetOrdinal(posicion);
            //    posicion++;
            //    dt.Columns[cc.NombreCampo].ColumnName = cc.NombrePropiedad;
            //}
            foreach (PropertyInfo propiedad in PropiedadesNoMappeadas)
            {
                dt.Columns.Add(propiedad.Name, propiedad.PropertyType);
            }
        }

        public DataTable ObtenerTodos(params string[] Propiedades)//vERRRRRR
        {
            DataTable dt;
            if (Propiedades.Count() != 0)
            {
                List<string> campos = new List<string>();
                foreach (string s in Propiedades)
                {
                    campos.Add(ObtenerCampo(s));
                }
                dt = _libbd.ObtenerTodos(_nombretabla, campos);
            }
            else
            {
                dt = _libbd.ObtenerTodos(_nombretabla);
            }
            CambiarEsquemaDeCamposAColumnas(ref dt);
            //Type tipo = typeof(T);
            //foreach (PropertyInfo propiedad in PropiedadesNoMappeadas)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        List<ColumnaValor> lcv = new List<ColumnaValor>();
            //        foreach (string s in ObtenerColumnasIds())
            //        {
            //            lcv.Add(new ColumnaValor(s, dr[s].ToString()));
            //        }
            //        T elemento = Obtener(lcv.ToArray());
            //        dr[propiedad.Name] = tipo.GetProperty(propiedad.Name).GetValue(elemento, null);
            //    }
            //}
            return dt;
        }

        //public DataTable ObtenerTodos(List<string> Propiedades)
        //{
        //    List<string> campos = new List<string>();
        //    foreach (string s in Propiedades)
        //    {
        //        campos.Add(ObtenerCampo(s));
        //    }
        //    DataTable dt = _libbd.ObtenerTodos(_nombretabla, campos);
        //    CambiarEsquemaDeCamposAColumnas(ref dt);
        //    return dt;
        //}

        public List<Dictionary<string,object>> ObtenerTodosDataReader()
        {
            return _libbd.ObtenerTodosDataReader(_nombretabla);
        }

        public List<T> ObtenerTodosLista()
        {
            List<T> devolver = new List<T>();
            try
            {
                return ConvertirDataTableAList(_libbd.ObtenerTodos(_nombretabla));
            }
            catch (BaseDatosSinConexionException bdex)
            {
                throw new ModeloSinConexionException(bdex);
            }
        }

        public List<T> ObtenerEnList(List<ModeloWhere> condiciones) //idsvalores son extra
        {
            List<T> devolver = new List<T>();
            List<ValorWhere> lvw = new List<ValorWhere>();
            if (condiciones != null && condiciones.Count > 0)
            {
                foreach (ModeloWhere mw in condiciones)
                {
                    lvw.Add(ConvertirWhereModeloABase(mw));
                }
            }
            DataTable dt = _libbd.Obtener(_nombretabla, lvw);
            devolver = ConvertirDataTableAList(dt);
            return devolver;
        }

      

        public DataTable ObtenerEsquema()
        {
            DataTable dt = _libbd.ObtenerEsquema(_nombretabla);
            int posicion = 0;
            foreach (CampoPropiedad cc in _camposPropiedades)
            {
                dt.Columns[cc.NombreCampo].SetOrdinal(posicion);
                posicion++;
                dt.Columns[cc.NombreCampo].ColumnName = cc.NombrePropiedad;
            }
            foreach (PropertyInfo propiedad in PropiedadesNoMappeadas)
            {
                dt.Columns.Add(propiedad.Name, propiedad.PropertyType);
            }
            return dt;
        }

        void ElementoACamposValores(T elemento, ref List<string> campos, ref List<string> valores)
        {
            foreach (CampoPropiedad cc in _camposPropiedades)
            {/*
                campos.Add(cc.NombreCampo);
                PropertyInfo Propiedad = elemento.GetType().GetProperty(cc.NombreColumna);
                if (Propiedad.PropertyType.BaseType.Name == "ElementoBase")
                {
                    object ob = Propiedad.GetValue(elemento,null);
                    valores.Add(ob.GetType().GetProperty("Id").GetValue(ob,null).ToString());
                }
                else
                    valores.Add(elemento.GetType().GetProperty(cc.NombreColumna).GetValue(elemento,null).ToString()); //Por los valores nulos
                 */
                object valor = elemento.GetType().GetProperty(cc.NombrePropiedad).GetValue(elemento, null);
                if (valor != null)
                {
                    campos.Add(cc.NombreCampo);
                    valores.Add(valor.ToString());
                }
            }
        }

        public void Modificar(T elementooriginal, T elementonuevo)
        {
            List<string> campos = new List<string>();
            List<string> datos = new List<string>();
            List<string> valoresoriginales = new List<string>();
            //ElementoACamposValores(elementonuevo, ref campos, ref datos);
            //campos.Clear();
            //ElementoACamposValores(elementooriginal, ref campos, ref valoresoriginales); //Para lo de valores nulos
            //_libbd.Modificar(_nombretabla, campos, valoresoriginales, datos);
            List<ValorWhere> lvw = TipoAValorWhere(elementooriginal);
            List<ValorUpdate> lvu = TipoAValorUpdate(elementonuevo);
            //List<ValorUpdate> lvu = new List<ValorUpdate>();
            //foreach (PropertyInfo pi in Propiedades.Except(PropiedadesNoMappeadas))
            //{
            //    lvu.Add(new ValorUpdate(ObtenerCampo(pi.Name), pi.GetValue(elementonuevo, null).ToString(),true));
            //}
            try
            {
                _libbd.Modificar(_nombretabla, lvu, lvw);
            }
            catch (BaseDatosEntradaDuplicadaException exc)
            {
                throw new ModeloEntradaDuplicadaException(exc.ValorDuplicado, exc.CampoDuplicado);
            }
        }

        //public void Modificar(ModeloWhere[] valoresWhere, ModeloUpdate[] valoresUpdate)
        //{
        //    List<ValorUpdate> lvu = new List<ValorUpdate>();
        //    foreach (ModeloUpdate vu in valoresUpdate)
        //    {
        //        vu.Campo = ObtenerCampo(vu.Campo);
        //        lvu.Add(vu);
        //    }
        //    List<ValorWhere> lvw = new List<ValorWhere>();
        //    foreach (ModeloWhere mu in valoresWhere)
        //    {
        //        mu.ValorDerecha = ObtenerCampo(mu.ValorDerecha); ///////////VERRRRRR si siempre valorDerecha va a ser campo
        //        lvw.Add(mu);
        //    }
        //    _libbd.Modificar(_nombretabla, lvu, lvw);
        //}

        protected T ConvertirDictionaryATipo(Dictionary<string,object> dicparam)
        {
            Type tipo = typeof(T);
            object nuevo = tipo.GetConstructor(Type.EmptyTypes).Invoke(null);
            PropertyInfo[] propiedades = nuevo.GetType().GetProperties();
            foreach (PropertyInfo prop in propiedades.Except(PropiedadesNoMappeadas))
            {
                if (prop.PropertyType.BaseType.Name == "ElementoBase")
                {
                    object[] parametros = new object[1];
                    parametros[0] = dicparam[ObtenerCampo(prop.Name)];
                    tipo.GetMethod("Set" + prop.Name).Invoke(nuevo, parametros);
                }
                else
                {
                    string campo = ObtenerCampo(prop.Name);
                    object obj = dicparam[campo];
                    if (obj is DBNull)
                    prop.SetValue(nuevo, null, null);
                    else
                        try
                        {
                            prop.SetValue(nuevo, obj, null);
                        }
                        catch (Exception exc)
                        {
                            if (exc.Message.Contains("no puede convertirse"))
                            {
                                throw new ModeloOperacionesException("Los tipos de datos del campo '"+campo+"' y la propiedad '"+prop.Name+"' de la clase '"+typeof(T).Name+ "' no coinciden.");
                            }
                        }
                }
            }
            return (T)nuevo;
        }

        List<T> ConvertirDataTableAList(DataTable dt)
        {
            //DataRow dr = dt.Rows[0];
            List<T> devolver = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                Type tipo = typeof(T);
                object nuevo = tipo.GetConstructor(System.Type.EmptyTypes).Invoke(null);
                System.Reflection.PropertyInfo[] propiedades = nuevo.GetType().GetProperties();
                foreach (System.Reflection.PropertyInfo propiedad in propiedades)
                {
                    foreach (CampoPropiedad cc in _camposPropiedades)
                    {
                        if (propiedad.Name == cc.NombrePropiedad)
                            if (propiedad.PropertyType.BaseType.Name == "ElementoBase")
                            {
                                object[] parametros = new object[1];
                                parametros[0] = dr[cc.NombreCampo];
                                tipo.GetMethod("Set" + propiedad.Name).Invoke(nuevo, parametros);
                            }
                            else
                            {
                                object obj = dr[cc.NombreCampo];
                                if (obj is DBNull)
                                    propiedad.SetValue(nuevo, null, null);
                                else
                                    propiedad.SetValue(nuevo, obj, null);
                                //propiedad.SetValue(nuevo, dr[cc.NombreCampo], null);
                                break;
                            }
                    }
                }
                devolver.Add((T)nuevo);
            }
            return devolver;
        }
        /*
        public virtual T Obtener(params ColumnaValor[] idsValores)
        {
            //VerificarCamposColumnas(); fijarse
            List<string> campos = new List<string>(), valores = new List<string>();
            foreach (ColumnaValor cv in idsValores)
            {
                CampoColumna buscarcc = _camposcolumnas.Find(
                    delegate(CampoColumna cc)
                    {
                        return cv.NombreColumna == cc.NombreColumna;
                    }
                    );
                campos.Add(buscarcc.NombreCampo);
                valores.Add(cv.Valor);
            }
            DataTable dt = _libbd.Obtener(_nombretabla,campos,valores);
            if (dt.Rows.Count == 0)
                return null;
            List<T> devolver = ConvertirDataTableAList(dt);
            return devolver[0];
        }*/


        /// <summary>
        /// Obtiene el primer registro según las ColumnaValor pasados por parámetros (preferible que sea id)
        /// </summary>
        /// <param name="idsValores"></param>
        /// <returns></returns>
        public virtual T Obtener(List<ModeloWhere> condiciones)
        {
            List<string> campos = new List<string>(), valores = new List<string>();
            //foreach (ColumnaValor cv in idsValores)
            //{
            //    CampoColumna buscarcc = _camposcolumnas.Find(
            //        delegate(CampoColumna cc)
            //        {
            //            return cv.NombreColumna == cc.NombreColumna;
            //        }
            //        );
            //    if (buscarcc == null)
            //        throw new PropiedadNoEncontradaException(cv.NombreColumna, this._nombre);
            //    campos.Add(buscarcc.NombreCampo);
            //    valores.Add(cv.Valor);
            //}

            List<ValorWhere> lvw = new List<ValorWhere>();
            foreach (ModeloWhere mw in condiciones)
            {
                lvw.Add(ConvertirWhereModeloABase(mw));
            }
            Dictionary<string, object> dic;
            try
            {
                dic = _libbd.ObtenerEnDictionary(_nombretabla, lvw);
            }
            catch (BaseDatosSinConexionException bdexc)
            {
                throw new ModeloSinConexionException(bdexc);
            }
            if (dic == null)
                return null;
            return ConvertirDictionaryATipo(dic);
        }

        public T Obtener(ModeloWhere condicion)
        {
            List<ModeloWhere> lmw = new List<ModeloWhere> { condicion };
            return Obtener(lmw);
        }
        //public virtual T Obtener(params ColumnaValor[] idsValores)
        //{
        //    //VerificarCamposColumnas(); fijarse
        //    List<string> campos = new List<string>(), valores = new List<string>();
        //    foreach (ColumnaValor cv in idsValores)
        //    {
        //        CampoColumna buscarcc = _camposcolumnas.Find(
        //            delegate(CampoColumna cc)
        //            {
        //                return cv.NombreColumna == cc.NombreColumna;
        //            }
        //            );
        //        if (buscarcc == null)
        //            throw new PropiedadNoEncontradaException(cv.NombreColumna, this._nombre);
        //        campos.Add(buscarcc.NombreCampo);
        //        valores.Add(cv.Valor);
        //    }

        //    Dictionary<string,object> dic = _libbd.ObtenerEnDictionary(_nombretabla, campos, valores);
        //    if (dic == null)
        //        return null;
        //    return ConvertirDictionaryATipo(dic);
        //}

        public DataTable ObtenerEntre(string columna, string inf, string sup)
        {
            DataTable dt = _libbd.ObtenerEntre(_nombretabla,ObtenerCampo(columna),inf,sup);
            CambiarEsquemaDeCamposAColumnas(ref dt);
            return dt;
        }

        public DataTable ObtenerEntre(string columna,List<Dictionary<string,object>> listaDatosBetween)
        {
            DataTable dt = _libbd.ObtenerEntre(_nombretabla, listaDatosBetween);
            CambiarEsquemaDeCamposAColumnas(ref dt);
            return dt;
        }

        //public List<T> ObtenerEntreList(string columna, string limInf, string limSup) 
        //{
        //    DataTable dt = _libbd.ObtenerEntre(_nombretabla,ObtenerCampo(columna),limInf,limSup);
        //    List<T> devolver = this.ConvertirDataTableAList(dt);
        //    return devolver;
        //}

        //public DataTable ObtenerUnoEnDataTable(params ColumnaValor[] idsValores)
        //{
        //    DataTable devolver = ObtenerEsquema();
        //    DataRow dr = devolver.NewRow();
        //    T instancia = Obtener(idsValores);
        //    if (instancia == null)
        //        return null;
        //    foreach (PropertyInfo pi in Propiedades)
        //    {
        //        if (pi.PropertyType.BaseType.Name == "ElementoBase")
        //        {
        //            object ob = pi.GetValue(instancia, null);
        //            dr[pi.Name] = ob.GetType().GetProperty("Id").GetValue(ob, null);
        //        }
        //        else
        //        {
        //            object valor = pi.GetValue(instancia, null);
        //            if (valor == null)
        //            {
        //                dr[pi.Name] = DBNull.Value;
        //            }
        //            else
        //            {
        //                dr[pi.Name] = valor;
        //            }
        //        }
        //    }
        //    devolver.Rows.Add(dr);
        //    return devolver;
        //}

        public ObjetoInsert TipoAObjetoInsert(T elemento)
        {
            List<ValorInsert> lvi = new List<ValorInsert>();
            foreach (PropertyInfo pi in PropiedadesNoAutoincrementales.Except(PropiedadesNoMappeadas))
            {
                string valorParaValorInsert = "";
                object valor = pi.GetValue(elemento, null);
                if (valor != null)
                {
                    if (valor is decimal)
                    {
                        valorParaValorInsert = valor.ToString().Replace(',', '.');
                    }
                    else
                        valorParaValorInsert = valor.ToString();
                    ValorInsert vi = new ValorInsert(ObtenerCampo(pi.Name), valorParaValorInsert, true);
                    lvi.Add(vi);
                }
            }
            return new ObjetoInsert(_nombretabla, lvi);
        }

        public void Agregar(T elemento)
        {
            ObjetoInsert oi = TipoAObjetoInsert(elemento);
            try
            {
                _libbd.Insertar(oi);
            }
            catch (BaseDatosEntradaDuplicadaException exc)
            {
                throw new ModeloEntradaDuplicadaException(exc.ValorDuplicado, ObtenerColumna(exc.CampoDuplicado));
            }
        }

        protected Dictionary<string, object> ConvertirTipoADictionary(T objeto)
        {
            Dictionary<string, object> devolver = new Dictionary<string,object>();
            foreach (PropertyInfo pi in Propiedades.Except(PropiedadesNoMappeadas))
            {
                devolver.Add(ObtenerCampo(pi.Name),pi.GetValue(objeto,null));
            }
            return devolver;
        }

        protected List<ValorWhere> TipoAValorWhere(T objeto)
        {
            List<ValorWhere> devolver = new List<ValorWhere>();
            foreach (PropertyInfo pi in Propiedades.Except(PropiedadesNoMappeadas))
            {
                object valor = pi.GetValue(objeto,null);
                if (valor != null)
                devolver.Add(new ValorWhere(ObtenerCampo(pi.Name),false, pi.GetValue(objeto, null).ToString(),true, Signos.Igual));
            }
            return devolver;
        }

        protected List<ValorUpdate> TipoAValorUpdate(T objeto)
        {
            List<ValorUpdate> devolver = new List<ValorUpdate>();
            foreach (PropertyInfo pi in Propiedades.Except(PropiedadesNoMappeadas))
            {
                object valor = pi.GetValue(objeto, null);
                if (valor != null)
                    devolver.Add(new ValorUpdate(ObtenerCampo(pi.Name), pi.GetValue(objeto, null).ToString(), true));
            }
            return devolver;
        }

        public void AgregarVarios(List<T> elementos)
        {
            List<ObjetoInsert> loi = new List<ObjetoInsert>();
            foreach (T elemento in elementos)
            {
                loi.Add(TipoAObjetoInsert(elemento));
            }
            _libbd.Insertar(loi);
        }

        public LibreriaBD.ValorWhere ConvertirWhereModeloABase(ModeloWhere mw)
        {
            ValorWhere vw = new ValorWhere(ObtenerCampo(mw.NombrePropiedad), false, mw.ValorDerecha, mw.DerComillas, mw.Signo);
            return vw;
        }

        public void Eliminar(List<ModeloWhere> listaCondiciones)
        {
            List<ValorWhere> lvw = new List<ValorWhere>();
            foreach (ModeloWhere mw in listaCondiciones)
            {
                lvw.Add(ConvertirWhereModeloABase(mw));
            }
            _libbd.Eliminar(new ObjetoDelete(_nombretabla,lvw));
        }

        public void Eliminar(ModeloWhere condicion)
        {
            List<ModeloWhere> lmw = new List<ModeloWhere> { condicion };
            this.Eliminar(lmw);
        }

        Dictionary<string, string> ColumnaValoresADictionary(ColumnaValor[] cyv)
        {
            Dictionary<string, string> d = new Dictionary<string, string>();
            foreach (ColumnaValor cv in cyv)
            {
                d.Add(ObtenerCampo(cv.NombreColumna),cv.Valor);
            }
            return d;
        }

        public T ObtenerUltimo(string NombrePropiedad, params ModeloWhere[] condiciones)
        {
            string campo = ObtenerCampo(NombrePropiedad);
            List<ValorWhere> lvw = new List<ValorWhere>();
            foreach (ModeloWhere mw in condiciones)
            {
                lvw.Add(ConvertirWhereModeloABase(mw));
            }
            //Dictionary<string, string> dss = ColumnaValoresADictionary(cyv);
            Dictionary<string,object> obj = _libbd.ObtenerUltimo(_nombretabla,campo,lvw);
            if (obj != null)
                return ConvertirDictionaryATipo(obj);
            else
                return null;
        }
    }

    public class ModeloWhere
    {
        public string NombrePropiedad { get; set; }
        public string SignoString { get; set; }
        public bool DerComillas { get; set; }
        public string ValorDerecha { get; set; }
        public Signos Signo { get; set; }

        public ModeloWhere(string nombrePropiedad, string vDer, bool derComillas, Signos signo)
        {
            NombrePropiedad = nombrePropiedad;
            ValorDerecha = vDer;
            DerComillas = derComillas;
            Signo = signo;
            switch (signo)
            {
                case Signos.Distinto:
                    SignoString = "<>";
                    break;
                case Signos.Igual:
                    SignoString = "=";
                    break;
                case Signos.Mayor:
                    SignoString = ">";
                    break;
                case Signos.MayorIgual:
                    SignoString = ">=";
                    break;
                case Signos.Menor:
                    SignoString = "<";
                    break;
                case Signos.MenorIgual:
                    SignoString = "<=";
                    break;
            }
        }
    }

    //    public class ModeloUpdate : ValorUpdate
    //{
    //    public ModeloUpdate(string nombrePropiedad, string valornuevo, bool comillas)
    //        :base(nombrePropiedad,valornuevo,comillas)
    //    {
            
    //    }
    //}
}
