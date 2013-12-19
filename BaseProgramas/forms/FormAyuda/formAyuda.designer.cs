namespace Formularios
{
    //partial class formAyuda
    public partial class formAyuda : System.Windows.Forms.Form
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.botAceptar = new System.Windows.Forms.Button();
            this.botCerrar = new System.Windows.Forms.Button();
            this.lbDatos = new System.Windows.Forms.ListBox();
            this.tbBuscar = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // botAceptar
            // 
            this.botAceptar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.botAceptar.Location = new System.Drawing.Point(12, 231);
            this.botAceptar.Name = "botAceptar";
            this.botAceptar.Size = new System.Drawing.Size(97, 23);
            this.botAceptar.TabIndex = 3;
            this.botAceptar.Text = "Aceptar (Enter)";
            this.botAceptar.UseVisualStyleBackColor = true;
            this.botAceptar.Click += new System.EventHandler(this.botAceptar_Click);
            // 
            // botCerrar
            // 
            this.botCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.botCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCerrar.Location = new System.Drawing.Point(175, 231);
            this.botCerrar.Name = "botCerrar";
            this.botCerrar.Size = new System.Drawing.Size(97, 23);
            this.botCerrar.TabIndex = 4;
            this.botCerrar.Text = "Cerrar (Esc)";
            this.botCerrar.UseVisualStyleBackColor = true;
            // 
            // lbDatos
            // 
            this.lbDatos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbDatos.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.lbDatos.FormattingEnabled = true;
            this.lbDatos.HorizontalScrollbar = true;
            this.lbDatos.Location = new System.Drawing.Point(12, 39);
            this.lbDatos.Name = "lbDatos";
            this.lbDatos.Size = new System.Drawing.Size(260, 186);
            this.lbDatos.TabIndex = 2;
            this.lbDatos.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lbDatos_DrawItem);
            this.lbDatos.SelectedIndexChanged += new System.EventHandler(this.lbDatos_SelectedIndexChanged);
            this.lbDatos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbBuscar_KeyDown);
            this.lbDatos.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lbDatos_MouseDoubleClick);
            // 
            // tbBuscar
            // 
            this.tbBuscar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBuscar.Location = new System.Drawing.Point(12, 18);
            this.tbBuscar.Name = "tbBuscar";
            this.tbBuscar.Size = new System.Drawing.Size(260, 20);
            this.tbBuscar.TabIndex = 1;
            this.tbBuscar.TextChanged += new System.EventHandler(this.tbBuscar_TextChanged);
            this.tbBuscar.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbBuscar_KeyDown);
            // 
            // formAyuda
            // 
            this.AcceptButton = this.botAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.botCerrar;
            this.ClientSize = new System.Drawing.Size(284, 266);
            this.Controls.Add(this.tbBuscar);
            this.Controls.Add(this.lbDatos);
            this.Controls.Add(this.botCerrar);
            this.Controls.Add(this.botAceptar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "formAyuda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayuda";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button botAceptar;
        private System.Windows.Forms.Button botCerrar;
        private System.Windows.Forms.ListBox lbDatos;
        private System.Windows.Forms.TextBox tbBuscar;
    }
}