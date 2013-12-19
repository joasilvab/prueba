using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class Perfil: ElementoBase
    {
        public Perfil()
        {
        }

        public decimal Id { get; set; }
        public string Descripcion { get; set; }

        //Para el crear usuario
        public override string ToString()
        {
            return Id.ToString();
        }
    }
}
