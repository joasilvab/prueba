using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Modelo;
using System.Reflection;

namespace Formularios
{
    public partial class formAyuda : Form
    {
        List<ElementoAyuda> _listaDatos;
        public ElementoBase ElementoDevuelto { get; set; }

        public formAyuda(Conexion con, List<string> columnasMostrar, List<object> listaDatos)
        {
            try
            {
                InitializeComponent();
                _listaDatos = new List<ElementoAyuda>();
                foreach (object e in listaDatos)
                {
                    ElementoAyuda ea = new ElementoAyuda();
                    ea.Elemento = (ElementoBase)e;
                    foreach (string s in columnasMostrar)
                    {
                        ea.Mostrar += e.GetType().GetProperty(s).GetValue(e, null).ToString() + " -- ";
                    }
                    ea.Habilitado = true;
                    ea.Mostrar = ea.Mostrar.Remove(ea.Mostrar.Length - 3);
                    lbDatos.Items.Add(ea);
                    _listaDatos.Add(ea);
                }
                lbDatos.DisplayMember = "Mostrar";
                lbDatos.ValueMember = "Elemento";
                lbDatos.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public formAyuda(Conexion con, List<string> columnasMostrar, List<object> listaDatos, int ancho, int alto, Dictionary<string, string> valorDesactivado)
        {
            try
            {
                InitializeComponent();
                _listaDatos = new List<ElementoAyuda>();
                foreach (object e in listaDatos)
                {
                    ElementoAyuda ea = new ElementoAyuda();
                    ea.Elemento = (ElementoBase)e;
                    foreach (string s in columnasMostrar)
                    {
                        ea.Mostrar += e.GetType().GetProperty(s).GetValue(e, null).ToString() + " -- ";
                    }
                    ea.Habilitado = e.GetType().GetProperty(valorDesactivado.First().Key).GetValue(e, null).ToString() != valorDesactivado.First().Value;
                    ea.Mostrar = ea.Mostrar.Remove(ea.Mostrar.Length - 3);
                    lbDatos.Items.Add(ea);
                    _listaDatos.Add(ea);
                }
                lbDatos.DisplayMember = "Mostrar";
                lbDatos.ValueMember = "Elemento";
                lbDatos.SelectedIndex = 0;
            }
            catch (Exception e)
            {
                throw e;
            }
            this.Width = ancho;
            this.Height = alto;
        }

        public bool hayDatos;

        private void tbBuscar_TextChanged(object sender, EventArgs e)
        {
            lbDatos.Items.Clear();
            IEnumerable<ElementoAyuda> listaFiltrados = from filtrados in _listaDatos
                                                        where (filtrados.Mostrar.ToLower().Contains(tbBuscar.Text.ToLower()))// || filtrados.Mostrar.Contains(tbBuscar.Text.ToLower()))
                                                        select filtrados;
            foreach (ElementoAyuda s in listaFiltrados)
            {
                lbDatos.Items.Add(s);
            }
            if (lbDatos.Items.Count > 0)
                lbDatos.SelectedIndex = 0;
        }

        private void tbBuscar_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (lbDatos.Items.Count == 0)
                        this.botCerrar.PerformClick();
                    else
                        this.botAceptar.PerformClick();
                    break;
                case Keys.Up:
                    if (lbDatos.SelectedIndex != 0 && lbDatos.Items.Count > 0)
                    {
                        int index = 0;
                        if (sender is TextBox)
                        {
                            lbDatos.SelectedIndex -= 1;
                            index = lbDatos.SelectedIndex;
                        }
                        else
                        {
                            index = lbDatos.SelectedIndex - 1;
                        }
                        
                            ElementoAyuda ea = (ElementoAyuda)lbDatos.Items[index];
                            if (!ea.Habilitado)
                            {
                                //if (lbDatos.SelectedIndex - 1 < lbDatos.Items.Count)
                                //    lbDatos.SelectedIndex -= 1;
                                lbDatos.SelectedIndex = BuscarProximoHabilitado(false);
                                e.Handled = true;
                            }
                    }
                    break;
                case Keys.Down:
                    if (lbDatos.SelectedIndex != lbDatos.Items.Count - 1 && lbDatos.Items.Count > 0)
                    {
                        int index = 0;
                        if (sender is TextBox)
                        {
                            lbDatos.SelectedIndex += 1;
                            index = lbDatos.SelectedIndex;
                        }
                        else
                        {
                            index = lbDatos.SelectedIndex + 1;
                        }
                        //lbDatos.SelectedIndex += 1;
                        ElementoAyuda ea = (ElementoAyuda)lbDatos.Items[index];
                        if (!ea.Habilitado)
                        {
                            //if (lbDatos.SelectedIndex + 1 < lbDatos.Items.Count)
                                //lbDatos.SelectedIndex += 1;
                            lbDatos.SelectedIndex = BuscarProximoHabilitado(true);
                            e.Handled = true;
                        }
                    }
                    break;
                default:
                    break;
            }
        }
        int BuscarProximoHabilitado(bool paraAbajo)
        {
            if (!paraAbajo)
                for (int i = lbDatos.SelectedIndex-1; i >= 0; i--)
                {
                    ElementoAyuda ea = (ElementoAyuda)lbDatos.Items[i];
                    if (ea.Habilitado)
                    {
                        return i;
                    }
                }
            else
                for (int i = lbDatos.SelectedIndex+1; i < lbDatos.Items.Count; i++)
                {
                    ElementoAyuda ea = (ElementoAyuda)lbDatos.Items[i];
                    if (ea.Habilitado)
                    {
                        return i;
                    }
                }
            return 0;
        }

        private void botAceptar_Click(object sender, EventArgs e)
        {
            //valorDevuelto = "";
            if (lbDatos.Items.Count > 0)
            {
                ElementoDevuelto = ((ElementoAyuda)lbDatos.SelectedItem).Elemento;
                DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            
            //this.Close();
        }

        private void lbDatos_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = lbDatos.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                ElementoAyuda ea = (ElementoAyuda)lbDatos.Items[index];
                if (ea.Habilitado)
                {
                    botAceptar.PerformClick();
                }
            }
        }

        /// <summary>
        /// Handles the DrawItem event of the listBox1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Forms.DrawItemEventArgs"/> instance containing the event data.</param>
        private void lbDatos_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            Graphics g = e.Graphics;
            ElementoAyuda ea = (ElementoAyuda)lbDatos.Items[e.Index];
            // draw the background color you want
            // mine is set to olive, change it to whatever you want
            if (ea.Habilitado)
            {
                if (e.State.HasFlag(DrawItemState.Selected))
                {
                    g.FillRectangle(new SolidBrush(Color.LightBlue), e.Bounds);
                }
                else
                {
                    g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
                }
            }
            else
                g.FillRectangle(new SolidBrush(Color.LightGray), e.Bounds);
            
            
            // draw the text of the list item, not doing this will only show
            // the background color
            // you will need to get the text of item to display
            g.DrawString(ea.Mostrar, e.Font, new SolidBrush(Color.Black), new PointF(e.Bounds.X, e.Bounds.Y));
            e.DrawFocusRectangle();
        }

        private void lbDatos_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ElementoAyuda ea = (ElementoAyuda)lbDatos.Items[lbDatos.SelectedIndex];
            //if (!ea.Habilitado)
            //{
            //    if (lbDatos.SelectedIndex + 1 < lbDatos.Items.Count)
            //        lbDatos.SelectedIndex = lbDatos.SelectedIndex + 1;
            //}
        }
    }
}
