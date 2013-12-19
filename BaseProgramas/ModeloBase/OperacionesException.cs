using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriaBD;

namespace Modelo
{
    public class ModeloOperacionesException : Exception
    {
        public int Numero;

        public ModeloOperacionesException()
        {
        }

        public ModeloOperacionesException(string Message)
            : base(Message)
        {
        }

        public ModeloOperacionesException(string Message, Exception innerException)
            : base(Message,innerException)
        {
        }
    }
    public class ModeloSinCamposPropiedadesException : Exception
    {
        public ModeloSinCamposPropiedadesException(string nombreClase, string nombreOperaciones)
            : base("No se ha inicializado ninguna relación entre las propiedades de la clase " + nombreClase + " y los campos de la tabla en la BD, en el constructor de la clase " + nombreOperaciones)
        {
        }
    }

    public class ModeloCampoSinPropiedadException : Exception
    {
        public ModeloCampoSinPropiedadException(string nombreCampo,string nombreClase, string nombreOperaciones)
            : base("No se definió la relación entre el campo " + nombreCampo + " y alguna propiedad de la clase " + nombreClase +" en el constructor de la clase "+nombreOperaciones)
        {
        }
    }

    public class ModeloPropiedadNoEncontradaException : Exception
    {
        public string NombrePropiedad;
        public string NombreClase;
        public ModeloPropiedadNoEncontradaException(string propiedad, string clase)
        {
            NombreClase = clase;
            NombrePropiedad = propiedad;
        }
    }

    public class ModeloCampoNoEncontradoException : Exception
    {
        public string NombrePropiedad;
        public string NombreClase;
        public ModeloCampoNoEncontradoException(string propiedad, string clase)
        {
            NombreClase = clase;
            NombrePropiedad = propiedad;
        }
    }

    public class ModeloSinConexionException : ModeloOperacionesException
    {
        public ModeloSinConexionException(BaseDatosSinConexionException inner)
            :base("No se ha podido establecer la conexión con la base de datos",inner)
        {
        }
    }

    public class ModeloEntradaDuplicadaException : ModeloOperacionesException
    {
        public string ValorDuplicado { get; set; }
        public string PropiedadDuplicado { get; set; }
        public ModeloEntradaDuplicadaException(string valor, string propiedad)
        {
            ValorDuplicado = valor;
            PropiedadDuplicado = propiedad; //Siempre devuelver null porque tira el nombre de la "clave" no del campo o sea el nombre de la definición (en phpmyadmin ver 'indices' en definicion de tabla)
        }
    }
}
