namespace Formularios
{
    partial class ContenidoProductos
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.gbAcciones = new System.Windows.Forms.GroupBox();
            this.botEliminar = new System.Windows.Forms.Button();
            this.botEditar = new System.Windows.Forms.Button();
            this.botAgregar = new System.Windows.Forms.Button();
            this.gbTalles = new System.Windows.Forms.GroupBox();
            this.panelTallesAcciones = new System.Windows.Forms.Panel();
            this.botBorrarProdTalle = new System.Windows.Forms.Button();
            this.botEditarProdTalle = new System.Windows.Forms.Button();
            this.botAgregarProdTalle = new System.Windows.Forms.Button();
            this.dgvProdTalles = new System.Windows.Forms.DataGridView();
            this.Talles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbProdDatos = new System.Windows.Forms.GroupBox();
            this.tbProductoClave = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbProductoNombre = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.articuloDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tallesDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precioDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ivaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.renglonProductoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.botGuardar = new System.Windows.Forms.Button();
            this.flowBotFinalizar = new System.Windows.Forms.FlowLayoutPanel();
            this.botGuardarCambios = new System.Windows.Forms.Button();
            this.botEliminarProducto = new System.Windows.Forms.Button();
            this.botActualizar = new System.Windows.Forms.Button();
            this.gbInsumos = new System.Windows.Forms.GroupBox();
            this.chListInsumos = new System.Windows.Forms.CheckedListBox();
            this.gbAcciones.SuspendLayout();
            this.gbTalles.SuspendLayout();
            this.panelTallesAcciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdTalles)).BeginInit();
            this.gbProdDatos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.renglonProductoBindingSource)).BeginInit();
            this.flowBotFinalizar.SuspendLayout();
            this.gbInsumos.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Productos";
            // 
            // gbAcciones
            // 
            this.gbAcciones.Controls.Add(this.botEliminar);
            this.gbAcciones.Controls.Add(this.botEditar);
            this.gbAcciones.Controls.Add(this.botAgregar);
            this.gbAcciones.Location = new System.Drawing.Point(17, 39);
            this.gbAcciones.Name = "gbAcciones";
            this.gbAcciones.Size = new System.Drawing.Size(393, 65);
            this.gbAcciones.TabIndex = 1;
            this.gbAcciones.TabStop = false;
            // 
            // botEliminar
            // 
            this.botEliminar.Location = new System.Drawing.Point(295, 17);
            this.botEliminar.Name = "botEliminar";
            this.botEliminar.Size = new System.Drawing.Size(75, 34);
            this.botEliminar.TabIndex = 2;
            this.botEliminar.Text = "Eliminar (F4)";
            this.botEliminar.UseVisualStyleBackColor = true;
            this.botEliminar.Click += new System.EventHandler(this.botEliminar_Click);
            // 
            // botEditar
            // 
            this.botEditar.Location = new System.Drawing.Point(157, 17);
            this.botEditar.Name = "botEditar";
            this.botEditar.Size = new System.Drawing.Size(75, 34);
            this.botEditar.TabIndex = 1;
            this.botEditar.Text = "Editar (F3)";
            this.botEditar.UseVisualStyleBackColor = true;
            this.botEditar.Click += new System.EventHandler(this.botEditar_Click);
            // 
            // botAgregar
            // 
            this.botAgregar.Location = new System.Drawing.Point(11, 17);
            this.botAgregar.Name = "botAgregar";
            this.botAgregar.Size = new System.Drawing.Size(75, 34);
            this.botAgregar.TabIndex = 0;
            this.botAgregar.Text = "Nuevo (F2)";
            this.botAgregar.UseVisualStyleBackColor = true;
            this.botAgregar.Click += new System.EventHandler(this.botAgregar_Click);
            // 
            // gbTalles
            // 
            this.gbTalles.Controls.Add(this.panelTallesAcciones);
            this.gbTalles.Controls.Add(this.dgvProdTalles);
            this.gbTalles.Enabled = false;
            this.gbTalles.Location = new System.Drawing.Point(17, 216);
            this.gbTalles.Name = "gbTalles";
            this.gbTalles.Size = new System.Drawing.Size(393, 202);
            this.gbTalles.TabIndex = 3;
            this.gbTalles.TabStop = false;
            this.gbTalles.Text = "Talles";
            // 
            // panelTallesAcciones
            // 
            this.panelTallesAcciones.Controls.Add(this.botBorrarProdTalle);
            this.panelTallesAcciones.Controls.Add(this.botEditarProdTalle);
            this.panelTallesAcciones.Controls.Add(this.botAgregarProdTalle);
            this.panelTallesAcciones.Location = new System.Drawing.Point(10, 20);
            this.panelTallesAcciones.Name = "panelTallesAcciones";
            this.panelTallesAcciones.Size = new System.Drawing.Size(96, 103);
            this.panelTallesAcciones.TabIndex = 6;
            // 
            // botBorrarProdTalle
            // 
            this.botBorrarProdTalle.Location = new System.Drawing.Point(9, 67);
            this.botBorrarProdTalle.Name = "botBorrarProdTalle";
            this.botBorrarProdTalle.Size = new System.Drawing.Size(75, 23);
            this.botBorrarProdTalle.TabIndex = 5;
            this.botBorrarProdTalle.Text = "Borrar";
            this.botBorrarProdTalle.UseVisualStyleBackColor = true;
            this.botBorrarProdTalle.Click += new System.EventHandler(this.botBorrarProdTalle_Click);
            // 
            // botEditarProdTalle
            // 
            this.botEditarProdTalle.Location = new System.Drawing.Point(10, 38);
            this.botEditarProdTalle.Name = "botEditarProdTalle";
            this.botEditarProdTalle.Size = new System.Drawing.Size(75, 23);
            this.botEditarProdTalle.TabIndex = 4;
            this.botEditarProdTalle.Text = "Editar";
            this.botEditarProdTalle.UseVisualStyleBackColor = true;
            this.botEditarProdTalle.Click += new System.EventHandler(this.botEditarProdTalle_Click);
            // 
            // botAgregarProdTalle
            // 
            this.botAgregarProdTalle.Location = new System.Drawing.Point(10, 9);
            this.botAgregarProdTalle.Name = "botAgregarProdTalle";
            this.botAgregarProdTalle.Size = new System.Drawing.Size(75, 23);
            this.botAgregarProdTalle.TabIndex = 3;
            this.botAgregarProdTalle.Text = "Agregar";
            this.botAgregarProdTalle.UseVisualStyleBackColor = true;
            this.botAgregarProdTalle.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvProdTalles
            // 
            this.dgvProdTalles.AllowUserToAddRows = false;
            this.dgvProdTalles.AllowUserToDeleteRows = false;
            this.dgvProdTalles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProdTalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Talles,
            this.Precio});
            this.dgvProdTalles.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvProdTalles.Location = new System.Drawing.Point(120, 19);
            this.dgvProdTalles.Name = "dgvProdTalles";
            this.dgvProdTalles.RowHeadersVisible = false;
            this.dgvProdTalles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProdTalles.Size = new System.Drawing.Size(258, 167);
            this.dgvProdTalles.TabIndex = 0;
            this.dgvProdTalles.TabStop = false;
            // 
            // Talles
            // 
            this.Talles.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Talles.DefaultCellStyle = dataGridViewCellStyle7;
            this.Talles.HeaderText = "Talles";
            this.Talles.Name = "Talles";
            this.Talles.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Precio
            // 
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "$ 0.00";
            dataGridViewCellStyle8.NullValue = null;
            this.Precio.DefaultCellStyle = dataGridViewCellStyle8;
            this.Precio.HeaderText = "Precio";
            this.Precio.Name = "Precio";
            this.Precio.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Precio.Width = 50;
            // 
            // gbProdDatos
            // 
            this.gbProdDatos.Controls.Add(this.tbProductoClave);
            this.gbProdDatos.Controls.Add(this.label3);
            this.gbProdDatos.Controls.Add(this.tbProductoNombre);
            this.gbProdDatos.Controls.Add(this.label2);
            this.gbProdDatos.Enabled = false;
            this.gbProdDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbProdDatos.Location = new System.Drawing.Point(17, 110);
            this.gbProdDatos.Name = "gbProdDatos";
            this.gbProdDatos.Size = new System.Drawing.Size(393, 100);
            this.gbProdDatos.TabIndex = 2;
            this.gbProdDatos.TabStop = false;
            // 
            // tbProductoClave
            // 
            this.tbProductoClave.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProductoClave.Location = new System.Drawing.Point(71, 31);
            this.tbProductoClave.Name = "tbProductoClave";
            this.tbProductoClave.Size = new System.Drawing.Size(293, 20);
            this.tbProductoClave.TabIndex = 1;
            this.tbProductoClave.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.tbProductoClave_HelpRequested);
            this.tbProductoClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbProductoClave_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(18, 34);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(37, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Clave:";
            // 
            // tbProductoNombre
            // 
            this.tbProductoNombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProductoNombre.Location = new System.Drawing.Point(71, 70);
            this.tbProductoNombre.Name = "tbProductoNombre";
            this.tbProductoNombre.Size = new System.Drawing.Size(293, 20);
            this.tbProductoNombre.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(18, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Nombre:";
            // 
            // dgvProductos
            // 
            this.dgvProductos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProductos.AutoGenerateColumns = false;
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articuloDataGridViewTextBoxColumn,
            this.tallesDataGridViewTextBoxColumn,
            this.precioDataGridViewTextBoxColumn,
            this.ivaDataGridViewTextBoxColumn,
            this.totalDataGridViewTextBoxColumn});
            this.dgvProductos.DataSource = this.renglonProductoBindingSource;
            this.dgvProductos.Location = new System.Drawing.Point(439, 75);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.RowHeadersVisible = false;
            this.dgvProductos.Size = new System.Drawing.Size(440, 598);
            this.dgvProductos.TabIndex = 4;
            this.dgvProductos.TabStop = false;
            // 
            // articuloDataGridViewTextBoxColumn
            // 
            this.articuloDataGridViewTextBoxColumn.DataPropertyName = "Articulo";
            this.articuloDataGridViewTextBoxColumn.HeaderText = "Articulo";
            this.articuloDataGridViewTextBoxColumn.Name = "articuloDataGridViewTextBoxColumn";
            this.articuloDataGridViewTextBoxColumn.Width = 300;
            // 
            // tallesDataGridViewTextBoxColumn
            // 
            this.tallesDataGridViewTextBoxColumn.DataPropertyName = "Talles";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.tallesDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle9;
            this.tallesDataGridViewTextBoxColumn.HeaderText = "Talles";
            this.tallesDataGridViewTextBoxColumn.Name = "tallesDataGridViewTextBoxColumn";
            this.tallesDataGridViewTextBoxColumn.Width = 200;
            // 
            // precioDataGridViewTextBoxColumn
            // 
            this.precioDataGridViewTextBoxColumn.DataPropertyName = "Precio";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "$0.00";
            dataGridViewCellStyle10.NullValue = null;
            this.precioDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle10;
            this.precioDataGridViewTextBoxColumn.HeaderText = "Precio";
            this.precioDataGridViewTextBoxColumn.Name = "precioDataGridViewTextBoxColumn";
            // 
            // ivaDataGridViewTextBoxColumn
            // 
            this.ivaDataGridViewTextBoxColumn.DataPropertyName = "Iva";
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "$0.00";
            dataGridViewCellStyle11.NullValue = null;
            this.ivaDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle11;
            this.ivaDataGridViewTextBoxColumn.HeaderText = "Iva";
            this.ivaDataGridViewTextBoxColumn.Name = "ivaDataGridViewTextBoxColumn";
            // 
            // totalDataGridViewTextBoxColumn
            // 
            this.totalDataGridViewTextBoxColumn.DataPropertyName = "Total";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.Format = "$0.00";
            dataGridViewCellStyle12.NullValue = null;
            this.totalDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle12;
            this.totalDataGridViewTextBoxColumn.HeaderText = "Total";
            this.totalDataGridViewTextBoxColumn.Name = "totalDataGridViewTextBoxColumn";
            // 
            // renglonProductoBindingSource
            // 
            this.renglonProductoBindingSource.AllowNew = false;
            this.renglonProductoBindingSource.DataSource = typeof(Formularios.RenglonProducto);
            // 
            // botGuardar
            // 
            this.botGuardar.Location = new System.Drawing.Point(315, 3);
            this.botGuardar.Name = "botGuardar";
            this.botGuardar.Size = new System.Drawing.Size(75, 36);
            this.botGuardar.TabIndex = 6;
            this.botGuardar.Text = "Guardar";
            this.botGuardar.UseVisualStyleBackColor = true;
            this.botGuardar.Visible = false;
            this.botGuardar.Click += new System.EventHandler(this.botGuardar_Click);
            // 
            // flowBotFinalizar
            // 
            this.flowBotFinalizar.Controls.Add(this.botGuardar);
            this.flowBotFinalizar.Controls.Add(this.botGuardarCambios);
            this.flowBotFinalizar.Controls.Add(this.botEliminarProducto);
            this.flowBotFinalizar.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowBotFinalizar.Location = new System.Drawing.Point(17, 609);
            this.flowBotFinalizar.Name = "flowBotFinalizar";
            this.flowBotFinalizar.Size = new System.Drawing.Size(393, 43);
            this.flowBotFinalizar.TabIndex = 4;
            // 
            // botGuardarCambios
            // 
            this.botGuardarCambios.Location = new System.Drawing.Point(234, 3);
            this.botGuardarCambios.Name = "botGuardarCambios";
            this.botGuardarCambios.Size = new System.Drawing.Size(75, 36);
            this.botGuardarCambios.TabIndex = 6;
            this.botGuardarCambios.Text = "Guardar Cambios";
            this.botGuardarCambios.UseVisualStyleBackColor = true;
            this.botGuardarCambios.Visible = false;
            this.botGuardarCambios.Click += new System.EventHandler(this.botGuardarCambios_Click);
            // 
            // botEliminarProducto
            // 
            this.botEliminarProducto.Location = new System.Drawing.Point(153, 3);
            this.botEliminarProducto.Name = "botEliminarProducto";
            this.botEliminarProducto.Size = new System.Drawing.Size(75, 36);
            this.botEliminarProducto.TabIndex = 7;
            this.botEliminarProducto.Text = "Eliminar";
            this.botEliminarProducto.UseVisualStyleBackColor = true;
            this.botEliminarProducto.Visible = false;
            this.botEliminarProducto.Click += new System.EventHandler(this.botEliminarProducto_Click);
            // 
            // botActualizar
            // 
            this.botActualizar.Location = new System.Drawing.Point(804, 46);
            this.botActualizar.Name = "botActualizar";
            this.botActualizar.Size = new System.Drawing.Size(75, 23);
            this.botActualizar.TabIndex = 5;
            this.botActualizar.Text = "Actualizar";
            this.botActualizar.UseVisualStyleBackColor = true;
            this.botActualizar.Click += new System.EventHandler(this.botActualizar_Click);
            // 
            // gbInsumos
            // 
            this.gbInsumos.Controls.Add(this.chListInsumos);
            this.gbInsumos.Location = new System.Drawing.Point(17, 424);
            this.gbInsumos.Name = "gbInsumos";
            this.gbInsumos.Size = new System.Drawing.Size(393, 127);
            this.gbInsumos.TabIndex = 6;
            this.gbInsumos.TabStop = false;
            this.gbInsumos.Text = "Insumos";
            // 
            // chListInsumos
            // 
            this.chListInsumos.FormattingEnabled = true;
            this.chListInsumos.Location = new System.Drawing.Point(10, 19);
            this.chListInsumos.Name = "chListInsumos";
            this.chListInsumos.Size = new System.Drawing.Size(368, 94);
            this.chListInsumos.TabIndex = 0;
            // 
            // ContenidoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbInsumos);
            this.Controls.Add(this.botActualizar);
            this.Controls.Add(this.flowBotFinalizar);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.gbProdDatos);
            this.Controls.Add(this.gbTalles);
            this.Controls.Add(this.gbAcciones);
            this.Controls.Add(this.label1);
            this.Name = "ContenidoProductos";
            this.Size = new System.Drawing.Size(893, 689);
            this.Load += new System.EventHandler(this.ContenidoProductos_Load);
            this.gbAcciones.ResumeLayout(false);
            this.gbTalles.ResumeLayout(false);
            this.panelTallesAcciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdTalles)).EndInit();
            this.gbProdDatos.ResumeLayout(false);
            this.gbProdDatos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.renglonProductoBindingSource)).EndInit();
            this.flowBotFinalizar.ResumeLayout(false);
            this.gbInsumos.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbAcciones;
        private System.Windows.Forms.Button botEliminar;
        private System.Windows.Forms.Button botEditar;
        private System.Windows.Forms.Button botAgregar;
        private System.Windows.Forms.GroupBox gbTalles;
        private System.Windows.Forms.DataGridView dgvProdTalles;
        private System.Windows.Forms.GroupBox gbProdDatos;
        private System.Windows.Forms.TextBox tbProductoNombre;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button botBorrarProdTalle;
        private System.Windows.Forms.Button botEditarProdTalle;
        private System.Windows.Forms.Button botAgregarProdTalle;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.BindingSource renglonProductoBindingSource;
        private System.Windows.Forms.TextBox tbProductoClave;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button botGuardar;
        private System.Windows.Forms.FlowLayoutPanel flowBotFinalizar;
        private System.Windows.Forms.Button botGuardarCambios;
        private System.Windows.Forms.Button botActualizar;
        private System.Windows.Forms.DataGridViewTextBoxColumn articuloDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tallesDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn precioDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ivaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn totalDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn Talles;
        private System.Windows.Forms.DataGridViewTextBoxColumn Precio;
        private System.Windows.Forms.Panel panelTallesAcciones;
        private System.Windows.Forms.Button botEliminarProducto;
        private System.Windows.Forms.GroupBox gbInsumos;
        private System.Windows.Forms.CheckedListBox chListInsumos;
    }
}
