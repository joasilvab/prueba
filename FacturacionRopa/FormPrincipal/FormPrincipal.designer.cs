namespace Formularios
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowContenido = new System.Windows.Forms.FlowLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.productosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administraciónProductosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tallesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.confeccionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facturaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tsslabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cerrarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.talleresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.administrarTalleresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insumosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowContenido
            // 
            this.flowContenido.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.flowContenido.Location = new System.Drawing.Point(0, 24);
            this.flowContenido.Name = "flowContenido";
            this.flowContenido.Size = new System.Drawing.Size(839, 383);
            this.flowContenido.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.productosToolStripMenuItem,
            this.talleresToolStripMenuItem,
            this.confeccionesToolStripMenuItem,
            this.clientesToolStripMenuItem,
            this.facturaciónToolStripMenuItem,
            this.cerrarSesiónToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(839, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // productosToolStripMenuItem
            // 
            this.productosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administraciónProductosToolStripMenuItem,
            this.tallesToolStripMenuItem,
            this.insumosToolStripMenuItem});
            this.productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            this.productosToolStripMenuItem.Size = new System.Drawing.Size(73, 20);
            this.productosToolStripMenuItem.Text = "Productos";
            // 
            // administraciónProductosToolStripMenuItem
            // 
            this.administraciónProductosToolStripMenuItem.Name = "administraciónProductosToolStripMenuItem";
            this.administraciónProductosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.administraciónProductosToolStripMenuItem.Text = "Administrar productos";
            this.administraciónProductosToolStripMenuItem.Click += new System.EventHandler(this.administraciónProductosToolStripMenuItem_Click);
            // 
            // tallesToolStripMenuItem
            // 
            this.tallesToolStripMenuItem.Name = "tallesToolStripMenuItem";
            this.tallesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.tallesToolStripMenuItem.Text = "Talles...";
            this.tallesToolStripMenuItem.Click += new System.EventHandler(this.tallesToolStripMenuItem_Click);
            // 
            // confeccionesToolStripMenuItem
            // 
            this.confeccionesToolStripMenuItem.Name = "confeccionesToolStripMenuItem";
            this.confeccionesToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
            this.confeccionesToolStripMenuItem.Text = "Confecciones";
            // 
            // facturaciónToolStripMenuItem
            // 
            this.facturaciónToolStripMenuItem.Name = "facturaciónToolStripMenuItem";
            this.facturaciónToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.facturaciónToolStripMenuItem.Text = "Facturación";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 410);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(839, 22);
            this.statusStrip1.TabIndex = 8;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tsslabel
            // 
            this.tsslabel.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.tsslabel.Name = "tsslabel";
            this.tsslabel.Size = new System.Drawing.Size(127, 17);
            this.tsslabel.Text = "toolStripStatusLabel1";
            // 
            // cerrarSesiónToolStripMenuItem
            // 
            this.cerrarSesiónToolStripMenuItem.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.cerrarSesiónToolStripMenuItem.Name = "cerrarSesiónToolStripMenuItem";
            this.cerrarSesiónToolStripMenuItem.Size = new System.Drawing.Size(87, 20);
            this.cerrarSesiónToolStripMenuItem.Text = "Cerrar sesión";
            this.cerrarSesiónToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesiónToolStripMenuItem_Click);
            // 
            // talleresToolStripMenuItem
            // 
            this.talleresToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.administrarTalleresToolStripMenuItem});
            this.talleresToolStripMenuItem.Name = "talleresToolStripMenuItem";
            this.talleresToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.talleresToolStripMenuItem.Text = "Talleres";
            // 
            // administrarTalleresToolStripMenuItem
            // 
            this.administrarTalleresToolStripMenuItem.Name = "administrarTalleresToolStripMenuItem";
            this.administrarTalleresToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.administrarTalleresToolStripMenuItem.Text = "Administrar talleres";
            // 
            // insumosToolStripMenuItem
            // 
            this.insumosToolStripMenuItem.Name = "insumosToolStripMenuItem";
            this.insumosToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.insumosToolStripMenuItem.Text = "Insumos...";
            this.insumosToolStripMenuItem.Click += new System.EventHandler(this.insumosToolStripMenuItem_Click);
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(839, 432);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.flowContenido);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Administración Latino";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.DataGridViewTextBoxColumn nroDocDataGridViewTextBoxColumn;
        //private System.Windows.Forms.DataGridViewTextBoxColumn tipoDIDataGridViewTextBoxColumn;
        private System.Windows.Forms.FlowLayoutPanel flowContenido;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tsslabel;
        private System.Windows.Forms.ToolStripMenuItem productosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administraciónProductosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem confeccionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facturaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tallesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem talleresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem administrarTalleresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insumosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
    }
}

