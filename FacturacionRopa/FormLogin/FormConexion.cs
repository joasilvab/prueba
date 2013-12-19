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
    public partial class FormConexion : Form
    {
        public FormConexion()
        {
            InitializeComponent();
        }

        private void FormConexion_Load(object sender, EventArgs e)
        {
            tbServer.Text = Properties.Settings.Default.server;
            tbUser.Text = Properties.Settings.Default.user;
            tbDatabase.Text = Properties.Settings.Default.db;
            tbPass.Text = Properties.Settings.Default.pass;
        }

        private void botAceptar_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.server = tbServer.Text;
            Properties.Settings.Default.user = tbUser.Text;
            Properties.Settings.Default.db = tbDatabase.Text;
            Properties.Settings.Default.pass = tbPass.Text;
            Properties.Settings.Default.Save();
            this.Close();
        }

        private void tbProbar_Click(object sender, EventArgs e)
        {
            if (tbServer.Text == "" || tbDatabase.Text == "")
            {
                MessageBox.Show("Por favor, complete los campos Server y Database");
                return;
            }
            Conexion con = new Conexion(tbServer.Text, tbDatabase.Text, tbUser.Text,tbPass.Text);
            try
            {
                this.Enabled = false;
                if (con.ProbarConexion())
                    MessageBox.Show("La conexión ha sido exitosa");
            }
            catch (ModeloSinConexionException exc)
            {
                MessageBox.Show("No se ha podido establecer la conexión.\n\n" + exc.InnerException.Message);
            }
            finally
            {
                this.Enabled = true;
            }
        }
    }
}
