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
    public partial class FormGenerico<T>  : Form where T: ElementoBase
    {
        OperacionesBase<T> _op;
        Conexion _conexion;
        string _propiedadDescripcion;
        string _propiedadId;
        bool _masculino;
        string _claseSingular;

       
        public FormGenerico(bool HabilitarModificar, OperacionesBase<T> op, Conexion conexion, string PropiedadDescripcion, string PropiedadId, string clasePlural,string claseSingular, bool masculino)
        {
            _claseSingular = claseSingular;
            _masculino = masculino;
            _propiedadId = PropiedadId;
            _propiedadDescripcion = PropiedadDescripcion;
            _conexion = conexion;
            _op = op;
            InitializeComponent();
            this.Text = clasePlural;
            if (!HabilitarModificar)
                botModificar.Enabled = botBorrar.Enabled = false;
        }

        private void botAgregar_Click(object sender, EventArgs e)
        {
            FormAlta<T> fat = new FormAlta<T>(_conexion, _op);
            fat.ButtonClicked += ButtonOkClicked;
            try
            {
                if (fat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    dataGridView1.DataSource = _op.ObtenerTodos();
                }
            }
            catch (ModeloEntradaDuplicadaException exc)
            {
               MessageBox.Show("¡No se agregó "+(_masculino?"el":"la")+" "+_claseSingular+" '" + exc.ValorDuplicado + "' porque ya existe!");
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
            dataGridView1.DataSource = _op.ObtenerTodos();
        }

        private void botModificar_Click(object sender, EventArgs e)
        {
            string id = "";
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    id = dataGridView1.SelectedRows[0].Cells[_propiedadId].Value.ToString();
                    T obj = _op.Obtener(new ModeloWhere(_propiedadId, id, true, Signos.Igual));
                    FormModificar<T> fmt = new FormModificar<T>(_conexion, _op, obj);
                    if (fmt.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        ActualizarDatagrid();
                    }
                }
                catch (ModeloEntradaDuplicadaException)
                {
                    MessageBox.Show("¡No se modificó el " +_claseSingular + " porque ya existe otro igual!");
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
                }
            }
        }

        private void botBorrar_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string obj = dataGridView1.SelectedRows[0].Cells[_propiedadDescripcion].Value.ToString();
                object id = dataGridView1.SelectedRows[0].Cells[_propiedadId].Value.ToString();
                if (MessageBox.Show("¿Está seguro que desea borrar "+(_masculino?"el":"la")+" "+_claseSingular+" '" + obj + "'?", "Advertencia", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                {
                    _op.Eliminar(new ModeloWhere(_propiedadId, id.ToString(), true, Signos.Igual));
                    ActualizarDatagrid();
                }
            }
        }
    }
}
