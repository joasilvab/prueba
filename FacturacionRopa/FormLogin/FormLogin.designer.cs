namespace Formularios
{
    partial class FormLogin
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
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.tbContrasena = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.botIngresar = new System.Windows.Forms.Button();
            this.botCerrar = new System.Windows.Forms.Button();
            this.tbProbarConex = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbUsuario
            // 
            this.tbUsuario.Location = new System.Drawing.Point(92, 16);
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.Size = new System.Drawing.Size(153, 20);
            this.tbUsuario.TabIndex = 0;
            this.tbUsuario.Text = "yamil";
            // 
            // tbContrasena
            // 
            this.tbContrasena.Location = new System.Drawing.Point(92, 56);
            this.tbContrasena.Name = "tbContrasena";
            this.tbContrasena.PasswordChar = '*';
            this.tbContrasena.Size = new System.Drawing.Size(153, 20);
            this.tbContrasena.TabIndex = 1;
            this.tbContrasena.Text = "yamil";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Usuario:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Contraseña:";
            // 
            // botIngresar
            // 
            this.botIngresar.Location = new System.Drawing.Point(174, 107);
            this.botIngresar.Name = "botIngresar";
            this.botIngresar.Size = new System.Drawing.Size(75, 34);
            this.botIngresar.TabIndex = 5;
            this.botIngresar.Text = "Ingresar";
            this.botIngresar.UseVisualStyleBackColor = true;
            this.botIngresar.Click += new System.EventHandler(this.botIngresar_Click);
            // 
            // botCerrar
            // 
            this.botCerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.botCerrar.Location = new System.Drawing.Point(12, 107);
            this.botCerrar.Name = "botCerrar";
            this.botCerrar.Size = new System.Drawing.Size(75, 34);
            this.botCerrar.TabIndex = 6;
            this.botCerrar.Text = "Cerrar";
            this.botCerrar.UseVisualStyleBackColor = true;
            // 
            // tbProbarConex
            // 
            this.tbProbarConex.Location = new System.Drawing.Point(93, 107);
            this.tbProbarConex.Name = "tbProbarConex";
            this.tbProbarConex.Size = new System.Drawing.Size(75, 34);
            this.tbProbarConex.TabIndex = 9;
            this.tbProbarConex.Text = "Cambiar conexión...";
            this.tbProbarConex.UseVisualStyleBackColor = true;
            this.tbProbarConex.Click += new System.EventHandler(this.tbProbarConex_Click);
            // 
            // FormLogin
            // 
            this.AcceptButton = this.botIngresar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.botCerrar;
            this.ClientSize = new System.Drawing.Size(257, 151);
            this.Controls.Add(this.tbProbarConex);
            this.Controls.Add(this.botCerrar);
            this.Controls.Add(this.botIngresar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbContrasena);
            this.Controls.Add(this.tbUsuario);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.TextBox tbContrasena;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button botIngresar;
        private System.Windows.Forms.Button botCerrar;
        private System.Windows.Forms.Button tbProbarConex;
    }
}

