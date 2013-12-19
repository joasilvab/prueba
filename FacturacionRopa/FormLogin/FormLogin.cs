using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Modelo;
using Utilidades;

namespace Formularios
{
    public partial class FormLogin : Form
    {
        Usuario _usuario;
        public Usuario Usuario { get { return _usuario; } }
        Conexion _conexion;
        public Conexion Conexion { get { return _conexion; } }

        public FormLogin()
        {
            InitializeComponent();
            //Usuario = new usuario();
        }

        private void botIngresar_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Properties.Settings set = Properties.Settings.Default;
            _conexion = new Conexion(set.server, set.db, set.user, set.pass);
            ModeloWhere mw = new ModeloWhere(UsuarioOperaciones.Id, tbUsuario.Text, true, Signos.Igual);
            try
            {
                UsuarioOperaciones usOp = new UsuarioOperaciones(_conexion);
                _usuario = usOp.Obtener(new List<ModeloWhere> { mw });
            }
            catch (ModeloOperacionesException msce)
            {
                MessageBox.Show("No se pudo conectar a la base de datos. Compruebe que se esté ejecutando MySQL","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = true;
                return;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                this.Enabled = true;
                return;
            }
            //verifica que el usuario exista
            if (Usuario != null)
            {
                if (tbContrasena.Text == Usuario.Contrasena)//obtener_usuario(sigla, textBox1.Text)[2])
                {
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    //error
                    MessageBox.Show("Error al ingresar el nombre de usuario y/o contraseña");
                    tbUsuario.Select();
                }
            }
            else
            {
                MessageBox.Show("Error al ingresar el nombre de usuario y/o contraseña");
                tbUsuario.Select();
            }
            this.Enabled = true;
        }

        private void tbProbarConex_Click(object sender, EventArgs e)
        {
            FormConexion fc = new FormConexion();
            fc.ShowDialog();
        }
    }
}
