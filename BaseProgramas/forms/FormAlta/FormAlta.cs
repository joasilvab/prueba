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
using Formularios;
using Modelo;
using ControlesPropios;

using System.Net;
using System.Net.NetworkInformation;

namespace Formularios
{
    public partial class FormAlta<T> : FormAMBase<T> where T: Modelo.ElementoBase
    {
        public FormAlta(Conexion con,OperacionesBase<T> modelo)
            : base(con,modelo, false)
        {
            _modelo = modelo;
            InitializeComponent();
            botAceptar.Click += botAgregar_Click;
        }

        //bool _ABMComun;
        //Conexion _concomun;
        //public FormAlta(Conexion con,Conexion concomun, bool abmcomun, OperacionesBase<T> modelo)
        //    : base(con, modelo, false)
        //{
        //    _concomun = concomun;
        //    _ABMComun = abmcomun;
        //    _modelo = modelo;
        //    InitializeComponent();
        //    botAceptar.Click += botAgregar_Click;
        //}

        private void botAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                if (VerificarTodos())
                {
                    this.Enabled = false;
                    T objeto = ObtenerDeControles();
                    _modelo.Agregar(objeto);
                    MessageBox.Show("Ingresado con éxito");
                    NotifyButtonClicked(e);
                    LimpiarFormulario();
                }
            }
            catch (ControlesPropiosException exc)
            {
                MessageBox.Show(exc.Message);
                tableLayoutPanel2.Controls[exc.NombreControl].Select();
            }
            catch (ModeloOperacionesException o)
            {
                throw o;
            }
            //catch (Exception exc)
            //{
            //    //MessageBox.Show(exc.Message);
            //    MessageBox.Show(exc.Message + "\n\n" + exc.Source + "\n\n" + exc.StackTrace);
            //}
            finally
            {
                this.Enabled = true;
            }
        }
    }
}
