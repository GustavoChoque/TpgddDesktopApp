namespace WindowsFormsApplication1.ABM_Usuario
{
    partial class crearUsuario
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
            this.textBoxNuevoUsuario = new System.Windows.Forms.TextBox();
            this.textBoxNuevoUsuarioPassw = new System.Windows.Forms.TextBox();
            this.labNuevoUsuario = new System.Windows.Forms.Label();
            this.labNuevoUsuarioPassw = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.radioButCliente = new System.Windows.Forms.RadioButton();
            this.radioButEmpresa = new System.Windows.Forms.RadioButton();
            this.butContinuar = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxNuevoUsuario
            // 
            this.textBoxNuevoUsuario.Location = new System.Drawing.Point(211, 21);
            this.textBoxNuevoUsuario.Name = "textBoxNuevoUsuario";
            this.textBoxNuevoUsuario.Size = new System.Drawing.Size(100, 20);
            this.textBoxNuevoUsuario.TabIndex = 0;
            // 
            // textBoxNuevoUsuarioPassw
            // 
            this.textBoxNuevoUsuarioPassw.Location = new System.Drawing.Point(211, 47);
            this.textBoxNuevoUsuarioPassw.Name = "textBoxNuevoUsuarioPassw";
            this.textBoxNuevoUsuarioPassw.PasswordChar = '.';
            this.textBoxNuevoUsuarioPassw.Size = new System.Drawing.Size(100, 20);
            this.textBoxNuevoUsuarioPassw.TabIndex = 1;
            this.textBoxNuevoUsuarioPassw.UseSystemPasswordChar = true;
            // 
            // labNuevoUsuario
            // 
            this.labNuevoUsuario.AutoSize = true;
            this.labNuevoUsuario.Location = new System.Drawing.Point(42, 24);
            this.labNuevoUsuario.Name = "labNuevoUsuario";
            this.labNuevoUsuario.Size = new System.Drawing.Size(89, 13);
            this.labNuevoUsuario.TabIndex = 2;
            this.labNuevoUsuario.Text = "Id nuevo usuario:";
            // 
            // labNuevoUsuarioPassw
            // 
            this.labNuevoUsuarioPassw.AutoSize = true;
            this.labNuevoUsuarioPassw.Location = new System.Drawing.Point(42, 50);
            this.labNuevoUsuarioPassw.Name = "labNuevoUsuarioPassw";
            this.labNuevoUsuarioPassw.Size = new System.Drawing.Size(151, 13);
            this.labNuevoUsuarioPassw.TabIndex = 3;
            this.labNuevoUsuarioPassw.Text = "Contraseña del nuevo usuario:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(121, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Rol a asignar:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // radioButCliente
            // 
            this.radioButCliente.AutoSize = true;
            this.radioButCliente.Location = new System.Drawing.Point(211, 84);
            this.radioButCliente.Name = "radioButCliente";
            this.radioButCliente.Size = new System.Drawing.Size(57, 17);
            this.radioButCliente.TabIndex = 9;
            this.radioButCliente.TabStop = true;
            this.radioButCliente.Text = "Cliente";
            this.radioButCliente.UseVisualStyleBackColor = true;
            this.radioButCliente.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButEmpresa
            // 
            this.radioButEmpresa.AutoSize = true;
            this.radioButEmpresa.Location = new System.Drawing.Point(211, 107);
            this.radioButEmpresa.Name = "radioButEmpresa";
            this.radioButEmpresa.Size = new System.Drawing.Size(66, 17);
            this.radioButEmpresa.TabIndex = 10;
            this.radioButEmpresa.TabStop = true;
            this.radioButEmpresa.Text = "Empresa";
            this.radioButEmpresa.UseVisualStyleBackColor = true;
            this.radioButEmpresa.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // butContinuar
            // 
            this.butContinuar.Location = new System.Drawing.Point(100, 145);
            this.butContinuar.Name = "butContinuar";
            this.butContinuar.Size = new System.Drawing.Size(75, 23);
            this.butContinuar.TabIndex = 11;
            this.butContinuar.Text = "Continuar";
            this.butContinuar.UseVisualStyleBackColor = true;
            this.butContinuar.Click += new System.EventHandler(this.butContinuar_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(211, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 12;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // crearUsuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(383, 180);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.butContinuar);
            this.Controls.Add(this.radioButEmpresa);
            this.Controls.Add(this.radioButCliente);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labNuevoUsuarioPassw);
            this.Controls.Add(this.labNuevoUsuario);
            this.Controls.Add(this.textBoxNuevoUsuarioPassw);
            this.Controls.Add(this.textBoxNuevoUsuario);
            this.Name = "crearUsuario";
            this.Text = "ABM Usuario - Crear Usuario";
            this.Load += new System.EventHandler(this.crearUsuario_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNuevoUsuario;
        private System.Windows.Forms.TextBox textBoxNuevoUsuarioPassw;
        private System.Windows.Forms.Label labNuevoUsuario;
        private System.Windows.Forms.Label labNuevoUsuarioPassw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButCliente;
        private System.Windows.Forms.RadioButton radioButEmpresa;
        private System.Windows.Forms.Button butContinuar;
        private System.Windows.Forms.Button button2;
    }
}