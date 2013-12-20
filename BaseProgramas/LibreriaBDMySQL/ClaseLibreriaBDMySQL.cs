using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace LibreriaBD
{
    public class ClaseLibreriaBDMySQL: ClaseLibreriaBDBase
    {
        public ClaseLibreriaBDMySQL(string connstring)
        {
            _connectionString = connstring;
            _connection = new MySqlConnection(_connectionString);
        }

        private BaseDatosException LanzarExcepcion(MySqlException exc)
        {
            switch (exc.Number)
            {
                case 1042:
                    return new BaseDatosSinConexionException(exc.Message);
            }
            return new BaseDatosException(exc.Message);
        }

        public ClaseLibreriaBDMySQL(string server, string db, string user, string pass)
        {
            _connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3}", server, db, user, pass);
            _connection = new MySqlConnection(_connectionString);
        }

        public bool ProbarConexion()
        {
            try
            {
                _connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                if (e.Number == 1042)
                    throw new BaseDatosSinConexionException(e.Message);
                else
                    throw new BaseDatosSinConexionException(e.InnerException.Message);
            }
            finally
            {
                _connection.Close();
            }
        }

        string _connectionString;
        MySqlConnection _connection;
        MySqlDataAdapter _adapter;
        MySqlCommand _command;

        public override DataTable ObtenerNombresCampos(string tabla)
        {
            _command = new MySqlCommand("SELECT column_name FROM information_schema.columns WHERE table_name = '" + tabla + "' ORDER BY ordinal_position", _connection);
            _command.CommandType = CommandType.Text;
            _adapter = new MySqlDataAdapter(_command);
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            return dt;
        }
        
        public override DataTable ObtenerTodos(string tabla)
        {
            _adapter = new MySqlDataAdapter();
            _command = new MySqlCommand("SELECT * from " + tabla, _connection);
            _command.CommandType = CommandType.Text;
            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            try
            {
                _adapter.Fill(dt);
            }
            catch (MySqlException exc)
            {
                throw LanzarExcepcion(exc);
            }
            return dt;
        }

        public override DataTable ObtenerTodos(string tabla, List<string> campos)
        {
            _adapter = new MySqlDataAdapter();
            string camposstring ="";
            foreach (string s in campos)
            {
                camposstring += s + ",";
            }
            camposstring = RemoverUltimosCaracteres(camposstring, 1);
            _command = new MySqlCommand("SELECT "+camposstring+" from " + tabla, _connection);
            _command.CommandType = CommandType.Text;
            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            return dt;
        }
         

        public override List<Dictionary<string,object>> ObtenerTodosDataReader(string tabla)
        {
            return EjecutarConsulta("Select * from " + tabla);
        }
        /*
        public bool VerificarCampoExistente(string tabla, string campo)
        {
            return true;
        }*/

        /*
        DataTable ObtenerIds()
        {
            DataTable dt = new DataTable();
            SqlCommand com = new SqlCommand("SELECT COLUMN_NAME FROM ABM.INFORMATION_SCHEMA.TABLE_CONSTRAINTS as TC, ABM.INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE as CCU WHERE (TC.TABLE_NAME = 'personas') and (TC.CONSTRAINT_TYPE = 'PRIMARY KEY') and (CCU.TABLE_NAME = 'personas') and (TC.CONSTRAINT_NAME = CCU.CONSTRAINT_NAME)",_connection);
            SqlDataAdapter adapter = new SqlDataAdapter(com);
            adapter.Fill(dt);
            return dt;
        }*/
        public override List<Dictionary<string,object>> EjecutarConsulta(string consulta)
        {
            List<Dictionary<string, object>> devolver = new List<Dictionary<string, object>>();
            _command = _connection.CreateCommand();
            _command.CommandText = consulta;
            try
            {
                _connection.Open();
                MySqlDataReader sqldr = _command.ExecuteReader();
                while (sqldr.Read())
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    for (int i = 0; i < sqldr.FieldCount; i++)
                    {
                        dict.Add(sqldr.GetName(i), sqldr.GetValue(i));
                    }
                    devolver.Add(dict);
                }
                return devolver;
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                _connection.Close();
            }
        }

        public override DataTable EjecutarConsultaDataTable(string consulta)
        {
            try
            {
                _adapter.SelectCommand = new MySqlCommand(consulta,_connection);
                DataTable dt = new DataTable();
                _adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
            finally
            {
                _connection.Close();
            }
        }

        //public static List<Dictionary<string, object>> EjecutarConsulta(string server, string bd, bool IntegratedSecurity, string consulta)
        //{
            
        //    MySqlConnectionStringBuilder sqlcsb = new MySqlConnectionStringBuilder();
        //    sqlcsb.InitialCatalog = bd;
        //    sqlcsb.IntegratedSecurity = IntegratedSecurity;
        //    sqlcsb.DataSource = server;
        //    SqlConnection conn = new SqlConnection(sqlcsb.ToString());
        //    SqlCommand commando = new SqlCommand(consulta, conn);
        //    List<Dictionary<string, object>> devolver = new List<Dictionary<string, object>>();
        //    try
        //    {
        //        conn.Open();
        //        SqlDataReader sqldr = commando.ExecuteReader();
        //        while (sqldr.Read())
        //        {
        //            Dictionary<string, object> dict = new Dictionary<string, object>();
        //            for (int i = 0; i < sqldr.FieldCount; i++)
        //            {
        //                dict.Add(sqldr.GetName(i), sqldr.GetValue(i));
        //            }
        //            devolver.Add(dict);
        //        }
        //    }
        //    catch (Exception exc)
        //    {
        //        throw exc;
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //    return devolver;
        //}

        protected override string GenerarConsulta(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere)
        {
            if (campos == null)
                throw new ArgumentNullException("campos");
            if (tablasFrom == null)
                throw new ArgumentNullException("tablasFrom");
            string consulta = "Select ";
            if (distinct) consulta += "DISTINCT ";
            //consulta += "* FROM ";
            foreach (string s in campos) consulta += s + " ,";
            consulta = consulta.Remove(consulta.Length - 1) + "FROM ";
            foreach (string s in tablasFrom) consulta += s + " ,";
            consulta = consulta.Remove(consulta.Length - 1);
            if ((camposValoresWhere != null) || (camposCamposWhere != null))
            {
                consulta += "WHERE ";
                if (camposValoresWhere != null)
                    foreach (string s in camposValoresWhere.Keys)
                    {
                        object valor = camposValoresWhere[s];
                        if (valor == null)
                            consulta += string.Format("{0} is NULL AND ", s);
                        else
                            consulta += string.Format("{0}='{1}' AND ", s, camposValoresWhere[s]);
                    }
                if (camposCamposWhere != null)
                    foreach (string s in camposCamposWhere.Keys)
                    {
                        consulta += string.Format("{0}={1} AND ", s, camposCamposWhere[s]);
                    }
                consulta = consulta.Remove(consulta.Length - 4);
            }
            return consulta;
        }

        protected override string GenerarConsultaExtra(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere, List<string> filtroExtra)
        {
            string consulta = GenerarConsulta(distinct, campos, tablasFrom, camposValoresWhere, camposCamposWhere);
            foreach (string s in filtroExtra)
            {
                consulta += " and (" + s + ")";
            }
            return consulta;
        }

        /// <summary>
        /// Ejecuta una instruccion SELECT
        /// </summary>
        /// <param name="distinct">Indica si aparece DISTINCT (no repetidos)</param>
        /// <param name="campos">Campos a devolver</param>
        /// <param name="tablasFrom">Tablas que van en la clausula FROM</param>
        /// <param name="camposValoresWhere">Dictionary donde se indica key = valor para el WHERE (ej. desc = 'Descripcion')</param>
        /// <param name="camposCamposWhere">Dictionary donde se indica campo = otrocampo (es como el anterior pero le saca las comillas, ej. tabla1.campo = tabla2.campo)</param>
        /// <returns>Devuelve un Dictionary donde las keys son los nombre de los campos y los valores, son los valores de esos campos</returns>
        public override List<Dictionary<string, object>> Select(bool distinct, List<string> campos, List<string> tablasFrom, List<ValorWhere> listaCondiciones)
        {
            if (campos == null)
                throw new ArgumentNullException("campos");
            if (tablasFrom == null)
                throw new ArgumentNullException("tablasFrom");
            string consulta = "Select ";
            if (distinct) consulta += "DISTINCT ";
            //consulta += "* FROM ";
            foreach (string s in campos) consulta += s + " ,";
            consulta = consulta.Remove(consulta.Length - 1) + "FROM ";
            foreach (string s in tablasFrom) consulta += s + " ,";
            consulta = consulta.Remove(consulta.Length - 1);
            if (listaCondiciones != null && listaCondiciones.Count > 0)
            {
                consulta += "WHERE ";
                foreach (ValorWhere vw in listaCondiciones)
                {
                    consulta += GenerarCondicion(vw);
                    consulta += " AND ";
                }
                //if (camposValoresWhere != null)
                //    foreach (string s in camposValoresWhere.Keys)
                //    {
                //        object valor = camposValoresWhere[s];
                //        if (valor == null)
                //        consulta += string.Format("{0} is NULL AND ", s);
                //        else
                //            consulta += string.Format("{0}='{1}' AND ", s, camposValoresWhere[s]);
                //    }
                //if (camposCamposWhere != null)
                //    foreach (string s in camposCamposWhere.Keys)
                //    {
                //        consulta += string.Format("{0}={1} AND ", s, camposCamposWhere[s]);
                //    }
                consulta = consulta.Remove(consulta.Length - 4);
            }
            return EjecutarConsulta(consulta);
        }

        public override DataTable SelectDataTable(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere, List<string> filtrosExtra)
        {
            string consulta = GenerarConsultaExtra(distinct, campos, tablasFrom, camposValoresWhere, camposCamposWhere, filtrosExtra);
            return EjecutarConsultaDataTable(consulta);
        }

        public override DataTable ObtenerEntre(string tabla, string campo, string limiteinf, string limitesup)
        {
            string consulta = string.Format("SELECT * FROM {0} WHERE {1} BETWEEN '{2}' AND '{3}'", tabla, campo, limiteinf, limitesup);
            _adapter.SelectCommand = new MySqlCommand(consulta, _connection);
            DataTable dt = new DataTable();
            try
            {
                _adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 245)
                    throw new Exception("El tipo de dato ingresado no es correcto");
                else
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
            }
        }

        /// <summary>
        /// Obtener entre
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="datosBetween">List de dictionarys, cada uno con las claves "campo","desde" y "hasta" con sus respectivos valores</param>
        /// <returns></returns>
        public override DataTable ObtenerEntre(string tabla, List<Dictionary<string, object>> datosBetween)
        {
            //string consulta = string.Format("SELECT * FROM {0} WHERE {1} BETWEEN '{2}' AND '{3}'", tabla, campo, limiteinf, limitesup);
            string consulta = string.Format("SELECT * FROM {0} ", tabla);
            if (datosBetween.Count != 0)
            {
                consulta += "WHERE ";
                foreach (Dictionary<string, object> dic in datosBetween)
                {
                    string between = string.Format("({0} BETWEEN '{1}' AND '{2}') AND", dic["campo"], dic["desde"], dic["hasta"]);
                    consulta += between;
                }
                consulta = consulta.Remove(consulta.Length - 3);
            }
            _adapter.SelectCommand = new MySqlCommand(consulta, _connection);
            DataTable dt = new DataTable();
            try
            {
                _adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 245)
                    throw new Exception("El tipo de dato ingresado no es correcto");
                else
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
            }
        }

        public override DataTable Obtener(string tabla, List<ValorWhere> condiciones)
        {
            string consulta = "SELECT * FROM " + tabla;
            if (condiciones != null && condiciones.Count > 0)
            {
                consulta += " WHERE ";
                foreach (ValorWhere vw in condiciones)
                {
                    consulta += GenerarCondicion(vw) + " AND ";
                }
                consulta = RemoverUltimosCaracteres(consulta, 4);
            }
            _adapter = new MySqlDataAdapter(consulta, _connection);
            //_adapter.SelectCommand = new MySqlCommand(consulta, _connection);
            DataTable dt = new DataTable();
            try
            {
                _adapter.Fill(dt);
                return dt;
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 245)
                    throw new Exception("El tipo de dato ingresado no es correcto");
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
                }
            }
        }

        public override Dictionary<string,object> ObtenerUltimo(string tabla,string campoUltimo, List<ValorWhere> valoresWhere)
        {
            string consulta = "SELECT * from " + tabla;
            if (valoresWhere != null && valoresWhere.Count == 0)
            {
                consulta += " WHERE ";
                foreach (ValorWhere vw in valoresWhere)
                {
                    consulta += GenerarCondicion(vw) + " and ";
                }
                consulta = RemoverUltimosCaracteres(consulta, 4);
            }
            //if (valoresWhere != null && valoresWhere.Count()!=0 )
            //{
            //    consulta += " where ";
            //    foreach (string key in valoresWhere.Keys)
            //    {
            //        consulta += string.Format("({0}='{1}') and", key, valoresWhere[key]);
            //    }
            //    consulta = consulta.Remove(consulta.Length - 3);
            //}
            consulta +=" order by " + campoUltimo + " DESC LIMIT 1";
            try
            {
                List<Dictionary<string, object>> ld = EjecutarConsulta(consulta);
                if (ld.Count != 0)
                    return ld[0];
                else
                    return null;
            }
            catch (Exception exc)
            {
                throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + consulta);
            }
        }

        public override Dictionary<string,object> ObtenerEnDictionary(string tabla, List<ValorWhere> listaCondiciones)
        {
            string consulta = "SELECT * FROM " + tabla + " WHERE ";
            foreach (ValorWhere ve in listaCondiciones)
            {
                consulta += GenerarCondicion(ve)+" and ";
            }
            consulta = consulta.Remove(consulta.Length - 4)+" LIMIT 1";

            try
            {
                List<Dictionary<string, object>> ld = EjecutarConsulta(consulta);
                if (ld.Count == 0)
                    return null;
                else
                    return ld[0];
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 245)
                    throw new Exception("El tipo de dato ingresado no es correcto");
                if (exc.Number == 1042)
                {
                    throw new LibreriaBD.BaseDatosSinConexionException(exc.Message);
                }
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + consulta);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        public string GenerarUpdate(string tabla, List<ValorUpdate> listaUpdates, List<ValorWhere> condiciones)
        {
            string devolver = "UPDATE " + tabla + " SET ";
            foreach (ValorUpdate vu in listaUpdates)
            {
                devolver += vu.Campo + "=" + (vu.ValorComillas ? "'" + vu.ValorNuevo + "'" : vu.ValorNuevo) + ",";
            }
            devolver = RemoverUltimosCaracteres(devolver, 1);
            if (condiciones != null && condiciones.Count > 0)
            {
                devolver += " WHERE ";
                foreach (ValorWhere vw in condiciones)
                {
                    devolver += GenerarCondicion(vw);
                    devolver += " AND ";
                }
                devolver = RemoverUltimosCaracteres(devolver, 4);
            }
            return devolver+";";
        }

        public override int Modificar(string tabla, List<ValorUpdate> valoresUpdate, List<ValorWhere> valoresWhere)
        {
            if (tabla == null)
                throw new ArgumentNullException("tabla");
            if (valoresUpdate == null || valoresUpdate.Count == 0)
                throw new ArgumentNullException("lvu");
            //string consulta = "update " + tabla + " set ";
            //foreach (ValorUpdate vu in valoresUpdate)
            //{
            //    consulta += vu.Campo + "="+(vu.ValorComillas?"'"+vu.ValorNuevo+"'":vu.ValorNuevo)+",";
            //}
            //consulta = RemoverUltimosCaracteres(consulta, 1);
            //if (valoresWhere.Count != 0)
            //{
            //    consulta += " where ";
            //    foreach (ValorWhere vw in valoresWhere)
            //    {
            //        consulta += vw.ValorIzquierdaComillas ? string.Format("'{0}'", vw.ValorIzquierda) : vw.ValorIzquierda;
            //        consulta += vw.Signo;
            //        consulta += vw.ValorDerechaComillas ? string.Format("'{0}'", vw.ValorDerecha) : vw.ValorDerecha;
            //        consulta += " and ";
            //    }
            //    consulta = consulta.Remove(consulta.Length - 4);
            //}
            string consulta = GenerarUpdate(tabla, valoresUpdate, valoresWhere);
            _command = new MySqlCommand(consulta, _connection);
            try
            {
                _connection.Open();
                return _command.ExecuteNonQuery();
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 1062)
                {
                    string m = exc.Message;
                    string a = m.Substring(m.IndexOf('\'') + 1);
                    string valor = a.Remove(a.IndexOf('\''));
                    string c = a.Substring(a.IndexOf('\'') + 1);
                    string d = c.Substring(c.IndexOf('\'') + 1);
                    string campo = d.Remove(d.IndexOf('\''));
                    throw new BaseDatosEntradaDuplicadaException(valor, campo);
                    //exc.Message.Substring(esc.me
                }
                else
                {
                    throw exc;
                }
            }
            finally
            {
                _connection.Close();
            }
        }


        public override void Insertar(ObjetoInsert insert)
        {
            string consulta = GenerarInsertInto(insert);
            _command = new MySqlCommand(consulta, _connection);
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 2627)
                    throw new Exception("Ya existe un elemento con este Id. Por favor cámbielo y vuelva a intentarlo");
                if (exc.Number == 1062)
                {
                    //string m = exc.Message;
                    //string a = m.Substring(m.IndexOf('\'')+1);
                    //string valor = a.Remove(a.IndexOf('\''));
                    //string c = a.Substring(a.IndexOf('\'')+1);
                    //string d = c.Substring(c.IndexOf('\'')+1);
                    //string campo = d.Remove(d.IndexOf('\''));
                    string valor, campo;
                    ObtenerValorCampoDuplicado(exc.Message, out valor, out campo);
                    throw new BaseDatosEntradaDuplicadaException(valor, campo);
                    //exc.Message.Substring(esc.me
                }
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + consulta);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        void ObtenerValorCampoDuplicado(string mensageException, out string valor, out string campo)
        {
            string m = mensageException;
            string a = m.Substring(m.IndexOf('\'') + 1);
            valor = a.Remove(a.IndexOf('\''));
            string c = a.Substring(a.IndexOf('\'') + 1);
            string d = c.Substring(c.IndexOf('\'') + 1);
            campo = d.Remove(d.IndexOf('\''));
        }

        public override void Insertar(List<ObjetoInsert> listaInserts)
        {
            if (listaInserts == null || listaInserts.Count == 0)
                throw new ArgumentNullException("listaInserts");
            string consulta = "";
            foreach (ObjetoInsert oi in listaInserts)
            {
                consulta += GenerarInsertInto(oi);
            }
            _command = new MySqlCommand(consulta, _connection);
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 2627)
                    throw new Exception("Ya existe un elemento con este Id. Por favor cámbielo y vuelva a intentarlo");
                if (exc.Number == 1062)
                {
                    string valor, campo;
                    ObtenerValorCampoDuplicado(exc.Message, out valor, out campo);
                    throw new BaseDatosEntradaDuplicadaException(valor, campo);
                 
                }
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + consulta);
                }
            }
            finally
            {
                _connection.Close();
            }
            
        }

        public override void EliminarVarios(List<ObjetoDelete> listaDeletes)
        {
            throw new NotImplementedException();
        }

        public override List<Dictionary<string, object>> EjecutarConsulta(string server, string bd, bool IntegratedSecurity, string consulta)
        {
            throw new NotImplementedException();
        }

        public string GenerarInsertInto(ObjetoInsert ins)
        {   
            string devolver=null, into = null, values = null;
            for(int i= 0; i<ins.ValoresInsert.Count;i++)
            {
                into += ins.ValoresInsert[i].Into;
                values += ins.ValoresInsert[i].ValueComillas? "'"+ins.ValoresInsert[i].Value+"'":ins.ValoresInsert[i].Value;
                if (i != ins.ValoresInsert.Count - 1)
                {
                    into += ",";
                    values += ",";
                }
            }
            devolver = string.Format("INSERT INTO {0} ({1}) VALUES ({2});",ins.Tabla,into,values);
            return devolver;
        }

        string GenerarCondicion(ValorWhere valor)
        {
            string devolver = "";
            devolver += valor.ValorIzquierdaComillas ? string.Format("'{0}'", valor.ValorIzquierda) : valor.ValorIzquierda;
            devolver += valor.Signo;
            if (valor.ValorDerecha == null)
                devolver += "NULL";
            else
                devolver += valor.ValorDerechaComillas ? string.Format("'{0}'", valor.ValorDerecha) : valor.ValorDerecha;
            return devolver;
        }

        public override decimal Sum(string tabla,string campoASumar, List<ValorWhere> condiciones)
        {
            string consulta = string.Format("select sum({0}) from {1}",campoASumar,tabla);
            if (condiciones.Count > 0)
                consulta+=" where ";
            foreach (ValorWhere vw in condiciones)
            {
                consulta += GenerarCondicion(vw)+" and ";
            }
            consulta = consulta.Remove(consulta.Length-4);
            _command = new MySqlCommand(consulta, _connection);
            MySqlDataReader reader;
            try
            {
                _connection.Open();
                reader = _command.ExecuteReader();
                reader.Read();
                decimal devolver = (decimal)reader.GetValue(0);
                //decimal devolver = reader.GetDecimal(0);
                return devolver;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                _connection.Close();
            }
        }

        public override void Eliminar(ObjetoDelete od)
        {
            _command = new MySqlCommand(GenerarDeleteFrom(od), _connection);
            try
            {
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            catch (MySqlException exc)
            {
                if (exc.Number == 547)
                    throw new Exception("Existen relaciones hacia este elemento. Elimínelas y vuelva a intentarlo");
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _command.CommandText);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        string RemoverUltimosCaracteres(string cadena,int cant)
        {
            return cadena.Remove(cadena.Length - cant);
        }

        public string GenerarDeleteFrom(ObjetoDelete od)
        {
            string whereCadena = "";
            foreach (ValorWhere vw in od.ListaValoresWhere)
            {
                whereCadena += "(" + GenerarCondicion(vw) + ") and ";
            }
            whereCadena = RemoverUltimosCaracteres(whereCadena, 4);
            return string.Format("DELETE FROM {0} WHERE {1};", od.Tabla, whereCadena);
        }

        public override DataTable ObtenerEsquema(string tabla)
        {
            MySqlDataAdapter da = new MySqlDataAdapter("SELECT * from " + tabla, _connection);
            DataTable dt = new DataTable();
            return da.FillSchema(dt, SchemaType.Source);
        }

        public void Transaction(List<string> listaSentencias)//Creo que debe ser static
        {
            string consulta = "";
            foreach (string s in listaSentencias)
            {
                consulta += s;
            }
            _command = new MySqlCommand(consulta, _connection);
            _connection.Open();
            MySqlTransaction trans = _connection.BeginTransaction();
            _command.Transaction = trans;
            try
            {
                _command.ExecuteNonQuery();
                trans.Commit();
            }
            catch (MySqlException mysqlex)
            {
                switch (mysqlex.Number)
                {
                    case 1062:
                        string campo, valor;
                        ObtenerValorCampoDuplicado(mysqlex.Message, out valor, out campo);
                        throw new BaseDatosEntradaDuplicadaException(valor, campo);
                    default:
                        throw new Exception(mysqlex.Message);
                }
            }
            catch (Exception exc)
            {
                trans.Rollback();
                throw exc;
            }
            finally
            {
                _connection.Close();
            }
            
        }
    }
}
