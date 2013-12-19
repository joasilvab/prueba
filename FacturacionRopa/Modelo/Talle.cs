using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class Talle:ElementoBase
    {
        public int Id { get; set; }
        public string Sigla { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Talle)
            {
                Talle otro = obj as Talle;
                return (this.Id == otro.Id && this.Sigla == otro.Sigla);
            }
            else
                return false;
            //return base.Equals(obj);
        }
    }
}
