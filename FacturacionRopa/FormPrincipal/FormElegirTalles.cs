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
    public partial class FormElegirTalles : Form
    {
        public List<Talle> TallesSeleccionados { get; set; }
        public decimal PrecioVenta { get; set; }
        List<Talle> _listaTalles;
        List<Talle> _listaTallesEditar;

        Conexion _conexion;
        public FormElegirTalles(Conexion conexion, string nombreProducto, List<Talle> listaTalles)
            :this(conexion,nombreProducto,listaTalles,null,null)
        {
        }

        public FormElegirTalles(Conexion conexion, string nombreProducto, List<Talle> listaTalles, List<Talle> tallesSeleccionados, decimal? precioventa)
        {
            _listaTalles = listaTalles;
            _listaTallesEditar = tallesSeleccionados;
            _conexion = conexion;
            InitializeComponent();
            if (precioventa.HasValue)
                tbPrecioVenta.Text = precioventa.ToString();
            TallesSeleccionados = new List<Talle>();
            labProducto.Text = nombreProducto;
            tbPrecioVenta.KeyPress += Utilidades.ControlesTextBox.tbPrecio_KeyPress;
        }

        private void FormElegirTalles_Load(object sender, EventArgs e)
        {
            TalleOperaciones to = new TalleOperaciones(_conexion);
            List<Talle> lt = to.ObtenerTodosLista();
            List<Talle> tallesNumeros = new List<Talle>();
            List<Talle> tallesEses = new List<Talle>();
            List<Talle> tallesEmes = new List<Talle>();
            List<Talle> tallesEles = new List<Talle>();
            List<Talle> tallesOtros = new List<Talle>();
            foreach (Talle t in _listaTalles)
            {
                int salida;
                if (int.TryParse(t.Sigla, out salida))
                {
                    tallesNumeros.Add(t);
                }
                else
                {
                    if (t.Sigla.ToLower().Contains('s'))
                    {
                        tallesEses.Add(t);
                    }
                    else
                        if (t.Sigla.ToLower().Contains('m'))
                        {
                            tallesEmes.Add(t);
                        }
                        else
                            if (t.Sigla.ToLower().Contains('l'))
                            {
                                tallesEles.Add(t);
                            }
                            else
                                tallesOtros.Add(t);
                }
            }
            List<Talle> talles = new List<Talle>();
            tallesNumeros =tallesNumeros.OrderBy(p => int.Parse(p.Sigla)).ToList();
            tallesEses = tallesEses.OrderByDescending(t => t.Sigla).ToList();
            tallesEmes = tallesEmes.OrderByDescending(t => t.Sigla).ToList();
            tallesEles = tallesEles.OrderBy(t => t.Sigla).ToList();
            talles.AddRange(tallesNumeros);
            talles.AddRange(tallesEses);
            talles.AddRange(tallesEmes);
            talles.AddRange(tallesEles);
            talles.AddRange(tallesOtros);
            foreach (Talle t in talles)
            {
                LabelTalle lab = new LabelTalle();
                lab.Talle = t;
                lab.Text = t.Sigla;
                lab.Name = t.Sigla;
                lab.Margin = new Padding(3);
                flowTalles.Controls.Add(lab);
            }
            foreach (Talle t in _listaTallesEditar)
            {
                (flowTalles.Controls.Find(t.Sigla,false)[0] as LabelTalle).Seleccionado = true;
            }
        }

        private void botSeleccionarTodo_Click(object sender, EventArgs e)
        {
            foreach (LabelTalle lt in flowTalles.Controls)
            {
                lt.Seleccionado = true;
            }
        }

        private void botQuitarSeleccion_Click(object sender, EventArgs e)
        {
            foreach (LabelTalle lt in flowTalles.Controls)
            {
                lt.Seleccionado = false;
            }
        }

        private void botAceptar_Click(object sender, EventArgs e)
        {
            foreach (LabelTalle lt in flowTalles.Controls)
            {
                if (lt.Seleccionado)
                {
                    TallesSeleccionados.Add(lt.Talle);
                }
            }
            if (TallesSeleccionados.Count == 0)
            {
                MessageBox.Show("Debe seleccionar como mínimo un talle");
                return;
            }
            decimal precio;
            if (decimal.TryParse(tbPrecioVenta.Text, out precio))
                PrecioVenta = precio;
            else
            {
                TallesSeleccionados.Clear();
                MessageBox.Show("Ingrese un precio válido");
                tbPrecioVenta.Focus();
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }        
    }

    public delegate void SeleccionadoEventHandler(object sender, EventArgs e);

    public class LabelTalle : Label
    {
        private bool _seleccionado;
        public bool Seleccionado {
            get { return _seleccionado; }
            set 
            { 
                _seleccionado = value;
                if (_seleccionado)
                    this.BackColor = Color.FromKnownColor(KnownColor.Gray);
                else
                    this.BackColor = Color.Transparent;
            }
        }

        public Talle Talle { get; set; }

        public LabelTalle()
        {
            Seleccionado = false;
            Font f = new System.Drawing.Font(Font.FontFamily, 12);
            Font = f;
            Width = 50;
            Height = 50;
            TextAlign = ContentAlignment.MiddleCenter;
            Click += LabClick;
        }

        private void LabClick(object sender, EventArgs e)
        {
            LabelTalle ctrl = sender as LabelTalle;
            if (!ctrl.Seleccionado)
            {
                ctrl.Seleccionado = true;
            }
            else
            {
                ctrl.Seleccionado = false;
            }
        }
    }
}
