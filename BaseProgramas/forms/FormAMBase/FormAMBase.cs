using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using Modelo;
using ControlesPropios;

namespace Formularios
{
    abstract partial class FormAMBase<T> : Form where T: ElementoBase
    {
        protected OperacionesBase<T> _modelo;
        protected const string _prefijoControl = "control";
        protected const string _sufijoLabel = "label";
        bool _incluirautoincrementales;
        protected Conexion _con;

        public FormAMBase(Conexion con,OperacionesBase<T> modelo, bool IncluirAutoincrementales)
        {
            _con = con;
            _incluirautoincrementales = IncluirAutoincrementales;
            _modelo = modelo;
            InitializeComponent();
        }

        public event EventHandler ButtonClicked;

        public void NotifyButtonClicked(EventArgs e)
        {
           
            if (ButtonClicked != null)
            {
                ButtonClicked(this, e);
            }
        }

        protected void LimpiarFormulario()
        {
            tableLayoutPanel2.Controls[1].Select();
            foreach (Control c in tableLayoutPanel2.Controls)
            {
                if (c is ControlesPropios.MiControlBase || (c is Label && c.Name.Contains(_prefijoControl)))
                {
                    c.Text = string.Empty;
                }
                if (c is MiComboBoxBase)
                {
                    ((MiComboBoxBase)c).SelectedText = string.Empty;
                }
            }
        }

        protected bool VerificarTodos()
        {
            foreach (Control c in tableLayoutPanel2.Controls)
            {
                if (c is ControlesPropios.MiControlBase)
                {
                    ((MiControlBase)c).Verificar(); 
                }
            }
            return true;
        }
 
        private void tb_Leave(object sender, EventArgs e)
        {
            if (botCerrar.Focused == false)
            {
                MiTextBoxBase tbb = (MiTextBoxBase)sender;
                try
                {
                    tbb.Verificar();
                }
                catch (Exception exc)
                {
                    System.Media.SystemSounds.Beep.Play();
                    MessageBox.Show(exc.Message);
                    tbb.Select();
                }
            }
        }

