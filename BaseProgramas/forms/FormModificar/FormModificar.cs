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

using System.Net;
using System.Net.NetworkInformation;

namespace Formularios
{
    public partial class FormModificar<T> : FormAMBase<T> where T : ElementoBase
    {
        string nombreModulo;
        T _elemento;

        public FormModificar(Conexion con,OperacionesBase<T> modelo, T objeto)
            :base(con, modelo,true)
        {
            this.Load += FormModificar_Load;
            botAceptar.Click += botAceptar_Click;
            _elemento = objeto;
            InitializeComponent();
        }

        private void FormModificar_Load(object sender, EventArgs e)
        {
            this.Text = "Modificar " + typeof(T).Name;
            bool selected = false;
            foreach (PropertyInfo pi in _modelo.Propiedades)
            {
                if (pi.PropertyType.BaseType.Name == "ElementoBase")
                {
                    object ob = pi.GetValue(_elemento,null);
                    tableLayoutPanel2.Controls[_prefijoControl + pi.Name].Text = ob.GetType().GetProperty("Id").GetValue(ob,null).ToString();
                    tableLayoutPanel2.Controls[_prefijoControl + pi.Name + _sufijoLabel].Text = ob.GetType().GetProperty("Descripcion").GetValue(ob,null).ToString();
                }
                else
                {
                    tableLayoutPanel2.Controls[_prefijoControl + pi.Name].Text = pi.GetValue(_elemento,null).ToString();
                }
                if (!_modelo.PropiedadesNoAutoincrementales.Contains(pi))
                    tableLayoutPanel2.Controls[_prefijoControl + pi.Name].Enabled = false;
                else
                    if (!selected)
                    {
                        selected = true;
                        tableLayoutPanel2.Controls[_prefijoControl + pi.Name].Select();
                    }
            }
        }

        private void botAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                T elementonuevo = ObtenerDeControles();
                _modelo.Modificar(_elemento, elementonuevo);
                MessageBox.Show("Modificado con éxito");

                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            catch (ControlesPropiosException exc)
            {
                MessageBox.Show(exc.Message);
                tableLayoutPanel2.Controls[exc.NombreControl].Select();
            }
            catch (ModeloOperacionesException exc)
            {
                throw exc;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.Message);
                MessageBox.Show(exc.Message + "\n\n" + exc.Source + "\n\n" + exc.StackTrace);
            }
            finally
            {
                this.Enabled = true;
            }
        }
    }
}
