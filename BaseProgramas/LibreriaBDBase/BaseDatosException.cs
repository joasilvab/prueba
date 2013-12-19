using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibreriaBD
{
    public class BaseDatosException:Exception
    {
        public BaseDatosException()
        {
        }
        public BaseDatosException(string message)
            :base(message)
        {
        }
        public BaseDatosException(string message, Exception innerException)
            : base(message,innerException)
        {
        }
    }
}
