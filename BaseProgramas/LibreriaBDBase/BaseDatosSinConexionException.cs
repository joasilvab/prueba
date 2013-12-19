using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaBD
{
    public class BaseDatosSinConexionException: BaseDatosException
    {
        public BaseDatosSinConexionException()
        {
        }

        public BaseDatosSinConexionException(string Message)
            :base(Message)
        {
        }

        public BaseDatosSinConexionException(string Message, Exception innerException)
            :base(Message,innerException)
        {
        }
    }
}
