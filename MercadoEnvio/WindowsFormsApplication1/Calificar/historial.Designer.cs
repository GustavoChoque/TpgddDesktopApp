namespace WindowsFormsApplication1.Calificar
{
    partial class historial
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxComprasTotales = new System.Windows.Forms.TextBox();
            this.textBoxCompras5est = new System.Windows.Forms.TextBox();
            this.textBoxCompras4est = new System.Windows.Forms.TextBox();
            this.textBoxCompras3est = new System.Windows.Forms.TextBox();
            this.textBoxCompras2est = new System.Windows.Forms.TextBox();
            this.textBoxCompras1est = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxSubTotales = new System.Windows.Forms.TextBox();
            this.textBoxSub5est = new System.Windows.Forms.TextBox();
            this.textBoxSub4est = new System.Windows.Forms.TextBox();
            this.textBoxSub3est = new System.Windows.Forms.TextBox();
            this.textBoxSub2est = new System.Windows.Forms.TextBox();
            this.textBoxSub1est = new System.Windows.Forms.TextBox();
            this.OK = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Últimas 5 operaciones calificadas:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(15, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(515, 161);
            this.dataGridView1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 215);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Compras totales (calificadas):";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 262);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Calificaciones dadas:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 369);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "2 estrellas:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(30, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "5 estrellas:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 395);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(52, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "1 estrella:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(30, 317);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "4 estrellas:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 343);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "3 estrellas:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // textBoxComprasTotales
            // 
            this.textBoxComprasTotales.Location = new System.Drawing.Point(101, 234);
            this.textBoxComprasTotales.Name = "textBoxComprasTotales";
            this.textBoxComprasTotales.Size = new System.Drawing.Size(52, 20);
            this.textBoxComprasTotales.TabIndex = 9;
            // 
            // textBoxCompras5est
            // 
            this.textBoxCompras5est.Location = new System.Drawing.Point(121, 288);
            this.textBoxCompras5est.Name = "textBoxCompras5est";
            this.textBoxCompras5est.Size = new System.Drawing.Size(65, 20);
            this.textBoxCompras5est.TabIndex = 10;
            // 
            // textBoxCompras4est
            // 
            this.textBoxCompras4est.Location = new System.Drawing.Point(121, 314);
            this.textBoxCompras4est.Name = "textBoxCompras4est";
            this.textBoxCompras4est.Size = new System.Drawing.Size(65, 20);
            this.textBoxCompras4est.TabIndex = 11;
            // 
            // textBoxCompras3est
            // 
            this.textBoxCompras3est.Location = new System.Drawing.Point(121, 340);
            this.textBoxCompras3est.Name = "textBoxCompras3est";
            this.textBoxCompras3est.Size = new System.Drawing.Size(65, 20);
            this.textBoxCompras3est.TabIndex = 12;
            // 
            // textBoxCompras2est
            // 
            this.textBoxCompras2est.Location = new System.Drawing.Point(121, 366);
            this.textBoxCompras2est.Name = "textBoxCompras2est";
            this.textBoxCompras2est.Size = new System.Drawing.Size(65, 20);
            this.textBoxCompras2est.TabIndex = 13;
            // 
            // textBoxCompras1est
            // 
            this.textBoxCompras1est.Location = new System.Drawing.Point(121, 392);
            this.textBoxCompras1est.Name = "textBoxCompras1est";
            this.textBoxCompras1est.Size = new System.Drawing.Size(65, 20);
            this.textBoxCompras1est.TabIndex = 14;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(272, 215);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(191, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Subastas totales ganadas (calificadas):";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(272, 262);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(107, 13);
            this.label10.TabIndex = 16;
            this.label10.Text = "Calificaciones dadas:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(309, 291);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(57, 13);
            this.label11.TabIndex = 17;
            this.label11.Text = "5 estrellas:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(309, 343);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 18;
            this.label12.Text = "3 estrellas:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(309, 317);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(57, 13);
            this.label13.TabIndex = 19;
            this.label13.Text = "4 estrellas:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(309, 369);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(57, 13);
            this.label14.TabIndex = 20;
            this.label14.Text = "2 estrellas:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(309, 395);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(52, 13);
            this.label15.TabIndex = 21;
            this.label15.Text = "1 estrella:";
            // 
            // textBoxSubTotales
            // 
            this.textBoxSubTotales.Location = new System.Drawing.Point(397, 234);
            this.textBoxSubTotales.Name = "textBoxSubTotales";
            this.textBoxSubTotales.Size = new System.Drawing.Size(55, 20);
            this.textBoxSubTotales.TabIndex = 22;
            // 
            // textBoxSub5est
            // 
            this.textBoxSub5est.Location = new System.Drawing.Point(421, 288);
            this.textBoxSub5est.Name = "textBoxSub5est";
            this.textBoxSub5est.Size = new System.Drawing.Size(65, 20);
            this.textBoxSub5est.TabIndex = 23;
            // 
            // textBoxSub4est
            // 
            this.textBoxSub4est.Location = new System.Drawing.Point(421, 314);
            this.textBoxSub4est.Name = "textBoxSub4est";
            this.textBoxSub4est.Size = new System.Drawing.Size(65, 20);
            this.textBoxSub4est.TabIndex = 24;
            // 
            // textBoxSub3est
            // 
            this.textBoxSub3est.Location = new System.Drawing.Point(421, 340);
            this.textBoxSub3est.Name = "textBoxSub3est";
            this.textBoxSub3est.Size = new System.Drawing.Size(65, 20);
            this.textBoxSub3est.TabIndex = 25;
            // 
            // textBoxSub2est
            // 
            this.textBoxSub2est.Location = new System.Drawing.Point(421, 366);
            this.textBoxSub2est.Name = "textBoxSub2est";
            this.textBoxSub2est.Size = new System.Drawing.Size(65, 20);
            this.textBoxSub2est.TabIndex = 26;
            // 
            // textBoxSub1est
            // 
            this.textBoxSub1est.Location = new System.Drawing.Point(421, 392);
            this.textBoxSub1est.Name = "textBoxSub1est";
            this.textBoxSub1est.Size = new System.Drawing.Size(65, 20);
            this.textBoxSub1est.TabIndex = 27;
            // 
            // OK
            // 
            this.OK.Location = new System.Drawing.Point(240, 432);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 28;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            this.OK.Click += new System.EventHandler(this.OK_Click);
            // 
            // historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(542, 478);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.textBoxSub1est);
            this.Controls.Add(this.textBoxSub2est);
            this.Controls.Add(this.textBoxSub3est);
            this.Controls.Add(this.textBoxSub4est);
            this.Controls.Add(this.textBoxSub5est);
            this.Controls.Add(this.textBoxSubTotales);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBoxCompras1est);
            this.Controls.Add(this.textBoxCompras2est);
            this.Controls.Add(this.textBoxCompras3est);
            this.Controls.Add(this.textBoxCompras4est);
            this.Controls.Add(this.textBoxCompras5est);
            this.Controls.Add(this.textBoxComprasTotales);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Name = "historial";
            this.Text = "Historial";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxComprasTotales;
        private System.Windows.Forms.TextBox textBoxCompras5est;
        private System.Windows.Forms.TextBox textBoxCompras4est;
        private System.Windows.Forms.TextBox textBoxCompras3est;
        private System.Windows.Forms.TextBox textBoxCompras2est;
        private System.Windows.Forms.TextBox textBoxCompras1est;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBoxSubTotales;
        private System.Windows.Forms.TextBox textBoxSub5est;
        private System.Windows.Forms.TextBox textBoxSub4est;
        private System.Windows.Forms.TextBox textBoxSub3est;
        private System.Windows.Forms.TextBox textBoxSub2est;
        private System.Windows.Forms.TextBox textBoxSub1est;
        private System.Windows.Forms.Button OK;
    }
}