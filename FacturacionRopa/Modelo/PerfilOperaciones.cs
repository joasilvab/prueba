using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class PerfilOperaciones : OperacionesBase<Perfil>
    {
        public PerfilOperaciones(Conexion con)
            : base(con,"perfiles")
        {
            this.AgregarCampoColumna("per_id", Id);
            this.AgregarCampoColumna("per_nombre", Descripcion);
        }

        public List<string> ObtenerListaPerfiles()
        {
            List<string> devolver = new List<string>();
            foreach (Dictionary<string, object> d in _libbd.Select(true, new List<string> { ObtenerCampo("Descripcion") }, new List<string> { _nombretabla }, null))
            {
                devolver.Add(d[ObtenerCampo("Descripcion")].ToString());
            }
            return devolver;
        }

        public static string Id = "Id";
        public static string Descripcion = "Descripcion";
    }
}
