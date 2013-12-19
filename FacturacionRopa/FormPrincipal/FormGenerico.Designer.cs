using Modelo;

namespace Formularios
{
    partial class FormGenerico<T> where T: ElementoBase
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
            this.botAgregar = new System.Windows.Forms.Button();
            this.botModificar = new System.Windows.Forms.Button();
            this.botBorrar = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.botCerrar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // botAgregar
            // 
            this.botAgregar.Location = new System.Drawing.Point(12, 18);
            this.botAgregar.Name = "botAgregar";
            this.botAgregar.Size = new System.Drawing.Size(90, 37);
            this.botAgregar.TabIndex = 0;
            this.botAgregar.Text = "Agregar...";
            this.botAgregar.UseVisualStyleBackColor = true;
            this.botAgregar.Click += new System.EventHandler(this.botAgregar_Click);
            // 
            // botModificar
            // 
            this.botModificar.Location = new System.Drawing.Point(12, 61);
            this.botModificar.Name = "botModificar";
            this.botModificar.Size = new System.Drawing.Size(90, 37);
            this.botModificar.TabIndex = 1;
            this.botModificar.Text = "Modificar seleccionado";
            this.botModificar.UseVisualStyleBackColor = true;
            this.botModificar.Click += new System.EventHandler(this.botModificar_Click);
            // 
            // botBorrar
            // 
            this.botBorrar.Location = new System.Drawing.Point(12, 104);
            this.botBorrar.Name = "botBorrar";
            this.botBorrar.Size = new System.Drawing.Size(90, 37);
            this.botBorrar.TabIndex = 2;
            this.botBorrar.Text = "Borrar seleccionado";
            this.botBorrar.UseVisualStyleBackColor = true;
            this.botBorrar.Click += new System.EventHandler(this.botBorrar_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(113, 18);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(159, 232);
            this.dataGridView1.TabIndex = 3;
            // 
            // botCerrar
            // 
            this.botCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCerrar.Location = new System.Drawing.Point(12, 213);
            this.botCerrar.Name = "botCerrar";
            this.botCerrar.Size = new System.Drawing.Size(90, 37);
            this.botCerrar.TabIndex = 4;
            this.botCerrar.Text = "Cerrar (Esc)";
            this.botCerrar.UseVisualStyleBackColor = true;
            // 
            // FormGenerico
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.botCerrar;
            this.ClientSize = new System.Drawing.Size(285, 262);
            this.ControlBox = false;
            this.Controls.Add(this.botCerrar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.botBorrar);
            this.Controls.Add(this.botModificar);
            this.Controls.Add(this.botAgregar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormGenerico";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.FormTalles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button botAgregar;
        private System.Windows.Forms.Button botModificar;
        private System.Windows.Forms.Button botBorrar;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button botCerrar;
    }
}