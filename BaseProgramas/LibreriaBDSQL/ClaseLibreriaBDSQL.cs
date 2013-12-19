using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibreriaBD
{
    public class ClaseLibreriaBDSQL: ClaseLibreriaBDBase
    {
        public ClaseLibreriaBDSQL(string connstring)
        {
            _connectionString = connstring;
            _connection = new SqlConnection(_connectionString);
        }

        public SqlConnection connection;

        public ClaseLibreriaBDSQL(string server, string db, bool integratedSecurity)
        {
            _connectionString = string.Format("Data Source={0};", server);
            _connectionString += "Integrated Security=";
            _connectionString += integratedSecurity ? "SSPI" : "No";
            _connectionString += ";Initial Catalog=" + db;
            _connection = new SqlConnection(_connectionString);
            _adapter = new SqlDataAdapter();
        }

        string _connectionString;
        SqlConnection _connection;
        SqlDataAdapter _adapter;
        SqlCommand _command;

        public override DataTable ObtenerNombresCampos(string tabla)
        {
            _command = new SqlCommand("SELECT column_name FROM information_schema.columns WHERE table_name = '" + tabla + "' ORDER BY ordinal_position", _connection);
            _command.CommandType = CommandType.Text;
            _adapter = new SqlDataAdapter(_command);
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            return dt;
        }
        
        public override DataTable ObtenerTodos(string tabla)
        {
            _adapter = new SqlDataAdapter();
            _command = new SqlCommand("SELECT * from " + tabla, _connection);
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
        List<Dictionary<string,object>> EjecutarConsulta(string consulta)
        {
            try
            {
                List<Dictionary<string, object>> devolver = new List<Dictionary<string, object>>();
                _command = _connection.CreateCommand();
                _command.CommandText = consulta;
                _connection.Open();
                SqlDataReader sqldr = _command.ExecuteReader();
                while (sqldr.Read())
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    for (int i = 0; i < sqldr.FieldCount; i++)
                    {
                        dict.Add(sqldr.GetName(i), sqldr.GetValue(i));
                    }
                    devolver.Add(dict);
                }
                _connection.Close();
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

        DataTable EjecutarConsultaDataTable(string consulta)
        {
            try
            {
                _adapter.SelectCommand = new SqlCommand(consulta,_connection);
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

        public static List<Dictionary<string, object>> EjecutarConsulta(string server, string bd, bool IntegratedSecurity, string consulta)
        {
            
            SqlConnectionStringBuilder sqlcsb = new SqlConnectionStringBuilder();
            sqlcsb.InitialCatalog = bd;
            sqlcsb.IntegratedSecurity = IntegratedSecurity;
            sqlcsb.DataSource = server;
            SqlConnection conn = new SqlConnection(sqlcsb.ToString());
            SqlCommand commando = new SqlCommand(consulta, conn);
            List<Dictionary<string, object>> devolver = new List<Dictionary<string, object>>();
            try
            {
                conn.Open();
                SqlDataReader sqldr = commando.ExecuteReader();
                while (sqldr.Read())
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    for (int i = 0; i < sqldr.FieldCount; i++)
                    {
                        dict.Add(sqldr.GetName(i), sqldr.GetValue(i));
                    }
                    devolver.Add(dict);
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
            finally
            {
                conn.Close();
            }
            return devolver;
        }

        string GenerarConsulta(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere)
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

        string GenerarConsultaExtra(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere, List<string> filtroExtra)
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
        public List<Dictionary<string, object>> Select(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere,Dictionary<string, string> camposCamposWhere)
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
            return EjecutarConsulta(consulta);
        }

        public DataTable SelectDataTable(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere, List<string> filtrosExtra)
        {
            string consulta = GenerarConsultaExtra(distinct, campos, tablasFrom, camposValoresWhere, camposCamposWhere, filtrosExtra);
            return EjecutarConsultaDataTable(consulta);
        }

        public DataTable ObtenerEntre(string tabla, string campo, string limiteinf, string limitesup)
        {
            string consulta = string.Format("SELECT * FROM {0} WHERE {1} BETWEEN '{2}' AND '{3}'", tabla, campo, limiteinf, limitesup);
            _adapter.SelectCommand = new SqlCommand(consulta, _connection);
            DataTable dt = new DataTable();
            try
            {
                _adapter.Fill(dt);
                return dt;
            }
            catch (SqlException exc)
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
        public DataTable ObtenerEntre(string tabla, List<Dictionary<string, object>> datosBetween)
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
            _adapter.SelectCommand = new SqlCommand(consulta, _connection);
            DataTable dt = new DataTable();
            try
            {
                _adapter.Fill(dt);
                return dt;
            }
            catch (SqlException exc)
            {
                if (exc.Number == 245)
                    throw new Exception("El tipo de dato ingresado no es correcto");
                else
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
            }
        }

        public DataTable Obtener(string tabla, List<string> listaCampos, List<string> listaDatos)
        {
            string consulta = "SELECT * FROM " + tabla + " WHERE ";
            foreach (string cd in listaCampos)
            {
                consulta += string.Format("({0} = '{1}') ", cd, listaDatos[listaCampos.IndexOf(cd)]);
                if (!(listaCampos.Last() == cd))
                    consulta += "AND ";
            }
            _adapter.SelectCommand = new SqlCommand(consulta, _connection);
            DataTable dt = new DataTable();
            try
            {
                _adapter.Fill(dt);
                return dt;
            }
            catch (SqlException exc)
            {
                if (exc.Number == 245)
                    throw new Exception("El tipo de dato ingresado no es correcto");
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
                }
            }
        }

        public Dictionary<string,object> ObtenerUltimo(string tabla,string campoUltimo, Dictionary<string, string> valoresWhere)
        {
            string consulta = "SELECT TOP(1) * from " + tabla;
            if (valoresWhere != null && valoresWhere.Count()!=0 )
            {
                consulta += " where ";
                foreach (string key in valoresWhere.Keys)
                {
                    consulta += string.Format("({0}='{1}') and", key, valoresWhere[key]);
                }
                consulta = consulta.Remove(consulta.Length - 3);
            }
            consulta +=" order by " + campoUltimo + " DESC";
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

        public Dictionary<string,object> ObtenerEnDictionary(string tabla, List<string> listaCampos, List<string> listaDatos)
        {
            string consulta = "SELECT * FROM " + tabla + " WHERE ";
            foreach (string cd in listaCampos)
            {
                consulta += string.Format("({0} = '{1}') ", cd, listaDatos[listaCampos.IndexOf(cd)]);
                if (!(listaCampos.Last() == cd))
                    consulta += "AND ";
            }
            try
            {
                List<Dictionary<string, object>> ld = EjecutarConsulta(consulta);
                if (ld.Count == 0)
                    return null;
                else
                    return ld[0];
            }
            catch (SqlException exc)
            {
                if (exc.Number == 245)
                    throw new Exception("El tipo de dato ingresado no es correcto");
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

        public void Modificar(string tabla, List<string> campos, List<string> valoresoriginales,List<string> valoresnuevos)
        {
            
            SqlCommandBuilder sqlcb = new SqlCommandBuilder(new SqlDataAdapter("select * from " + tabla, _connection));
            SqlCommand modificar = sqlcb.GetUpdateCommand(true);
            foreach (SqlParameter p in modificar.Parameters)
            {
                foreach (string s in campos)
                {
                    if (p.ParameterName == "@"+s)
                    {
                        int indice = campos.IndexOf(s);
                        p.Value = valoresnuevos[indice];
                        //campos.RemoveAt(indice);
                        //valoresnuevos.RemoveAt(indice);
                        break;
                    }
                    else
                        if (p.ParameterName == "@Original_" + s)
                        {
                            int indice = campos.IndexOf(s);
                            p.Value = valoresoriginales[indice];
                            break;
                        }
                }
            }
            try
            {
                _connection.Open();
                modificar.ExecuteNonQuery();
                //_connection.Close();
            }
            catch (SqlException exc)
            {
                if (exc.Number == 547)
                    throw new Exception("No puede modificar el Id de este elemento, porque hay otros relacionados a él");
                if (exc.Number == 2627)
                    throw new Exception("Ya existe un elemento con este Id. Por favor cámbielo y vuelva a intentarlo");
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        public void Modificar(string tabla, Dictionary<string,object> valoresNuevos, Dictionary<string,object> valoresWhere)
        {
            /*
            string cadenaset = "UPDATE "+tabla+" SET ";
            foreach (string s in valoresNuevos.Keys)
            {
                if (valoresNuevos[s] == null)
                    cadenaset += s + "= NULL,";
                else
                cadenaset += s + "='" + valoresNuevos[s]+"',";
            }
            cadenaset = cadenaset.Remove(cadenaset.Length-1);
            cadenaset += " WHERE ";
            foreach (string s in valoresWhere.Keys)
            {
                if (valoresWhere[s] == null)
                    cadenaset += "(" + s + " is NULL) AND";
                else
                cadenaset += "(" + s + "='" + valoresWhere[s]+ "') AND";
            }
            cadenaset = cadenaset.Remove(cadenaset.Length-3);
            SqlCommand modificar = new SqlCommand(cadenaset, _connection);*/
            SqlCommandBuilder sqlcb = new SqlCommandBuilder(new SqlDataAdapter("select * from " + tabla, _connection));
            SqlCommand modificar = sqlcb.GetUpdateCommand(true);
            foreach (string s in valoresNuevos.Keys)
            {
                try
                {
                    if (valoresNuevos[s] == null)
                    modificar.Parameters["@" + s].Value = DBNull.Value;
                    else
                        modificar.Parameters["@" + s].Value = valoresNuevos[s];
                }
                catch
                {
                    //no encuentra el parametro...
                }
            }
            foreach (string s in valoresWhere.Keys)
            {
                try
                {
                    if (valoresWhere[s]==null)
                        modificar.Parameters["@Original_" + s].Value = DBNull.Value;
                    else
                    modificar.Parameters["@Original_" + s].Value = valoresWhere[s];
                }
                catch
                {
                    //no encuentra el parametro porque el id no se puede cambiar
                }
            }
            try
            {
                _connection.Open();
                modificar.ExecuteNonQuery();
            }
            catch (SqlException exc)
            {
                if (exc.Number == 547)
                    throw new Exception("No puede modificar el Id de este elemento, porque hay otros relacionados a él");
                if (exc.Number == 2627)
                    throw new Exception("Ya existe un elemento con este Id. Por favor cámbielo y vuelva a intentarlo");
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        public int Modificar(string tabla, List<ValorUpdate> valoresUpdate, List<ValorWhere> valoresWhere)
        {
            if (tabla == null)
                throw new ArgumentNullException("tabla");
            if (valoresUpdate == null || valoresUpdate.Count == 0)
                throw new ArgumentNullException("lvu");
            string consulta = "update " + tabla + " set ";
            foreach (ValorUpdate vu in valoresUpdate)
            {
                consulta += vu.Campo + "="+(vu.ValorComillas?"'"+vu.ValorNuevo+"'":vu.ValorNuevo)+" ";
            }
            if (valoresWhere.Count != 0)
            {
                consulta += " where ";
                foreach (ValorWhere vw in valoresWhere)
                {
                    consulta += vw.ValorIzquierdaComillas ? string.Format("'{0}'", vw.ValorIzquierda) : vw.ValorIzquierda;
                    consulta += vw.Signo;
                    consulta += vw.ValorDerechaComillas ? string.Format("'{0}'", vw.ValorDerecha) : vw.ValorDerecha;
                    consulta += " and ";
                }
                consulta = consulta.Remove(consulta.Length - 4);
            }
            _command = new SqlCommand(consulta, _connection);
            try
            {
                _connection.Open();
                return _command.ExecuteNonQuery();
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

        public void Insertar(string tabla, List<string> columns, List<string> datos)
        {
            /*
            SqlCommandBuilder cb = new SqlCommandBuilder();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * from " + tabla, _connection);
            cb.DataAdapter = da;
            SqlCommand insertarcommand = cb.GetInsertCommand(true);
            foreach (SqlParameter p in insertarcommand.Parameters)
            {
                foreach (string s in columns)
                {
                    if (p.SourceColumn == s)
                    {
                        int indice = columns.IndexOf(s);
                        p.Value = datos[indice];
                        columns.RemoveAt(indice);
                        datos.RemoveAt(indice);
                        break;
                    }
                }
            }*/
            string into = null;
            foreach (string s in columns)
                into+=s+",";
            into = into.Remove(into.Length-1);
            string values = null;
            foreach (string s in datos)
                values += "'"+s+"',";
            values = values.Remove(values.Length-1);
            string consulta = string.Format("Insert into {0} ({1}) values ({2})",tabla,into,values);
            SqlCommand insertarcommand = new SqlCommand(consulta, _connection);
            try
            {
                _connection.Open();
                insertarcommand.ExecuteNonQuery();
                //_connection.Close();
            }
            catch (SqlException exc)
            {
                if (exc.Number == 2627)
                    throw new Exception("Ya existe un elemento con este Id. Por favor cámbielo y vuelva a intentarlo");
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

        public void Insertar(string tabla, Dictionary<string,object> datos)
        {
            //List<string> listacampos = new List<string>();
            //foreach (string s in datos.Keys)
            //{
            //    listacampos.Add(s);
            //}
            //List<string> listadatos = new List<string>();
            //foreach (object o in datos.Values)
            //{
            //    listadatos.Add(o.ToString());
            //}
            //Insertar(tabla, listacampos, listadatos);
            List<string> listacampos = new List<string>();
            List<string> listadatos = new List<string>();
            foreach (string s in datos.Keys)
            {
                object valor = datos[s];
                if (valor != null)
                {
                    listacampos.Add(s);
                    if (valor is decimal)
                    {
                        listadatos.Add(valor.ToString().Replace(',','.'));
                    }
                    else
                        listadatos.Add(valor.ToString());
                }
            }
            Insertar(tabla, listacampos, listadatos);
        }

        string GenerarInsertInto(string tabla, Dictionary<string, object> datos)
        {
            string devolver = null, into = null, values = null;
            foreach (string s in datos.Keys)
            {
                into += s+",";
                string valor;
                if (datos[s] is decimal)
                    valor = datos[s].ToString().Replace(',', '.');
                else
                    valor = datos[s].ToString();
                values += "'"+valor+"',";
            }
            into = into.Remove(into.Length - 1);
            values = values.Remove(values.Length - 1);
            devolver = string.Format("INSERT INTO {0} ({1}) VALUES ({2});",tabla,into,values);
            return devolver;
        }

        public void InsertarVarios(string tabla, Dictionary<string, object> datos, params Dictionary<string, object>[] masDatos)
        {
            string consulta = GenerarInsertInto(tabla, datos);
            foreach (Dictionary<string, object> d in masDatos)
            {
                consulta += GenerarInsertInto(tabla, d);
            }
            SqlCommand insertarcommand = new SqlCommand(consulta, _connection);
            try
            {
                _connection.Open();
                insertarcommand.ExecuteNonQuery();
                //_connection.Close();
            }
            catch (SqlException exc)
            {
                if (exc.Number == 2627)
                    throw new Exception(exc.Message);
                //throw new Exception("Ya existe un elemento con este Id. Por favor cámbielo y vuelva a intentarlo");
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

        string TransformarCondicion(ValorWhere valor)
        {
            string devolver = "";
            devolver += valor.ValorIzquierdaComillas ? string.Format("'{0}'", valor.ValorIzquierda) : valor.ValorIzquierda;
            devolver += valor.Signo;
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
                consulta += TransformarCondicion(vw)+" and ";
            }
            consulta = consulta.Remove(consulta.Length-4);
            _command = new SqlCommand(consulta, _connection);
            SqlDataReader reader;
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

        public void Eliminar(string tabla, List<string> campos, List<string> datos)
        {
            SqlCommandBuilder cb = new SqlCommandBuilder();
            SqlDataAdapter da = new SqlDataAdapter("Select * from " + tabla,_connection);
            cb.DataAdapter = da;
            //SqlCommand deletecommand = cb.GetDeleteCommand(true);
            string wheresql = "WHERE ";
            for (int i = 0; i < campos.Count; i++)
			{
			    wheresql += "("+campos[i]+"='"+datos[i]+"')";
                if (i!=campos.Count-1)
                    wheresql += " AND ";
			}
            SqlCommand deletecommand = new SqlCommand("DELETE from " + tabla + " "+wheresql,_connection);
            foreach (SqlParameter p in deletecommand.Parameters)
            {
                foreach (string s in campos)
                {
                    if (p.SourceColumn == s)
                    {
                        int indice = campos.IndexOf(s);
                        p.Value = datos[indice];
                        campos.RemoveAt(indice);
                        datos.RemoveAt(indice);
                        break;
                    }
                }
            }
            try
            {
                _connection.Open();
                deletecommand.ExecuteNonQuery();
                //_connection.Close();
            }
            catch (SqlException exc)
            {
                if (exc.Number == 547)
                    throw new Exception("Existen relaciones hacia este elemento. Elimínelas y vuelva a intentarlo");
                else
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + deletecommand.CommandText);
                }
            }
            finally
            {
                _connection.Close();
            }
        }

        public string GenerarDeleteFrom(string tabla, Dictionary<string, object> where)
        {
            string whereCadena = "";
            foreach (string key in where.Keys)
            {
                whereCadena += string.Format("({0} = '{1}') AND ", key, where[key].ToString());
            }
            whereCadena = whereCadena.Remove(whereCadena.Length - 4);
            return string.Format("DELETE FROM {0} WHERE {1};", tabla, whereCadena);
        }

        public void EliminarVarios(string tabla, List<Dictionary<string, object>> datos)
        {
            string consulta ="";
            foreach (Dictionary<string, object> d in datos)
            {
                consulta += GenerarDeleteFrom(tabla, d);
            }
            SqlCommand eliminarcommand = new SqlCommand(consulta, _connection);
            try
            {
                _connection.Open();
                eliminarcommand.ExecuteNonQuery();
                //_connection.Close();
            }
            catch (SqlException exc)
            {
                if (exc.Number == 2627)
                    throw new Exception(exc.Message);
                //throw new Exception("Ya existe un elemento con este Id. Por favor cámbielo y vuelva a intentarlo");
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

        public DataTable ObtenerEsquema(string tabla)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * from " + tabla, _connection);
            DataTable dt = new DataTable();
            return da.FillSchema(dt, SchemaType.Source);
        }        

        //Empresas

        static SqlConnection _connectionStatic;
        static SqlCommand _commandStatic;

        static public List<Dictionary<string, object>> EjecutarConsultaStatic(string consulta)
        {
            _connectionStatic = new SqlConnection("Data Source=localhost;database=master; integrated security=yes");
            List<Dictionary<string, object>> devolver = new List<Dictionary<string, object>>();
            _commandStatic = _connectionStatic.CreateCommand();
            _commandStatic.CommandText = consulta;
            _connectionStatic.Open();
            SqlDataReader sqldr = _commandStatic.ExecuteReader();
            while (sqldr.Read())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < sqldr.FieldCount; i++)
                {
                    dict.Add(sqldr.GetName(i), sqldr.GetValue(i));
                }
                devolver.Add(dict);
            }
            _connectionStatic.Close();
            return devolver;

        }

        static public List<Dictionary<string, object>> EjecutarConsultaTablas(string consulta, string labase)
        {
            _connectionStatic = new SqlConnection("Data Source=localhost;database=" + labase + "; integrated security=yes");
            List<Dictionary<string, object>> devolver = new List<Dictionary<string, object>>();
            _commandStatic = _connectionStatic.CreateCommand();
            _commandStatic.CommandText = consulta;
            _connectionStatic.Open();
            SqlDataReader sqldr = _commandStatic.ExecuteReader();
            while (sqldr.Read())
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                for (int i = 0; i < sqldr.FieldCount; i++)
                {
                    dict.Add(sqldr.GetName(i), sqldr.GetValue(i));
                }
                devolver.Add(dict);
            }
            _connectionStatic.Close();
            return devolver;

        }


        public String[] basesDeDatos(string instancia)
        {
            // Las bases de datos propias de SQL Server
            string[] basesSys = { "master", "model", "msdb", "tempdb" };
            string[] bases;
            DataTable dt = new DataTable();
            // Usamos la seguridad integrada de Windows
            string sCnn = "Server=" + instancia + "; database=master; integrated security=yes";

            // La orden T-SQL para recuperar las bases de master
            string sel = "SELECT name FROM sysdatabases";
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sel, sCnn);
                da.Fill(dt);
                bases = new string[dt.Rows.Count - 1];
                int k = -1;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string s = dt.Rows[i]["name"].ToString();
                    // Solo asignar las bases que no son del sistema
                    if (Array.IndexOf(basesSys, s) == -1)
                    {
                        k += 1;
                        bases[k] = s;
                    }
                }
                if (k == -1) return null;
                // ReDim Preserve
                {
                    int i1_RPbases = bases.Length;
                    string[] copiaDe_bases = new string[i1_RPbases];
                    Array.Copy(bases, copiaDe_bases, i1_RPbases);
                    bases = new string[(k + 1)];
                    Array.Copy(copiaDe_bases, bases, (k + 1));
                };
                return bases;

            }
            catch (Exception ex)
            {
                /*MessageBox.Show(ex.Message,
                    "Error al recuperar las bases de la instancia indicada",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);*/
            }
            return null;
        }
    }
}
