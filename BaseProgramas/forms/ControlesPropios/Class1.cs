using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace ControlesPropios
{
    public class ControlesPropiosException : Exception
    {
        public ControlesPropiosException(string mensaje,string nombrecontrol)
            :base(mensaje)
        {
            NombreControl = nombrecontrol;
        }

        public string NombreControl
        {
            get;
            set;
        }
    }

    public interface MiControlBase
    {
        bool Verificar();
        string NombrePropiedad { get; set; }
        string MensajeAyuda { get;}
    }

    public abstract class MiTextBoxBase : TextBox, MiControlBase
    {

        public string MensajeAyuda
        {
            get;
            protected set;
        }

        public string NombrePropiedad
        {
            get;
            set;
        }

        public abstract bool Verificar();
    }

    public abstract class MiComboBoxBase : ComboBox, MiControlBase
    {
        public string MensajeAyuda
        {
            get;
            protected set;
        }

        public string NombrePropiedad
        {
            get;
            set;
        }

        public abstract bool Verificar();

    }

    public class TextBoxInt32 : MiTextBoxBase
    {
        public TextBoxInt32()
        {
            this.MensajeAyuda = "Este campo debe contener sólo dígitos";
        }

        public override bool Verificar()
        {
            if (string.IsNullOrWhiteSpace(this.Text)) throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " no puede estar vacío",this.Name);
            foreach (char c in this.Text.Trim())
            {
                if (!char.IsDigit(c)) throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " no contiene un número válido",this.Name);
            }
            try
            {
                Convert.ToInt32(this.Text);
            }
            catch (OverflowException exc)
            {
                throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " debe contener un numero entero entre "+Int32.MinValue+" y " + System.Int32.MaxValue,this.Name);
            }
            return true;
        }
    }

    public class TextBoxString : MiTextBoxBase
    {
        public TextBoxString()
        {
            this.MensajeAyuda = "Este campo puede contener cualquier caracter";
        }

        public override bool Verificar()
        {
            if (string.IsNullOrWhiteSpace(this.Text))
                throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " no puede estar vacío",this.Name);
            else
                return true;
        }
    }

    public class ComboBoxInt32 : MiComboBoxBase
    {
        public override bool Verificar()
        {
            if (string.IsNullOrWhiteSpace(this.Text)) throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " no puede estar vacío", this.Name);
            foreach (char c in this.Text.Trim())
            {
                if (!char.IsDigit(c)) throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " no contiene un número válido", this.Name);
            }
            try
            {
                Convert.ToInt32(this.Text);
            }
            catch (OverflowException exc)
            {
                throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " debe contener un numero entero entre 0 y " + System.Int32.MaxValue, this.Name);
            }
            return true;
        }
    }
}
