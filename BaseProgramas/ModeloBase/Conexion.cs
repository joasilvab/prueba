using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibreriaBD;

namespace Modelo
{
    public class Conexion
    {
        public Conexion()
        {
        }

        public Conexion(string server, string bd, string user, string pass)
        {
            this.Server = server;
            this.BD = bd;
            this.User = user;
            this.Pass = pass;
        }
        public string Server { get; set; }
        public string BD { get; set; }
        public string User { get; set; }
        public string Pass { get; set; }

        public bool ProbarConexion()
        {
            LibreriaBD.ClaseLibreriaBDMySQL _libdb = new ClaseLibreriaBDMySQL(this.Server, this.BD, this.User, this.Pass);
            try
            {
                return _libdb.ProbarConexion();
            }
            catch (BaseDatosSinConexionException e)
            {
                throw new ModeloSinConexionException(e);
            }
        }
    }
}
