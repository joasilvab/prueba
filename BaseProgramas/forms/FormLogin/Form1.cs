using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Modelo;

namespace Formularios
{
    public partial class Form1 : Form
    {
        public LibreriaBD.ClaseLibreriaBD ClaseB;
        public UsuarioOperaciones ki;
        public Usuario Usuario;
        public sucursales PtoVta;
        Conexion _con;

        public Form1(Conexion con)
        {
            _con = con;
            ki = new UsuarioOperaciones(con);
            InitializeComponent();
            //Usuario = new usuario();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Usuario = ki.Obtener(new ColumnaValor("Id", textBox1.Text));
            //verifica que el usuario exista
            if (Usuario != null)
            {
                if (textBox2.Text == Usuario.Contrasena)//obtener_usuario(sigla, textBox1.Text)[2])
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    //error
                    MessageBox.Show("Error al ingresar el nombre de usuario y/o contraseña");
                    textBox1.Select();
                }
            }
            else
            {
                MessageBox.Show("Error al ingresar el nombre de usuario y/o contraseña");
                textBox1.Select();
            }
        }

        sucursalesOperaciones sop;
        private void Form1_Load(object sender, EventArgs e)
        {
            if (sop == null)
            {
                sop = new sucursalesOperaciones(_con);
            }
            sucursales suc = sop.ObtenerTodosLista()[0];
            tbPtoVta.Text = suc.id.ToString();
            tbPtoVtaDesc.Text = suc.desc;
            PtoVta = suc;
        }

        private void botVolver_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Ignore;
        }

        formAyuda CrearAyudaSucursales()
        {
            if (sop == null)
                sop = new sucursalesOperaciones(_con);
            List<object> lista = new List<object>();
            foreach (sucursales s in sop.ObtenerTodosLista())
            {
                lista.Add(s);
            }
            return new formAyuda(_con, new List<string>() { sucursalesOperaciones.id, sucursalesOperaciones.desc }, lista);
        }

        private void tbPtoVta_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            formAyuda fa = CrearAyudaSucursales();
            if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tbPtoVta.Text = ((sucursales)fa.ElementoDevuelto).id.ToString();
            }
        }

        private void tbPtoVta_Leave(object sender, EventArgs e)
        {
            if (sop == null)
                sop = new sucursalesOperaciones(_con);
            sucursales suc = null;
            try
            {
                suc = sop.Obtener(new ColumnaValor(sucursalesOperaciones.id, tbPtoVta.Text));
            }
            catch
            {

            }
            finally
            {
                if (suc == null)
                {
                    formAyuda fa = CrearAyudaSucursales();
                    if (fa.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        suc = (sucursales)fa.ElementoDevuelto;
                        tbPtoVta.Text = suc.id.ToString();
                        tbPtoVtaDesc.Text = suc.desc;
                    }
                }
                else
                {
                    tbPtoVtaDesc.Text = suc.desc;
                }
            }
            PtoVta = suc;
        }
    }
}
