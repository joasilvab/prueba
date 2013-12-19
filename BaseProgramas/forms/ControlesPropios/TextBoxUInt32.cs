using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlesPropios
{
    public class TextBoxUInt32: MiTextBoxBase
    {
        public TextBoxUInt32()
        {
            this.MensajeAyuda = "Este campo debe contener solo dígitos y debe ser un número mayor que cero.";
        }
        public override bool Verificar()
        {
            try
            {
                uint.Parse(this.Text);
                return true;
            }
            catch (FormatException)
            {
                throw new ControlesPropiosException("El campo '"+this.NombrePropiedad+"' debe ser un número entero mayor que cero.", this.Name);
            }
            catch (OverflowException)
            {
                throw new ControlesPropiosException("El campo '" + this.NombrePropiedad + "' debe estar entre " + UInt32.MinValue + " y " + UInt32.MaxValue + ".", this.Name);
            }
        }
    }
}
