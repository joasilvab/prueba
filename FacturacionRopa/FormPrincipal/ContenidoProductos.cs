using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Modelo;

namespace Formularios
{
    public partial class ContenidoProductos : ContenidoBase
    {
        enum Acciones { CargarProducto, EditarProducto, EliminarProducto }
        Acciones Accion;
        Conexion _conexion;
        List<ProdTalle> _listaProdTalles = new List<ProdTalle>();
        TalleOperaciones _top;
        ProductoOperaciones _pop;
        ProdTalleOperaciones _ptop;

        void InicializarTalleOperaciones()
        {
            if (_top == null)
                _top = new TalleOperaciones(_conexion);
        }

        void InicializarProdTalleOperaciones()
        {
            if (_ptop == null)
                _ptop = new ProdTalleOperaciones(_conexion);
        }

        void InicializarProductoOperaciones()
        {
            if (_pop == null)
                _pop = new ProductoOperaciones(_conexion);
        }

        public ContenidoProductos(Conexion conexion)
        {
            _conexion = conexion;
            InitializeComponent();
            InicializarProdTalleOperaciones();
            InicializarProductoOperaciones();
            InicializarTalleOperaciones();
            //Inicia
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InicializarTalleOperaciones();
            List<Talle> listaTalles = new List<Talle>();
            try
            {
                listaTalles = _top.ObtenerTodosLista();
                foreach (ProdTalle pt in _listaProdTalles)
                {
                    Talle t = pt.Talle(_conexion);
                    if (listaTalles.Contains(t))
                    {
                        listaTalles.Remove(t);
                    }
                }
                FormElegirTalles fet = new FormElegirTalles(_conexion, tbProductoNombre.Text, listaTalles);
                if (fet.ShowDialog() == DialogResult.OK)
                {
                    string talles = "";
                    foreach (Talle t in fet.TallesSeleccionados)
                    {
                        talles += t.Sigla + " - ";
                        _listaProdTalles.Add(new ProdTalle() { TalleId = t.Id, PrecioVenta = fet.PrecioVenta, Renglon = dgvProdTalles.Rows.Count });
                    }
                    talles = talles.Remove(talles.Length - 2);
                    dgvProdTalles.Rows.Add(new object[] { talles, fet.PrecioVenta });
                }
            }
            catch (ModeloOperacionesException exc)
            {
                MessageBox.Show(exc.Message);
                return;
            }
        }

        private void botEditarProdTalle_Click(object sender, EventArgs e)
        {
            if (dgvProdTalles.SelectedRows.Count > 0)
            {
                try
                {
                    //InicializarProdTalleOperaciones();
                    InicializarTalleOperaciones();
                    List<Talle> seleccionados = new List<Talle>();
                    int index = dgvProdTalles.SelectedRows[0].Index;
                    foreach (ProdTalle pt in _listaProdTalles.FindAll(p => p.Renglon == index))
                    {
                        seleccionados.Add(pt.Talle(_conexion));//No se, tal vez se conecte mucho
                    }
                    List<Talle> listaTalles = new List<Talle>();
                    listaTalles = _top.ObtenerTodosLista();
                    foreach (ProdTalle pt in _listaProdTalles)
                    {
                        Talle t = pt.Talle(_conexion);
                        if (listaTalles.Contains(t))
                        {
                            listaTalles.Remove(t);
                        }
                    }
                    listaTalles.AddRange(seleccionados);
                    FormElegirTalles fet = new FormElegirTalles(_conexion, tbProductoNombre.Text, listaTalles, seleccionados, (decimal)dgvProdTalles.SelectedRows[0].Cells[1].Value);
                    if (fet.ShowDialog() == DialogResult.OK)
                    {
                        _listaProdTalles.RemoveAll(p => p.Renglon == index);
                        string talles = "";
                        foreach (Talle t in fet.TallesSeleccionados)
                        {
                            talles += t.Sigla + " - ";
                            _listaProdTalles.Add(new ProdTalle() { TalleId = t.Id, PrecioVenta = fet.PrecioVenta, Renglon = index });
                        }
                        talles = talles.Remove(talles.Length - 2);
                        dgvProdTalles.Rows[index].SetValues(talles, fet.PrecioVenta);
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message +"\n\n"+exc.StackTrace);
                }
            }
        }

