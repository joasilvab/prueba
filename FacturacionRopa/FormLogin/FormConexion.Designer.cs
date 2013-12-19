namespace Formularios
{
    partial class FormConexion
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tbServer = new System.Windows.Forms.TextBox();
            this.tbDatabase = new System.Windows.Forms.TextBox();
            this.tbUser = new System.Windows.Forms.TextBox();
            this.tbPass = new System.Windows.Forms.TextBox();
            this.tbCancelar = new System.Windows.Forms.Button();
            this.tbAceptar = new System.Windows.Forms.Button();
            this.tbProbar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Server:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Database:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "User:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(33, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Pass:";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(139, 24);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(100, 20);
            this.tbServer.TabIndex = 4;
            // 
            // tbDatabase
            // 
            this.tbDatabase.Location = new System.Drawing.Point(139, 50);
            this.tbDatabase.Name = "tbDatabase";
            this.tbDatabase.Size = new System.Drawing.Size(100, 20);
            this.tbDatabase.TabIndex = 5;
            // 
            // tbUser
            // 
            this.tbUser.Location = new System.Drawing.Point(139, 76);
            this.tbUser.Name = "tbUser";
            this.tbUser.Size = new System.Drawing.Size(100, 20);
            this.tbUser.TabIndex = 6;
            // 
            // tbPass
            // 
            this.tbPass.Location = new System.Drawing.Point(139, 102);
            this.tbPass.Name = "tbPass";
            this.tbPass.Size = new System.Drawing.Size(100, 20);
            this.tbPass.TabIndex = 7;
            // 
            // tbCancelar
            // 
            this.tbCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.tbCancelar.Location = new System.Drawing.Point(27, 172);
            this.tbCancelar.Name = "tbCancelar";
            this.tbCancelar.Size = new System.Drawing.Size(75, 23);
            this.tbCancelar.TabIndex = 8;
            this.tbCancelar.Text = "Cancelar";
            this.tbCancelar.UseVisualStyleBackColor = true;
            // 
            // tbAceptar
            // 
            this.tbAceptar.Location = new System.Drawing.Point(164, 172);
            this.tbAceptar.Name = "tbAceptar";
            this.tbAceptar.Size = new System.Drawing.Size(75, 23);
            this.tbAceptar.TabIndex = 9;
            this.tbAceptar.Text = "Aceptar";
            this.tbAceptar.UseVisualStyleBackColor = true;
            this.tbAceptar.Click += new System.EventHandler(this.botAceptar_Click);
            // 
            // tbProbar
            // 
            this.tbProbar.Location = new System.Drawing.Point(164, 128);
            this.tbProbar.Name = "tbProbar";
            this.tbProbar.Size = new System.Drawing.Size(75, 23);
            this.tbProbar.TabIndex = 10;
            this.tbProbar.Text = "Probar";
            this.tbProbar.UseVisualStyleBackColor = true;
            this.tbProbar.Click += new System.EventHandler(this.tbProbar_Click);
            // 
            // FormConexion
            // 
            this.AcceptButton = this.tbAceptar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.tbCancelar;
            this.ClientSize = new System.Drawing.Size(270, 204);
            this.ControlBox = false;
            this.Controls.Add(this.tbProbar);
            this.Controls.Add(this.tbAceptar);
            this.Controls.Add(this.tbCancelar);
            this.Controls.Add(this.tbPass);
            this.Controls.Add(this.tbUser);
            this.Controls.Add(this.tbDatabase);
            this.Controls.Add(this.tbServer);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormConexion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cambiar conexión";
            this.Load += new System.EventHandler(this.FormConexion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.TextBox tbDatabase;
        private System.Windows.Forms.TextBox tbUser;
        private System.Windows.Forms.TextBox tbPass;
        private System.Windows.Forms.Button tbCancelar;
        private System.Windows.Forms.Button tbAceptar;
        private System.Windows.Forms.Button tbProbar;
    }
}