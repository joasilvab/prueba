using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LibreriaBD
{
    public abstract class ClaseLibreriaBDBase
    {
        public abstract DataTable ObtenerNombresCampos(string tabla);

        public abstract DataTable ObtenerTodos(string tabla);

        public abstract DataTable ObtenerTodos(string tabla, List<string> campos);

        public abstract List<Dictionary<string, object>> ObtenerTodosDataReader(string tabla);

        public abstract List<Dictionary<string, object>> EjecutarConsulta(string consulta);

        public abstract DataTable EjecutarConsultaDataTable(string consulta);

        public abstract List<Dictionary<string, object>> EjecutarConsulta(string server, string bd, bool IntegratedSecurity, string consulta);

        protected abstract string GenerarConsulta(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere);

        protected abstract string GenerarConsultaExtra(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere, List<string> filtroExtra);

        /// <summary>
        /// Ejecuta una instruccion SELECT
        /// </summary>
        /// <param name="distinct">Indica si aparece DISTINCT (no repetidos)</param>
        /// <param name="campos">Campos a devolver</param>
        /// <param name="tablasFrom">Tablas que van en la clausula FROM</param>
        /// <param name="camposValoresWhere">Dictionary donde se indica key = valor para el WHERE (ej. desc = 'Descripcion')</param>
        /// <param name="camposCamposWhere">Dictionary donde se indica campo = otrocampo (es como el anterior pero le saca las comillas, ej. tabla1.campo = tabla2.campo)</param>
        /// <returns>Devuelve un Dictionary donde las keys son los nombre de los campos y los valores, son los valores de esos campos</returns>
        public abstract List<Dictionary<string, object>> Select(bool distinct, List<string> campos, List<string> tablasFrom, List<ValorWhere> condiciones);

        public abstract DataTable SelectDataTable(bool distinct, List<string> campos, List<string> tablasFrom, Dictionary<string, string> camposValoresWhere, Dictionary<string, string> camposCamposWhere, List<string> filtrosExtra);

        public abstract DataTable ObtenerEntre(string tabla, string campo, string limiteinf, string limitesup);

        /// <summary>
        /// Obtener entre
        /// </summary>
        /// <param name="tabla">Nombre de la tabla</param>
        /// <param name="datosBetween">List de dictionarys, cada uno con las claves "campo","desde" y "hasta" con sus respectivos valores</param>
        /// <returns></returns>
        public abstract DataTable ObtenerEntre(string tabla, List<Dictionary<string, object>> datosBetween);

        public abstract DataTable Obtener(string tabla, List<ValorWhere> condiciones);

        public abstract Dictionary<string,object> ObtenerUltimo(string tabla,string campoUltimo, List<ValorWhere> valoresWhere);

        //public abstract Dictionary<string,object> ObtenerEnDictionary(string tabla, List<string> listaCampos, List<string> listaDatos);
        public abstract Dictionary<string, object> ObtenerEnDictionary(string tabla, List<ValorWhere> listaCondiciones);

        public abstract int Modificar(string tabla, List<ValorUpdate> valoresUpdate, List<ValorWhere> valoresWhere);

        public abstract void Insertar(ObjetoInsert Insert);

        public abstract void Insertar(List<ObjetoInsert> listaInserts);

        public abstract decimal Sum(string tabla, string campoASumar, List<ValorWhere> condiciones);

        public abstract void Eliminar(ObjetoDelete od);

        public abstract void EliminarVarios(List<ObjetoDelete> listaDeletes);

        //Devuelve el DataTable.FillSchema(dt,SchemaType.Source)
        public abstract DataTable ObtenerEsquema(string tabla);
    }
}