        private void cbox_Leave(object sender, EventArgs e)
        {
            if (botCerrar.Focused == false)
            {
                try
                {
                    sender.GetType().GetMethod("ActualizarLabel").Invoke(sender, new object[] { tableLayoutPanel2.Controls[_prefijoControl + ((MiComboBoxBase)sender).NombrePropiedad + _sufijoLabel] });
                }
                /*
                MiComboBoxBase ctrl = (MiComboBoxBase)sender;
                object ob = typeof(T).Assembly.GetType("Modelo." + ctrl.Name.TrimStart(_prefijoControl.ToCharArray()) + "Operaciones").GetConstructor(Type.EmptyTypes).Invoke(null);
                object[] parametro = new object[1];
                parametro[0] = new ColumnaValor[] { new ColumnaValor("Id", ctrl.Text) };
                try
                {
                    if (ctrl.Verificar())
                    {
                        object instancia = ob.GetType().GetMethod("Obtener").Invoke(ob, parametro);
                        if (instancia == null)
                        {
                            System.Media.SystemSounds.Beep.Play();
                            ctrl.DroppedDown = true;
                            ctrl.Select();
                        }
                        else
                            tableLayoutPanel2.Controls[_prefijoControl + ctrl.NombrePropiedad + _sufijoLabel].Text = instancia.GetType().GetProperty("Descripcion").GetValue(instancia,null).ToString();
                    }
                    else
                    {
                        System.Media.SystemSounds.Beep.Play();
                        ctrl.DroppedDown = true;
                        ctrl.Select();
                    }
                }*/
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /*
        private void cbox_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            ComboBox cbox = (ComboBox)sender;
            cbox.DroppedDown = true;
        }
        */
        private void tb_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            MessageBox.Show(((MiTextBoxBase)sender).MensajeAyuda);
        }

        private void FormPersonaAgregar_Load(object sender, EventArgs e)
        {
            tableLayoutPanel2.RowCount = 0;
            this.Text = "Agregar " + typeof(T).Name;
            List<PropertyInfo> ConjuntoPropiedades;
            ConjuntoPropiedades = _incluirautoincrementales ? _modelo.Propiedades : _modelo.PropiedadesNoAutoincrementales;//Para formmodificar o formagregar
            foreach (PropertyInfo pi in ConjuntoPropiedades)
            {
                tableLayoutPanel2.RowCount += 1;
                Label label = new Label();
                label.AutoSize = true;
                //label.Text = pi.Name + ":";
                label.Text = pi.Name.Length > 2 && pi.Name.Substring(pi.Name.Length - 2) == "Id" ? pi.Name.Remove(pi.Name.Length - 2) + ":" : pi.Name + ":";
                tableLayoutPanel2.Controls.Add(label, 0, tableLayoutPanel2.RowCount - 1);
                label.Padding = new Padding(0, 6, 0, 0);
                //if (pi.PropertyType.BaseType.Name == "ElementoBase")
                if (pi.Name.Length > 2 && pi.Name.Substring(pi.Name.Length-2) == "Id") //El length > 2 sirve por si hay una propiedad que se llama Id
                {
                    //var model = Assembly.Load("Modelo").GetType("Modelo."+ pi.PropertyType.Name+"Operaciones").GetConstructor(Type.EmptyTypes).Invoke(null);
                    string nombreClase = pi.Name.Remove(pi.Name.Length - 2);

                    //PARCHAZOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
                    object model;
                    Conexion conexionparche = new Conexion();
                    bool parche = false;
                    //if (typeof(T).Name != "Persona")
                    //{
                    //    model = Assembly.Load("Modelo").GetType("Modelo." + nombreClase + "Operaciones").GetConstructor(new Type[] { typeof(Conexion) }).Invoke(new object[] { _con });
                    //}
                    //else
                    //{
                        
                    //    conexionparche.Server = "localhost";
                    //    conexionparche.BD = "comunes";
                    //    model = Assembly.Load("Modelo").GetType("Modelo." + nombreClase + "Operaciones").GetConstructor(new Type[] { typeof(Conexion) }).Invoke(new object[] { conexionparche });
                    //}
                    try
                    {
                        model = Assembly.Load("Modelo").GetType("Modelo." + nombreClase + "Operaciones").GetConstructor(new Type[] { typeof(Conexion) }).Invoke(new object[] { _con });
                    }
                    catch
                    {
                        parche = true;
                        conexionparche.Server = "localhost";
                        conexionparche.BD = "comunes";
                        model = Assembly.Load("Modelo").GetType("Modelo." + nombreClase + "Operaciones").GetConstructor(new Type[] { typeof(Conexion) }).Invoke(new object[] { conexionparche });
                    }
                    //Se crea un FormAlta<nombreModelo>
                    Type tipoClase = Assembly.Load("Modelo").GetType("Modelo." + nombreClase);
                    var genericListType = typeof(ComboBoxReferencialInt32<>);
                    var specificListType = genericListType.MakeGenericType(tipoClase);
                    object[] parametros;
                    //if (typeof(T).Name != "Persona")
                    //{
                    //    parametros = new object[]{ _con, model, new List<string> { "Id", "Descripcion" }, "Id", "Descripcion", nombreClase };
                    //}
                    //else
                    //{
                    //    parametros = new object[] { conexionparche, model, new List<string> { "Id", "Descripcion" }, "Id", "Descripcion", nombreClase };
                    //}
                    if (!parche)
                    {
                        parametros = new object[] { _con, model, new List<string> { "Id", "Descripcion" }, "Id", "Descripcion", nombreClase };
                    }
                    else
                    {
                        parametros = new object[] { conexionparche, model, new List<string> { "Id", "Descripcion" }, "Id", "Descripcion", nombreClase };
                    }
                    var instancia = Activator.CreateInstance(specificListType, parametros);
                    //Se le asigna ButtonClicked para que cuando agregue se actualice el datagrid pero no se cierre el formalta
                    //var ev = instancia.GetType().GetEvent("Leave");
                    //MethodInfo mi = this.GetType().GetMethod("cbox_Leave", BindingFlags.NonPublic | BindingFlags.Instance);
                   // System.Delegate d = Delegate.CreateDelegate(ev.EventHandlerType, this, mi);
                    //instancia.GetType().GetEvent("Leave").AddEventHandler(instancia, d);
                    MiComboBoxBase cb = (MiComboBoxBase)instancia;

                    //ComboBoxInt32 cb = new ComboBoxInt32();
                    cb.Name = _prefijoControl + pi.Name;

                    //cb.Name = _prefijoControl + nombreClase;
                    cb.TabIndex = tableLayoutPanel2.RowCount;
                    //cb.DropDownWidth = 200;
                    //cb.DropDown += comboBox1_DropDown_1;
                    //cb.DropDownClosed += comboBox1_DropDownClosed;
                    //cb.HelpRequested += cbox_HelpRequested;
                    cb.Leave += cbox_Leave;
                    //cb.NombrePropiedad = pi.Name;
                    tableLayoutPanel2.Controls.Add(cb, 1, tableLayoutPanel2.RowCount - 1);
                    tableLayoutPanel2.RowCount += 1;
                    Label labDesc = new Label();
                    labDesc.AutoSize = true;
                    //labDesc.Name = _prefijoControl + pi.Name + _sufijoLabel;
                    labDesc.Name = _prefijoControl + nombreClase + _sufijoLabel;
                    tableLayoutPanel2.Controls.Add(labDesc, 1, tableLayoutPanel2.RowCount - 1);
                }
                else
                {
                    Type tipo = typeof(MiTextBoxBase);
                    MiTextBoxBase tb = (MiTextBoxBase)tipo.Assembly.GetType("ControlesPropios.TextBox" + pi.PropertyType.Name).GetConstructor(Type.EmptyTypes).Invoke(null);
                    tb.TabIndex = tableLayoutPanel2.RowCount;
                    tb.NombrePropiedad = pi.Name;
                    tb.Leave += tb_Leave;
                    tb.HelpRequested += tb_HelpRequested;
                    tb.Width = 121;
                    tb.Name = _prefijoControl + pi.Name;
                    int longitud = _modelo.Campos()[pi.Name].MaxLength;
                    if (longitud != -1)
                        tb.MaxLength = longitud;
                    tableLayoutPanel2.Controls.Add(tb, 1, tableLayoutPanel2.RowCount - 1);
                }
            }
            botAceptar.TabIndex = tableLayoutPanel2.RowCount + 1;
            botCerrar.TabIndex = tableLayoutPanel2.RowCount + 2;
            tableLayoutPanel2.Controls[1].Select();
        }

        protected T ObtenerDeControles()
        {
            T elemento = (T)typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);
            foreach (PropertyInfo pi in _modelo.Propiedades)
            {
                Control c = tableLayoutPanel2.Controls[_prefijoControl + pi.Name];
                if (c != null)
                    if (c is MiTextBoxBase)
                        pi.SetValue(elemento, Convert.ChangeType(c.Text, pi.PropertyType),null);
                    else
                        if (c is MiComboBoxBase)
                        {
                            pi.SetValue(elemento, Convert.ChangeType(c.Text, pi.PropertyType), null);
                            //object[] parametros = new object[1];
                            //ParameterInfo parinfo = elemento.GetType().GetMethod("Set" + ((MiComboBoxBase)c).NombrePropiedad).GetParameters()[0];
                            //parametros[0] = Convert.ChangeType(c.Text, parinfo.ParameterType);
                            //elemento.GetType().GetMethod("Set" + ((MiComboBoxBase)c).NombrePropiedad).Invoke(elemento, parametros);
                        }
            }
            return elemento;
        }
        /*
        private void comboBox1_DropDown_1(object sender, EventArgs e)
        {
            ComboBoxInt32 cbox = (ComboBoxInt32)sender;
            if (cbox.DataSource == null)
            {
                object ob = typeof(T).Assembly.GetType("Modelo."+cbox.Name.TrimStart(_prefijoControl.ToCharArray())+"Operaciones").GetConstructor(Type.EmptyTypes).Invoke(null);
                DataTable dt = (DataTable)ob.GetType().GetMethod("ObtenerTodos").Invoke(ob, null);
                dt.Columns.Add("Mostrar");
                dt.Columns["Mostrar"].Expression = "Id+' '+Descripcion";
                cbox.DataSource = dt;
                cbox.ValueMember = "Id";
                cbox.SelectedIndex = 0;
            }
            int selectedindex = cbox.SelectedIndex;
            cbox.DisplayMember = "Mostrar";
            cbox.SelectedIndex = selectedindex;
            
        }*/
        /*
        private void comboBox1_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxInt32 cbox = (ComboBoxInt32)sender;
            int itemindex = cbox.SelectedIndex;
            cbox.DisplayMember = "Id";
            cbox.Text =cbox.SelectedValue.ToString();
            cbox.SelectedIndex = itemindex;
            bool b = this.SelectNextControl(cbox, true, true, true, true);
        }*/
    }
}
