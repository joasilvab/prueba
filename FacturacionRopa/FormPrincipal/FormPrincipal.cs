using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using Modelo;

using System.Diagnostics;

namespace Formularios
{
    public partial class FormPrincipal : Form
    {
        public Usuario _us { get; set; }
        Conexion _con;
        public bool CerrarSesion { get; set; }

        public FormPrincipal(Conexion con, Usuario us)
        {
            _us = us;
            _con = con;
            InitializeComponent();
            tsslabel.Text = "";
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            this.Text = Text + " -- Usuario: " + _us.Nombre;
        }

        private void StatusTextChanged(object sender, StatusLabelArgs e)
        {
            tsslabel.Text = e.Texto;
            tsslabel.ForeColor = e.Color;
           //agregar hora
        }

        private void administraciónProductosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContenidoProductos prod = new ContenidoProductos(_con);
            prod.StatusLabelChanged += StatusTextChanged;
            prod.Size = new System.Drawing.Size(flowContenido.Width, flowContenido.Height);
            flowContenido.Controls.Clear();
            flowContenido.Controls.Add(prod);
            prod.Focus();
        }

        private void tallesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool editarHabilitado = true;
            if (flowContenido.Controls.Count != 0)
            {
                if (flowContenido.Controls[0] is ContenidoProductos)
                    editarHabilitado = false;
            }
            
            //FormTalles ft = new FormTalles(_con,editarHabilitado);
            TalleOperaciones top = new TalleOperaciones(_con);
            FormGenerico<Talle> ft = new FormGenerico<Talle>(editarHabilitado, top, _con, TalleOperaciones.Sigla, TalleOperaciones.Id, "Talles", "talle", true);
            ft.ShowDialog();
        }

        private void cerrarSesiónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CerrarSesion = true;
            this.Close();
        }

        private void insumosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsumoOperaciones iop = new InsumoOperaciones(_con);
            FormGenerico<Insumo> fgi = new FormGenerico<Insumo>(true, iop,_con,InsumoOperaciones.Descripcion,InsumoOperaciones.Id, "Insumos","insumo", true);
            fgi.ShowDialog();
        }
    }
}
