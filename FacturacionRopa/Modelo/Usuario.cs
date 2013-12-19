using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    //Siempre tiene que tener una propiedad que se llame "Id". Ver metodo Agregar en OperacionesBase
    //Si se relaciona con otra clase, tiene que haber un metodo que se llame "Set"+nombreClase, con parametros con tipo igual al id. Ver Obtener
    //Toda clase tiene que tener otra que se llame <NombredeClase>Operaciones que sea de tipo OperacionesBase

    public class Usuario : ElementoBase
    {
        public Usuario()
        {
        }

        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }

        public int PerfilId { get; set; }

        public Perfil Perfil(Conexion con)
        {
            PerfilOperaciones op = new PerfilOperaciones(con);
            List<ModeloWhere> mw = new List<ModeloWhere>();
            mw.Add(new ModeloWhere(PerfilOperaciones.Id,PerfilId.ToString(),true, Utilidades.Signos.Igual));
            return op.Obtener(mw);
        }
    }
}
