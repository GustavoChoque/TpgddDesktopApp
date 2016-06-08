namespace WindowsFormsApplication1.ABM_Usuario
{
    partial class abmUsuarioPrincipal
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
            this.butCrear = new System.Windows.Forms.Button();
            this.butBajaUsuario = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.butVolver = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // butCrear
            // 
            this.butCrear.Location = new System.Drawing.Point(34, 33);
            this.butCrear.Name = "butCrear";
            this.butCrear.Size = new System.Drawing.Size(126, 32);
            this.butCrear.TabIndex = 0;
            this.butCrear.Text = "Crear Usuario";
            this.butCrear.UseVisualStyleBackColor = true;
            this.butCrear.Click += new System.EventHandler(this.butCrear_Click);
            // 
            // butBajaUsuario
            // 
            this.butBajaUsuario.Location = new System.Drawing.Point(34, 73);
            this.butBajaUsuario.Name = "butBajaUsuario";
            this.butBajaUsuario.Size = new System.Drawing.Size(126, 32);
            this.butBajaUsuario.TabIndex = 1;
            this.butBajaUsuario.Text = "Baja Usuario";
            this.butBajaUsuario.UseVisualStyleBackColor = true;
            this.butBajaUsuario.Click += new System.EventHandler(this.butBajaUsuario_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(34, 113);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(126, 32);
            this.button3.TabIndex = 2;
            this.button3.Text = "Modificacion Usuario";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // butVolver
            // 
            this.butVolver.Location = new System.Drawing.Point(223, 159);
            this.butVolver.Name = "butVolver";
            this.butVolver.Size = new System.Drawing.Size(93, 26);
            this.butVolver.TabIndex = 3;
            this.butVolver.Text = "Volver";
            this.butVolver.UseVisualStyleBackColor = true;
            this.butVolver.Click += new System.EventHandler(this.butVolver_Click);
            // 
            // abmUsuarioPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(328, 197);
            this.Controls.Add(this.butVolver);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.butBajaUsuario);
            this.Controls.Add(this.butCrear);
            this.Name = "abmUsuarioPrincipal";
            this.Text = "ABM Usuario - Sólo Administrador";
            this.Load += new System.EventHandler(this.abmUsuarioPrincipal_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butCrear;
        private System.Windows.Forms.Button butBajaUsuario;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button butVolver;
    }
}