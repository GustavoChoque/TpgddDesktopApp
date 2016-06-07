namespace WindowsFormsApplication1.Generar_Publicación
{
    partial class Form2
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
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fecVenc = new System.Windows.Forms.TextBox();
            this.fecCrea = new System.Windows.Forms.TextBox();
            this.estado = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.stock = new System.Windows.Forms.TextBox();
            this.rubro = new System.Windows.Forms.ComboBox();
            this.precio = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.tipoVisib = new System.Windows.Forms.ComboBox();
            this.tipo = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(309, 400);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(13, 400);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Activar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fecVenc);
            this.groupBox1.Controls.Add(this.fecCrea);
            this.groupBox1.Controls.Add(this.estado);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.stock);
            this.groupBox1.Controls.Add(this.rubro);
            this.groupBox1.Controls.Add(this.precio);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.tipoVisib);
            this.groupBox1.Controls.Add(this.tipo);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(372, 382);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de la publicacion";
            // 
            // fecVenc
            // 
            this.fecVenc.Enabled = false;
            this.fecVenc.Location = new System.Drawing.Point(128, 347);
            this.fecVenc.Name = "fecVenc";
            this.fecVenc.Size = new System.Drawing.Size(120, 20);
            this.fecVenc.TabIndex = 23;
            this.fecVenc.TextChanged += new System.EventHandler(this.fecVenc_TextChanged);
            // 
            // fecCrea
            // 
            this.fecCrea.Enabled = false;
            this.fecCrea.Location = new System.Drawing.Point(128, 321);
            this.fecCrea.Name = "fecCrea";
            this.fecCrea.Size = new System.Drawing.Size(121, 20);
            this.fecCrea.TabIndex = 22;
            // 
            // estado
            // 
            this.estado.Location = new System.Drawing.Point(128, 295);
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(121, 20);
            this.estado.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 347);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(101, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Fecha Vencimiento:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 321);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Fecha Creacion:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 18;
            this.label3.Text = "Estado:";
            // 
            // stock
            // 
            this.stock.Location = new System.Drawing.Point(128, 160);
            this.stock.Name = "stock";
            this.stock.Size = new System.Drawing.Size(121, 20);
            this.stock.TabIndex = 17;
            // 
            // rubro
            // 
            this.rubro.FormattingEnabled = true;
            this.rubro.Location = new System.Drawing.Point(128, 269);
            this.rubro.Name = "rubro";
            this.rubro.Size = new System.Drawing.Size(121, 21);
            this.rubro.TabIndex = 16;
            // 
            // precio
            // 
            this.precio.Location = new System.Drawing.Point(128, 189);
            this.precio.Name = "precio";
            this.precio.Size = new System.Drawing.Size(120, 20);
            this.precio.TabIndex = 15;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 189);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(37, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Precio";
            // 
            // tipoVisib
            // 
            this.tipoVisib.FormattingEnabled = true;
            this.tipoVisib.Location = new System.Drawing.Point(128, 242);
            this.tipoVisib.Name = "tipoVisib";
            this.tipoVisib.Size = new System.Drawing.Size(121, 21);
            this.tipoVisib.TabIndex = 12;
            // 
            // tipo
            // 
            this.tipo.FormattingEnabled = true;
            this.tipo.Location = new System.Drawing.Point(128, 215);
            this.tipo.Name = "tipo";
            this.tipo.Size = new System.Drawing.Size(121, 21);
            this.tipo.TabIndex = 11;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(25, 54);
            this.richTextBox1.MaxLength = 256;
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(341, 96);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 269);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Rubros:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Tipo de visibilidad:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 215);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Tipo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Stock:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Descripción:";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(157, 400);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 23);
            this.button3.TabIndex = 6;
            this.button3.Text = "Modificar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(395, 436);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form2";
            this.Text = "Vista previa publicacion";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox stock;
        private System.Windows.Forms.ComboBox rubro;
        private System.Windows.Forms.TextBox precio;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox tipoVisib;
        private System.Windows.Forms.ComboBox tipo;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fecVenc;
        private System.Windows.Forms.TextBox fecCrea;
        private System.Windows.Forms.TextBox estado;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
    }
}