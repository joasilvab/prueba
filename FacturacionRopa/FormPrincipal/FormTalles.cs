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
    public partial class FormTalles : Form
    {
        Conexion _conexion;
        TalleOperaciones _top;
        void InicializarTallesOperaciones()
        {
            if (_top == null)
                _top = new TalleOperaciones(_conexion);
        }
       
        public FormTalles(Conexion conexion, bool HabilitarModificar)
        {
            _conexion = conexion;
            InitializeComponent();
            if (!HabilitarModificar)
                botModificar.Enabled = botBorrar.Enabled = false;
        }

        private void botAgregar_Click(object sender, EventArgs e)
        {
            InicializarTallesOperaciones();
            //TalleOperaciones top = new TalleOperaciones(_conexion);
            FormAlta<Talle> fat = new FormAlta<Talle>(_conexion, _top);
            fat.ButtonClicked += ButtonOkClicked;
            try
            {
                if (fat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dataGridView1.DataSource = _top.ObtenerTodos();
                }
            }
            catch (ModeloEntradaDuplicadaException exc)
            {
                MessageBox.Show("¡No se agregó el talle '" + exc.ValorDuplicado + "' porque ya existe!");
            }
        }

        private void ButtonOkClicked(object sender, EventArgs e)
        {
            ActualizarDatagrid();
        }

        private void FormTalles_Load(object sender, EventArgs e)
        {
            ActualizarDatagrid();
        }

        private void ActualizarDatagrid()
        {
            InicializarTallesOperaciones();
            dataGridView1.DataSource = _top.ObtenerTodos(TalleOperaciones.Sigla);
        }

        private void botModificar_Click(object sender, EventArgs e)
        {
            InicializarTallesOperaciones();
            string desc = "";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                desc = dataGridView1.SelectedRows[0].Cells[TalleOperaciones.Sigla].Value.ToString();
                Talle talle = _top.Obtener(new ModeloWhere(TalleOperaciones.Sigla, desc, true, Signos.Igual));
                FormModificar<Talle> fmt = new FormModificar<Talle>(_conexion, _top, talle);
                try
                {
                    if (fmt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ActualizarDatagrid();
                    }
                }
                catch (ModeloEntradaDuplicadaException exc)
                {
                    MessageBox.Show("¡No se modificó el talle porque ya existe otro igual!");
                }
            }
        }

        private void botBorrar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string talle = dataGridView1.SelectedRows[0].Cells[TalleOperaciones.Sigla].Value.ToString();
                if (MessageBox.Show("¿Está seguro que desea borrar el talle '" + talle + "'?", "Advertencia", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _top.Eliminar(new ModeloWhere(TalleOperaciones.Sigla, talle, true, Signos.Igual));
                    ActualizarDatagrid();
                }
            }
        }
    }
}