        private void botBorrarProdTalle_Click(object sender, EventArgs e)
        {
            if (dgvProdTalles.SelectedRows.Count > 0)
            {
                List<int> indexes = new List<int>();
                foreach (DataGridViewRow dr in dgvProdTalles.SelectedRows)
                {
                    indexes.Add(dr.Index);
                }
                foreach (int i in indexes)
                {
                    _listaProdTalles.RemoveAll(p => p.Renglon == i);
                    foreach (ProdTalle pt in _listaProdTalles.FindAll(p => p.Renglon > i))
                    {
                        pt.Renglon--;
                    }
                    dgvProdTalles.Rows.RemoveAt(i);
                }
            }
        }

        private void botAgregar_Click(object sender, EventArgs e)
        {
            gbProdDatos.Enabled = true;
            gbTalles.Enabled = true;
            gbProdDatos.Text = "Nuevo producto";
            dgvProdTalles.Rows.Clear();
            _listaProdTalles.Clear();
            tbProductoClave.Clear();
            tbProductoClave.Enabled = false;
            tbProductoNombre.Enabled = true;
            tbProductoNombre.Clear();
            tbProductoNombre.Focus();
            panelTallesAcciones.Enabled = true;
            foreach (Control c in flowBotFinalizar.Controls)
            {
                if (c == botGuardar)
                    c.Visible = true;
                else
                    c.Visible = false;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (tbProductoClave.Focused)
                if (keyData == Keys.Tab)
                {
                    KeyPressEventArgs kea = new KeyPressEventArgs((char)Keys.Tab);
                    tbProductoClave_KeyPress(tbProductoClave, kea);
                    return true;
                }
            if (keyData == Keys.F2)
            {
                botAgregar.PerformClick();
                return true;
            }
            if (keyData == Keys.F3)
            {
                botEditar.PerformClick();
                return true;
            }
            if (keyData == Keys.F4)
            {
                botEliminar.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool ControlarDatos()
        {
            if (string.IsNullOrWhiteSpace(tbProductoNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto");
                tbProductoNombre.Focus();
                return false;
            }
            if (_listaProdTalles.Count == 0)
            {
                MessageBox.Show("Debe especificar los precios de los talles de las prendas");
                botAgregarProdTalle.PerformClick();
                return false;
            }
            return true;
        }


        private void botGuardar_Click(object sender, EventArgs e)
        {
            if (ControlarDatos())
            {
                try
                {
                    InicializarProdTalleOperaciones();
                    InicializarProductoOperaciones();
                    Producto prod = new Producto();
                    prod.Descripcion = tbProductoNombre.Text.Trim();
                    prod.TasaIVAId = 1;//Tasa General
                    _pop.AgregarProductoConTalles(prod, _listaProdTalles, _conexion);
                    SetStatusText(string.Format("El producto '{1}' ha sido cargado correctamente.",prod.Descripcion),Utilidades.Colores.Correcto,true);
                    ActualizarDataGrid();
                    botAgregar.PerformClick();
                }
                catch (ModeloEntradaDuplicadaException duplex)
                {
                    if (duplex.PropiedadDuplicado == ProductoOperaciones.Descripcion)
                    {
                        MessageBox.Show("¡Ya existe un producto con esa descripción!");
                    }
                    else
                        MessageBox.Show(duplex.Message);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
                }
            }
        }

        void ActualizarDataGrid()
        {
            try
            {
                InicializarProductoOperaciones();
                InicializarTalleOperaciones();
                InicializarProdTalleOperaciones();
                List<RenglonProducto> lrp = new List<RenglonProducto>();
                List<ProductoIVA> listProdIVA = _pop.ObtenerConIVA(_conexion);
                List<ProdTalleDescripcion> lptd = _ptop.ObtenerProdTallesDescripciones(_conexion,null);
                lptd = _ptop.OrdenarTallesDescripcion(lptd);
                List<Talle> listTalles = _top.ObtenerTodosLista();
                foreach (ProductoIVA p in listProdIVA)
                {
                    List<ProdTalleDescripcion> tallesProd = lptd.FindAll(t => t.ProdId == p.Id);
                    bool corte = false;
                    bool primero = true;
                    int renglon = 0;
                    while (!corte)
                    {
                        RenglonProducto rp = new RenglonProducto();
                        string talles = "";
                        List<ProdTalleDescripcion> tallesRenglon = tallesProd.FindAll(t => t.Renglon == renglon);
                        if (tallesRenglon.Count > 0)
                        {
                            decimal precioVenta = tallesRenglon.First().PrecioVenta;
                            foreach (ProdTalleDescripcion pt in tallesRenglon)
                            {
                                talles += pt.TalleDesc + " - ";
                            }
                            tallesProd.RemoveAll(t => t.Renglon == renglon);
                            talles = talles.Remove(talles.Length - 3);
                            if (primero)
                            {
                                rp.Articulo = p.Descripcion;
                                primero = false;
                            }
                            rp.Talles = talles;
                            rp.Precio = precioVenta;
                            rp.Iva = precioVenta * p.Iva;
                            rp.Total = precioVenta * (p.Iva + 1);
                            lrp.Add(rp);
                            renglon++;
                        }
                        else
                            corte = true;
                    }
                    lrp.Add(new RenglonProducto());
                }
                dgvProductos.DataSource = lrp;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
            }
        }



        private void botActualizar_Click(object sender, EventArgs e)
        {
            ActualizarDataGrid();
        }

        private void botEditar_Click(object sender, EventArgs e)
        {
            Accion = Acciones.EditarProducto;
            SetStatusText("Ingrese la clave (id) del producto y presione TAB o Enter. Si no recuerda el id, presione F1 o cualquier letra para mostrar la ayuda.",Utilidades.Colores.Normal,false);
            gbProdDatos.Enabled = true;
            gbTalles.Enabled = false;
            gbProdDatos.Text = "Editar producto";
            dgvProdTalles.Rows.Clear();
            _listaProdTalles.Clear();
            tbProductoClave.Clear();
            tbProductoClave.Enabled = true;
            tbProductoClave.Focus();
            tbProductoNombre.Clear();
            tbProductoNombre.Enabled = false;
            foreach (Control c in flowBotFinalizar.Controls)
            {
                if (c == botGuardarCambios)
                    c.Visible = true;
                else
                    c.Visible = false;
            }
        }

        formAyuda GenerarAyudaProductos()
        {
            try
            {
                InicializarProductoOperaciones();
                List<string> propiedades = new List<string>();
                propiedades.Add(ProductoOperaciones.Id);
                propiedades.Add(ProductoOperaciones.Descripcion);
                List<object> datos = new List<object>();
                foreach (Producto prod in _pop.ObtenerTodosLista())
                {
                    datos.Add((object)prod);
                }
                return new formAyuda(_conexion, propiedades, datos);
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
                return null;
            }
        }

        void CargarProductoParaEditar(Producto producto)
        {
            try
            {
                CargarProducto(producto);
                SetStatusText("",Utilidades.Colores.Normal,false);
                tbProductoNombre.Enabled = true;
                tbProductoNombre.SelectAll();
                tbProductoClave.Enabled = false;
                gbTalles.Enabled = true;
                panelTallesAcciones.Enabled = true;
                //InicializarProdTalleOperaciones();
                //StatusLabelText = "";
                //tbProductoNombre.Enabled = true;
                //tbProductoNombre.Text = producto.Descripcion;
                //tbProductoNombre.SelectAll();
                //tbProductoClave.Text = producto.Id.ToString();
                //tbProductoClave.Enabled = false;
                //ModeloWhere mw = new ModeloWhere(ProdTalleOperaciones.ProdId, producto.Id.ToString(), true, Utilidades.Signos.Igual);
                //List<ProdTalleDescripcion> lprd = _ptop.ObtenerProdTallesDescripciones(_conexion, new List<ModeloWhere> { mw });
                //lprd = _ptop.OrdenarTallesDescripcion(lprd);
                //int ultimo = lprd.Max(p => p.Renglon);
                //for (int i = 0; i <= ultimo; i++)
                //{
                //    string talles = "";
                //    List<ProdTalleDescripcion> listaRenglon = lprd.FindAll(t => t.Renglon == i);
                //    foreach (ProdTalleDescripcion t in listaRenglon)
                //    {
                //        talles += t.TalleDesc + " - ";
                //    }
                //    talles = talles.Remove(talles.Length - 2);
                //    dgvProdTalles.Rows.Add(talles, listaRenglon[0].PrecioVenta);
                //    lprd.RemoveAll(t => t.Renglon == i);
                //}
                //ModeloWhere condicion = new ModeloWhere(ProdTalleOperaciones.ProdId, producto.Id.ToString(), true, Utilidades.Signos.Igual);
                //_listaProdTalles = _ptop.ObtenerEnList(new List<ModeloWhere> { condicion });
                //gbTalles.Enabled = true;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
            }
        }

        void CargarProducto(Producto producto)
        {
            InicializarProdTalleOperaciones();
            tbProductoNombre.Text = producto.Descripcion;
            tbProductoClave.Text = producto.Id.ToString();
            ModeloWhere mw = new ModeloWhere(ProdTalleOperaciones.ProdId, producto.Id.ToString(), true, Utilidades.Signos.Igual);
            List<ProdTalleDescripcion> lprd = _ptop.ObtenerProdTallesDescripciones(_conexion, new List<ModeloWhere> { mw });
            lprd = _ptop.OrdenarTallesDescripcion(lprd);
            int ultimo = lprd.Max(p => p.Renglon);
            for (int i = 0; i <= ultimo; i++)
            {
                string talles = "";
                List<ProdTalleDescripcion> listaRenglon = lprd.FindAll(t => t.Renglon == i);
                foreach (ProdTalleDescripcion t in listaRenglon)
                {
                    talles += t.TalleDesc + " - ";
                }
                talles = talles.Remove(talles.Length - 2);
                dgvProdTalles.Rows.Add(talles, listaRenglon[0].PrecioVenta);
                lprd.RemoveAll(t => t.Renglon == i);
            }
            ModeloWhere condicion = new ModeloWhere(ProdTalleOperaciones.ProdId, producto.Id.ToString(), true, Utilidades.Signos.Igual);
            _listaProdTalles = _ptop.ObtenerEnList(new List<ModeloWhere> { condicion });
        }

        void CargarProductoParaEliminar(Producto producto)
        {
            try
            {
                CargarProducto(producto);
                panelTallesAcciones.Enabled = false;
                tbProductoClave.Enabled = false;
                botEliminarProducto.Focus();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
            }
        }


        private void tbProductoClave_HelpRequested(object sender, HelpEventArgs hlpevent)
        {
            formAyuda fa = GenerarAyudaProductos();
            if (fa.ShowDialog() == DialogResult.OK)
            {
                
                Producto prod = (Producto)fa.ElementoDevuelto;
                if (Accion == Acciones.EditarProducto)
                    CargarProductoParaEditar(prod);
                else
                    CargarProductoParaEliminar(prod);
            }
        }

        private void botGuardarCambios_Click(object sender, EventArgs e)
        {
            if (ControlarDatos())
            {
                try
                {
                    InicializarProdTalleOperaciones();
                    InicializarProductoOperaciones();
                    Producto prod = new Producto(){ Id = Convert.ToInt32(tbProductoClave.Text), Descripcion = tbProductoNombre.Text.Trim(), TasaIVAId = 1};
                    _pop.ModificarProductoConTalles(prod, _listaProdTalles, _conexion);
                    SetStatusText(string.Format("El producto '{1}' ha sido modificado correctamente.", prod.Descripcion),Utilidades.Colores.Correcto,false);
                    ActualizarDataGrid();
                    //bot.PerformClick();
                }
                catch (ModeloEntradaDuplicadaException duplex)
                {
                    if (duplex.PropiedadDuplicado == ProductoOperaciones.Descripcion)
                    {
                        MessageBox.Show("¡Ya existe un producto con esa descripción!");
                    }
                    else
                        MessageBox.Show(duplex.Message);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
                }
            }
        }

        private void tbProductoClave_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if ((e.KeyChar == (char)Keys.Enter || e.KeyChar == (char)Keys.Tab))
                {
                    Producto prod = _pop.Obtener(new ModeloWhere(ProductoOperaciones.Id, tbProductoClave.Text, true, Utilidades.Signos.Igual));
                    if (prod == null)
                    {
                        e.Handled = true;
                        formAyuda fa = GenerarAyudaProductos();
                        if (fa.ShowDialog() == DialogResult.OK)
                            if (Accion == Acciones.EditarProducto)
                                CargarProductoParaEditar((Producto)fa.ElementoDevuelto);
                            else
                                CargarProductoParaEliminar((Producto)fa.ElementoDevuelto);
                    }
                    else
                        if (Accion == Acciones.EditarProducto)
                            CargarProductoParaEditar(prod);
                        else
                            CargarProductoParaEliminar(prod);
                }
                else
                    if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != (char)Keys.Back)
                    {
                        e.Handled = true;
                        formAyuda fa = GenerarAyudaProductos();
                        if (fa.ShowDialog() == DialogResult.OK)
                            if (Accion == Acciones.EditarProducto)
                                CargarProductoParaEditar((Producto)fa.ElementoDevuelto);
                            else
                                CargarProductoParaEliminar((Producto)fa.ElementoDevuelto);
                    }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
            }
        }

        private void botEliminar_Click(object sender, EventArgs e)
        {
            Accion = Acciones.EliminarProducto;
            foreach (Control c in flowBotFinalizar.Controls)
            {
                if (c == botEliminarProducto)
                    c.Visible = true;
                else
                    c.Visible = false;
            }
            gbProdDatos.Enabled = true;
            gbProdDatos.Text = "Eliminar producto";
            dgvProdTalles.Rows.Clear();
            tbProductoClave.Enabled = true; tbProductoClave.Clear(); tbProductoClave.Focus();
            tbProductoNombre.Enabled = false; tbProductoNombre.Clear();
            gbTalles.Enabled = true;
            //botAgregarProdTalle.Enabled = botBorrarProdTalle.Enabled = botEditarProdTalle.Enabled = false;
            panelTallesAcciones.Enabled = false;
        }

        private void botEliminarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                InicializarProductoOperaciones();
                Producto prod = _pop.Obtener(new ModeloWhere(ProductoOperaciones.Id,tbProductoClave.Text,true, Utilidades.Signos.Igual));
                if (prod != null)
                {
                    if (MessageBox.Show("¿Está seguro que desea eliminar el producto '"+prod.Descripcion+"'?","Advertencia", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2)== DialogResult.Yes)
                    {
                        _pop.EliminarProductoConTalles(prod, _conexion);
                        this.SetStatusText("El producto '" + prod.Descripcion + "' ha sido borrado con éxito", Utilidades.Colores.Correcto, true);
                        botEliminar.PerformClick();
                    }
                }
                else
                    MessageBox.Show("No se encontró el producto");
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message + "\n\n" + exc.StackTrace);
            }
        }

        private void ContenidoProductos_Load(object sender, EventArgs e)
        {

        }
    }
}
