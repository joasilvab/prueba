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

        public ClaseLibreriaBDMySQL(string server, string db, bool integratedSecurity)
        {
            _connectionString = string.Format("Data Source={0};", server);
            _connectionString += "Integrated Security=";
            _connectionString += integratedSecurity ? "SSPI" : "No";
            _connectionString += ";Initial Catalog=" + db;
            _connection = new MySqlConnection(_connectionString);
            _adapter = new MySqlDataAdapter();
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
            try
            {
                List<Dictionary<string, object>> devolver = new List<Dictionary<string, object>>();
                _command = _connection.CreateCommand();
                _command.CommandText = consulta;
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
        public override List<Dictionary<string, object>> Select(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere,Dictionary<string, string> camposCamposWhere)
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

        public override DataTable Obtener(string tabla, List<string> listaCampos, List<string> listaDatos)
        {
            string consulta = "SELECT * FROM " + tabla + " WHERE ";
            foreach (string cd in listaCampos)
            {
                consulta += string.Format("({0} = '{1}') ", cd, listaDatos[listaCampos.IndexOf(cd)]);
                if (!(listaCampos.Last() == cd))
                    consulta += "AND ";
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
                {
                    throw new Exception("Mensaje: " + exc.Message + "\n\n" + "Consulta: " + _adapter.SelectCommand.CommandText);
                }
            }
        }

        public override Dictionary<string,object> ObtenerUltimo(string tabla,string campoUltimo, Dictionary<string, string> valoresWhere)
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

        public override Dictionary<string,object> ObtenerEnDictionary(string tabla, List<string> listaCampos, List<string> listaDatos)
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
            catch (MySqlException exc)
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

        public override int Modificar(string tabla, List<ValorUpdate> valoresUpdate, List<ValorWhere> valoresWhere)
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
            _command = new MySqlCommand(consulta, _connection);
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

        public override void Insertar(List<ObjetoInsert> listaInserts)
        {
            throw new NotImplementedException();
        }

        public override void EliminarVarios(List<ObjetoDelete> listaDeletes)
        {
            throw new NotImplementedException();
        }

        public override List<Dictionary<string, object>> EjecutarConsulta(string server, string bd, bool IntegratedSecurity, string consulta)
        {
            throw new NotImplementedException();
        }

        string GenerarInsertInto(ObjetoInsert ins)
        {   
            string devolver=null, into = null, values = null;
            for(int i= 0; i<ins.ValoresInsert.Count;i++)
            {
                into = ins.ValoresInsert[i].Into;
                values = ins.ValoresInsert[i].Value;
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

        string GenerarDeleteFrom(ObjetoDelete od)
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
    }
}
