namespace WindowsFormsApplication1.Historial_Cliente
{
    partial class Historial
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.fechaCompra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.calificada = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estrellas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.salir = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fechaCompra,
            this.descripcion,
            this.cantidad,
            this.calificada,
            this.estrellas});
            this.dataGridView1.Location = new System.Drawing.Point(12, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(620, 217);
            this.dataGridView1.TabIndex = 0;
            // 
            // fechaCompra
            // 
            this.fechaCompra.HeaderText = "Fecha de Compra";
            this.fechaCompra.Name = "fechaCompra";
            // 
            // descripcion
            // 
            this.descripcion.HeaderText = "Descripcion";
            this.descripcion.Name = "descripcion";
            // 
            // cantidad
            // 
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // calificada
            // 
            this.calificada.HeaderText = "Calificada";
            this.calificada.Name = "calificada";
            // 
            // estrellas
            // 
            this.estrellas.HeaderText = "Estrellas";
            this.estrellas.Name = "estrellas";
            // 
            // salir
            // 
            this.salir.Location = new System.Drawing.Point(557, 280);
            this.salir.Name = "salir";
            this.salir.Size = new System.Drawing.Size(75, 23);
            this.salir.TabIndex = 1;
            this.salir.Text = "Salir";
            this.salir.UseVisualStyleBackColor = true;
            this.salir.Click += new System.EventHandler(this.salir_Click);
            // 
            // Historial
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 315);
            this.Controls.Add(this.salir);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Historial";
            this.Text = "Historial de Compra";
            this.Load += new System.EventHandler(this.Historial_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaCompra;
        private System.Windows.Forms.DataGridViewTextBoxColumn descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn calificada;
        private System.Windows.Forms.DataGridViewTextBoxColumn estrellas;
        private System.Windows.Forms.Button salir;
    }
}