using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Utilidades;

using Modelo;

namespace ControlesPropios
{
    public abstract class ComboBoxReferencial<T> : MiComboBoxBase, MiControlBase where T : ElementoBase
    {
        OperacionesBase<T> _modelo;
        List<string> _columnasAMostrar;
        //Label _labelActualizar;
        string _valueMember;
        string _columnaLabel;
        Conexion _con;

        public ComboBoxReferencial(Conexion con, OperacionesBase<T> modelo, List<string> columnasMostrar, string ValueMember, string columnaLabel, string nombrePropiedad)
        {
            _con = con;
            this.NombrePropiedad = nombrePropiedad;
            _modelo = modelo;
            _columnasAMostrar = columnasMostrar;
            //_labelActualizar = label;
            _valueMember = ValueMember;
            _columnaLabel = columnaLabel;
            this.DropDownWidth = 200;
            this.DropDown += ComboBoxReferencial_DropDown;
            this.DropDownClosed += ComboBoxReferencial_DropDownClosed;
            this.HelpRequested += ComboBoxReferencial_HelpRequested;
        }


        /*
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
            catch (OverflowException)
            {
                throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " debe contener un numero entero entre 0 y " + System.Int32.MaxValue, this.Name);
            }
            return true;
        }
         */

        public void ActualizarLabel(Label label)
        {
            //            MiComboBoxBase ctrl = (MiComboBoxBase)sender;
            //object ob = typeof(T).Assembly.GetType("Modelo." + ctrl.Name.TrimStart(_prefijoControl.ToCharArray()) + "Operaciones").GetConstructor(Type.EmptyTypes).Invoke(null);
            //object[] parametro = new object[1];
            //parametro[0] = new ColumnaValor[] { new ColumnaValor("Id", this.Text) };

            //Este try esta por si el campo en la base de datos acepta como maximo x caracteres y en el combobox podes poner cualquier cantidad
            try
            {
                if (this.Verificar())
                {
                    //object instancia = ob.GetType().GetMethod("Obtener").Invoke(ob, parametro);
                    _modelo.ObtenerColumnasIdsBase();
                    List<ModeloWhere> listaMwhere = new List<ModeloWhere>();
                    listaMwhere.Add(new ModeloWhere(_modelo.ObtenerColumnasIds()[0],this.Text,true, Signos.Igual));//Suponiendo que tiene solo un campo ID!!!!!!!
                    object instancia = _modelo.Obtener(listaMwhere);
                    if (instancia == null)
                    {
                        System.Media.SystemSounds.Beep.Play();
                        this.DroppedDown = true;
                        this.Select();
                    }
                    else
                        label.Text = instancia.GetType().GetProperty(_columnaLabel).GetValue(instancia, null).ToString();
                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                    this.DroppedDown = true;
                    this.Select();
                }
            }
            catch
            {
               //ver comentario antes del try
                System.Media.SystemSounds.Beep.Play();
                this.DroppedDown = true;
                this.Select();
            }
        }

        private void ComboBoxReferencial_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            //ComboBox cbox = (ComboBox)sender;
            this.DroppedDown = true;
        }

        private void ComboBoxReferencial_DropDown(object sender, EventArgs e)
        {
            //ComboBoxInt32 cbox = (ComboBoxInt32)sender;
            if (this.DataSource == null)
            {
                object ob = typeof(T).Assembly.GetType("Modelo." + this.NombrePropiedad + "Operaciones").GetConstructor(new Type[]{typeof(Conexion)}).Invoke(new object[]{_con});
                DataTable dt = (DataTable)ob.GetType().GetMethod("ObtenerTodos").Invoke(ob, null);
                dt.Columns.Add("Mostrar");
                string expresion = null;
                foreach (string s in _columnasAMostrar)
                    expresion += s + "+' '+";
                expresion = expresion.Remove(expresion.Length - 5);
                dt.Columns["Mostrar"].Expression = expresion;
                this.DataSource = dt;
                this.ValueMember = _valueMember;
                this.SelectedIndex = 0;
            }
            int selectedindex = this.SelectedIndex;
            this.DisplayMember = "Mostrar";
            this.SelectedIndex = selectedindex;
        }

        private void ComboBoxReferencial_DropDownClosed(object sender, EventArgs e)
        {
            //ComboBoxInt32 cbox = (ComboBoxInt32)sender;
            int itemindex = this.SelectedIndex;
            this.DisplayMember = "Id";
            this.Text = this.SelectedValue.ToString();
            this.SelectedIndex = itemindex;
            bool b = this.SelectNextControl(this, true, true, true, true);
        }

    }

    public class ComboBoxReferencialInt32<T> : ComboBoxReferencial<T> where T : ElementoBase
    {
        public ComboBoxReferencialInt32(Conexion con, OperacionesBase<T> modelo, List<string> columnasMostrar, string ValueMember, string columnaLabel, string nombrePropiedad)
            : base(con, modelo, columnasMostrar, ValueMember, columnaLabel, nombrePropiedad)
        {
        }

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
            catch (OverflowException)
            {
                throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " debe contener un numero entero entre 0 y " + System.Int32.MaxValue, this.Name);
            }
            return true;
        }
    }

    /// <summary>
    /// ComboBox que referencia una clase
    /// </summary>
    /// <typeparam name="T">ElementoBase</typeparam>
    public class ComboBoxReferencialDecimal<T> : ComboBoxReferencial<T> where T : ElementoBase
    {
        /// <summary>
        /// Crea un comboBox referencial
        /// </summary>
        /// <param name="modelo">Instancia de -nombre de la clase-Operaciones (que usa para traer los datos)</param>
        /// <param name="columnasMostrar">Columnas que se muestran cuando se pide la ayuda</param>
        /// <param name="ValueMember">"El valor que va a aparecer en el combobox cuando se abandone el mismo"</param>
        /// <param name="columnaLabel">"La columna cuyo valor se muestra en el label"</param>
        /// <param name="nombrePropiedad">"Nombre de la clase"</param>
        public ComboBoxReferencialDecimal(Conexion con, OperacionesBase<T> modelo, List<string> columnasMostrar, string ValueMember, string columnaLabel, string nombrePropiedad)//TODO calcular nombrepropiedad de T.gettype().name
            : base(con, modelo, columnasMostrar, ValueMember, columnaLabel, nombrePropiedad)
        {
        }

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
            catch (OverflowException)
            {
                throw new ControlesPropiosException("El campo " + this.NombrePropiedad + " debe contener un numero entero entre 0 y " + System.Int32.MaxValue, this.Name);
            }
            return true;
        }
    }
}
