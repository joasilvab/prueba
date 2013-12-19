namespace Formularios
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.botVolver = new System.Windows.Forms.Button();
            this.tbPtoVta = new System.Windows.Forms.TextBox();
            this.tbPtoVtaDesc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(112, 84);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(109, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "programador";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(112, 124);
            this.textBox2.Name = "textBox2";
            this.textBox2.PasswordChar = '*';
            this.textBox2.Size = new System.Drawing.Size(109, 20);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "programador";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(146, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Entrar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Id:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 127);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Contraseña:";
            // 
            // botVolver
            // 
            this.botVolver.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botVolver.Location = new System.Drawing.Point(56, 180);
            this.botVolver.Name = "botVolver";
            this.botVolver.Size = new System.Drawing.Size(75, 23);
            this.botVolver.TabIndex = 5;
            this.botVolver.Text = "Volver";
            this.botVolver.UseVisualStyleBackColor = true;
            this.botVolver.Click += new System.EventHandler(this.botVolver_Click);
            // 
            // tbPtoVta
            // 
            this.tbPtoVta.Location = new System.Drawing.Point(112, 21);
            this.tbPtoVta.Name = "tbPtoVta";
            this.tbPtoVta.Size = new System.Drawing.Size(109, 20);
            this.tbPtoVta.TabIndex = 12;
            this.tbPtoVta.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.tbPtoVta_HelpRequested);
            this.tbPtoVta.Leave += new System.EventHandler(this.tbPtoVta_Leave);
            // 
            // tbPtoVtaDesc
            // 
            this.tbPtoVtaDesc.Location = new System.Drawing.Point(35, 51);
            this.tbPtoVtaDesc.Name = "tbPtoVtaDesc";
            this.tbPtoVtaDesc.ReadOnly = true;
            this.tbPtoVtaDesc.Size = new System.Drawing.Size(186, 20);
            this.tbPtoVtaDesc.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Pto. de venta:";
            // 
            // Form1
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.botVolver;
            this.ClientSize = new System.Drawing.Size(261, 228);
            this.Controls.Add(this.tbPtoVta);
            this.Controls.Add(this.tbPtoVtaDesc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.botVolver);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button botVolver;
        private System.Windows.Forms.TextBox tbPtoVta;
        private System.Windows.Forms.TextBox tbPtoVtaDesc;
        private System.Windows.Forms.Label label3;
    }
}

