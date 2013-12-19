using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlesPropios
{
    public class TextBoxDecimal: MiTextBoxBase
    {
        public override bool Verificar()
        {
            try
            {
                Decimal.Parse(this.Text);
                return true;
            }
            catch
            {
                throw new ControlesPropiosException(string.Format("El campo {0} contiene datos incorrectos!",this.NombrePropiedad), this.Name);
            }
        }
    }
}
