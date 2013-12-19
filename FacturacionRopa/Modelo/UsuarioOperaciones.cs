using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class UsuarioOperaciones : OperacionesBase<Usuario>
    {
        public UsuarioOperaciones(Conexion con)
            : base(con,"usuarios")
        {
            this.AgregarCampoColumna("usu_id", Id);
            this.AgregarCampoColumna("usu_nombre", Nombre);
            this.AgregarCampoColumna("usu_contrasena", Contraseña);
            this.AgregarCampoColumna("usu_per_id", PerfilId);
        }

        public static string Id = "Id";
        public static string Nombre = "Nombre";
        public static string Contraseña = "Contrasena";
        public static string PerfilId = "PerfilId";

    }
}