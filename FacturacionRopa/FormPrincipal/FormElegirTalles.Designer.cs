namespace Formularios
{
    partial class FormElegirTalles
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
            this.labTallesPara = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPrecioVenta = new System.Windows.Forms.TextBox();
            this.botSeleccionarTodo = new System.Windows.Forms.Button();
            this.botQuitarSeleccion = new System.Windows.Forms.Button();
            this.botCancelar = new System.Windows.Forms.Button();
            this.botAceptar = new System.Windows.Forms.Button();
            this.flowTalles = new System.Windows.Forms.FlowLayoutPanel();
            this.labProducto = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labTallesPara
            // 
            this.labTallesPara.AutoSize = true;
            this.labTallesPara.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labTallesPara.Location = new System.Drawing.Point(9, 18);
            this.labTallesPara.Name = "labTallesPara";
            this.labTallesPara.Size = new System.Drawing.Size(134, 20);
            this.labTallesPara.TabIndex = 1;
            this.labTallesPara.Text = "Elegir talles para: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 277);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Precio de venta:";
            // 
            // tbPrecioVenta
            // 
            this.tbPrecioVenta.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbPrecioVenta.Location = new System.Drawing.Point(140, 274);
            this.tbPrecioVenta.Name = "tbPrecioVenta";
            this.tbPrecioVenta.Size = new System.Drawing.Size(186, 26);
            this.tbPrecioVenta.TabIndex = 4;
            // 
            // botSeleccionarTodo
            // 
            this.botSeleccionarTodo.Location = new System.Drawing.Point(13, 230);
            this.botSeleccionarTodo.Name = "botSeleccionarTodo";
            this.botSeleccionarTodo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.botSeleccionarTodo.Size = new System.Drawing.Size(112, 23);
            this.botSeleccionarTodo.TabIndex = 2;
            this.botSeleccionarTodo.Text = "Seleccionar todo";
            this.botSeleccionarTodo.UseVisualStyleBackColor = true;
            this.botSeleccionarTodo.Click += new System.EventHandler(this.botSeleccionarTodo_Click);
            // 
            // botQuitarSeleccion
            // 
            this.botQuitarSeleccion.Location = new System.Drawing.Point(131, 230);
            this.botQuitarSeleccion.Name = "botQuitarSeleccion";
            this.botQuitarSeleccion.Size = new System.Drawing.Size(112, 23);
            this.botQuitarSeleccion.TabIndex = 3;
            this.botQuitarSeleccion.Text = "Quitar selección";
            this.botQuitarSeleccion.UseVisualStyleBackColor = true;
            this.botQuitarSeleccion.Click += new System.EventHandler(this.botQuitarSeleccion_Click);
            // 
            // botCancelar
            // 
            this.botCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCancelar.Location = new System.Drawing.Point(282, 334);
            this.botCancelar.Name = "botCancelar";
            this.botCancelar.Size = new System.Drawing.Size(75, 38);
            this.botCancelar.TabIndex = 6;
            this.botCancelar.Text = "Cancelar";
            this.botCancelar.UseVisualStyleBackColor = true;
            // 
            // botAceptar
            // 
            this.botAceptar.Location = new System.Drawing.Point(363, 334);
            this.botAceptar.Name = "botAceptar";
            this.botAceptar.Size = new System.Drawing.Size(75, 38);
            this.botAceptar.TabIndex = 5;
            this.botAceptar.Text = "Aceptar";
            this.botAceptar.UseVisualStyleBackColor = true;
            this.botAceptar.Click += new System.EventHandler(this.botAceptar_Click);
            // 
            // flowTalles
            // 
            this.flowTalles.AutoScroll = true;
            this.flowTalles.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.flowTalles.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowTalles.Location = new System.Drawing.Point(13, 54);
            this.flowTalles.Name = "flowTalles";
            this.flowTalles.Size = new System.Drawing.Size(434, 170);
            this.flowTalles.TabIndex = 1;
            // 
            // labProducto
            // 
            this.labProducto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labProducto.Location = new System.Drawing.Point(149, 18);
            this.labProducto.Name = "labProducto";
            this.labProducto.Size = new System.Drawing.Size(298, 23);
            this.labProducto.TabIndex = 7;
            this.labProducto.Text = "label1";
            this.labProducto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FormElegirTalles
            // 
            this.AcceptButton = this.botAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.botCancelar;
            this.ClientSize = new System.Drawing.Size(461, 384);
            this.ControlBox = false;
            this.Controls.Add(this.labProducto);
            this.Controls.Add(this.flowTalles);
            this.Controls.Add(this.botAceptar);
            this.Controls.Add(this.botCancelar);
            this.Controls.Add(this.botQuitarSeleccion);
            this.Controls.Add(this.botSeleccionarTodo);
            this.Controls.Add(this.tbPrecioVenta);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labTallesPara);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormElegirTalles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Elegir talles";
            this.Load += new System.EventHandler(this.FormElegirTalles_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labTallesPara;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPrecioVenta;
        private System.Windows.Forms.Button botSeleccionarTodo;
        private System.Windows.Forms.Button botQuitarSeleccion;
        private System.Windows.Forms.Button botCancelar;
        private System.Windows.Forms.Button botAceptar;
        private System.Windows.Forms.FlowLayoutPanel flowTalles;
        private System.Windows.Forms.Label labProducto;
    }
}