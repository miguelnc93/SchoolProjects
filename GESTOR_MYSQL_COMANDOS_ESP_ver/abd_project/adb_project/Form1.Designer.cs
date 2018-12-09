namespace abd_project
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
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtsentencia = new System.Windows.Forms.TextBox();
            this.lblsentencia = new System.Windows.Forms.Label();
            this.btexecute = new System.Windows.Forms.Button();
            this.txtresultados = new System.Windows.Forms.TextBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.labelbdenuso = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtcontraseña = new System.Windows.Forms.TextBox();
            this.txtservidor = new System.Windows.Forms.TextBox();
            this.lblusuario = new System.Windows.Forms.Label();
            this.lblcontraseña = new System.Windows.Forms.Label();
            this.txtusuario = new System.Windows.Forms.TextBox();
            this.lblservidor = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.btimportar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(12, 38);
            this.listBox1.Name = "listBox1";
            this.listBox1.ScrollAlwaysVisible = true;
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.Size = new System.Drawing.Size(145, 537);
            this.listBox1.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Location = new System.Drawing.Point(736, 540);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 47);
            this.button2.TabIndex = 7;
            this.button2.Text = "CERRAR";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtsentencia
            // 
            this.txtsentencia.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtsentencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtsentencia.Location = new System.Drawing.Point(166, 47);
            this.txtsentencia.Multiline = true;
            this.txtsentencia.Name = "txtsentencia";
            this.txtsentencia.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtsentencia.Size = new System.Drawing.Size(696, 180);
            this.txtsentencia.TabIndex = 8;
            // 
            // lblsentencia
            // 
            this.lblsentencia.AutoSize = true;
            this.lblsentencia.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblsentencia.Location = new System.Drawing.Point(495, 20);
            this.lblsentencia.Name = "lblsentencia";
            this.lblsentencia.Size = new System.Drawing.Size(125, 18);
            this.lblsentencia.TabIndex = 9;
            this.lblsentencia.Text = "Escribir sentencia";
            // 
            // btexecute
            // 
            this.btexecute.Location = new System.Drawing.Point(621, 15);
            this.btexecute.Name = "btexecute";
            this.btexecute.Size = new System.Drawing.Size(78, 26);
            this.btexecute.TabIndex = 10;
            this.btexecute.Text = "Execute";
            this.btexecute.UseVisualStyleBackColor = true;
            this.btexecute.Click += new System.EventHandler(this.btexecute_Click);
            // 
            // txtresultados
            // 
            this.txtresultados.Location = new System.Drawing.Point(168, 285);
            this.txtresultados.Multiline = true;
            this.txtresultados.Name = "txtresultados";
            this.txtresultados.ReadOnly = true;
            this.txtresultados.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtresultados.Size = new System.Drawing.Size(694, 172);
            this.txtresultados.TabIndex = 13;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.Location = new System.Drawing.Point(168, 285);
            this.listBox2.Name = "listBox2";
            this.listBox2.ScrollAlwaysVisible = true;
            this.listBox2.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox2.Size = new System.Drawing.Size(694, 173);
            this.listBox2.TabIndex = 17;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(168, 285);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(694, 172);
            this.dataGridView1.TabIndex = 24;
            // 
            // labelbdenuso
            // 
            this.labelbdenuso.AutoSize = true;
            this.labelbdenuso.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelbdenuso.Location = new System.Drawing.Point(12, 12);
            this.labelbdenuso.Name = "labelbdenuso";
            this.labelbdenuso.Size = new System.Drawing.Size(0, 18);
            this.labelbdenuso.TabIndex = 23;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = global::adb_project.Properties.Resources.userslogin;
            this.pictureBox1.Location = new System.Drawing.Point(106, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(345, 303);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtcontraseña);
            this.groupBox1.Controls.Add(this.txtservidor);
            this.groupBox1.Controls.Add(this.lblusuario);
            this.groupBox1.Controls.Add(this.lblcontraseña);
            this.groupBox1.Controls.Add(this.txtusuario);
            this.groupBox1.Controls.Add(this.lblservidor);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(523, 19);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(298, 350);
            this.groupBox1.TabIndex = 21;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ingresar Datos";
            // 
            // txtcontraseña
            // 
            this.txtcontraseña.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcontraseña.Location = new System.Drawing.Point(123, 123);
            this.txtcontraseña.Name = "txtcontraseña";
            this.txtcontraseña.PasswordChar = '*';
            this.txtcontraseña.Size = new System.Drawing.Size(138, 26);
            this.txtcontraseña.TabIndex = 19;
            // 
            // txtservidor
            // 
            this.txtservidor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtservidor.Location = new System.Drawing.Point(123, 39);
            this.txtservidor.Name = "txtservidor";
            this.txtservidor.Size = new System.Drawing.Size(138, 22);
            this.txtservidor.TabIndex = 20;
            // 
            // lblusuario
            // 
            this.lblusuario.AutoSize = true;
            this.lblusuario.Location = new System.Drawing.Point(24, 80);
            this.lblusuario.Name = "lblusuario";
            this.lblusuario.Size = new System.Drawing.Size(68, 20);
            this.lblusuario.TabIndex = 5;
            this.lblusuario.Text = "Usuario:";
            // 
            // lblcontraseña
            // 
            this.lblcontraseña.AutoSize = true;
            this.lblcontraseña.Location = new System.Drawing.Point(21, 126);
            this.lblcontraseña.Name = "lblcontraseña";
            this.lblcontraseña.Size = new System.Drawing.Size(96, 20);
            this.lblcontraseña.TabIndex = 6;
            this.lblcontraseña.Text = "Contraseña:";
            // 
            // txtusuario
            // 
            this.txtusuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtusuario.Location = new System.Drawing.Point(123, 80);
            this.txtusuario.Name = "txtusuario";
            this.txtusuario.Size = new System.Drawing.Size(138, 22);
            this.txtusuario.TabIndex = 18;
            // 
            // lblservidor
            // 
            this.lblservidor.AutoSize = true;
            this.lblservidor.Location = new System.Drawing.Point(21, 39);
            this.lblservidor.Name = "lblservidor";
            this.lblservidor.Size = new System.Drawing.Size(71, 20);
            this.lblservidor.TabIndex = 12;
            this.lblservidor.Text = "Servidor:";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(193, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(84, 29);
            this.button1.TabIndex = 4;
            this.button1.Text = "Conectar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btimportar
            // 
            this.btimportar.Location = new System.Drawing.Point(717, 14);
            this.btimportar.Name = "btimportar";
            this.btimportar.Size = new System.Drawing.Size(117, 26);
            this.btimportar.TabIndex = 25;
            this.btimportar.Text = "Importar Archivo";
            this.btimportar.UseVisualStyleBackColor = true;
            this.btimportar.Click += new System.EventHandler(this.btimportar_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(864, 606);
            this.ControlBox = false;
            this.Controls.Add(this.btimportar);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.labelbdenuso);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.txtresultados);
            this.Controls.Add(this.btexecute);
            this.Controls.Add(this.lblsentencia);
            this.Controls.Add(this.txtsentencia);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximumSize = new System.Drawing.Size(880, 645);
            this.MinimumSize = new System.Drawing.Size(880, 645);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABD Proyecto";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lblusuario;
        private System.Windows.Forms.Label lblcontraseña;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtsentencia;
        private System.Windows.Forms.Label lblsentencia;
        private System.Windows.Forms.Button btexecute;
        private System.Windows.Forms.Label lblservidor;
        private System.Windows.Forms.TextBox txtresultados;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox txtusuario;
        private System.Windows.Forms.TextBox txtcontraseña;
        private System.Windows.Forms.TextBox txtservidor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label labelbdenuso;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btimportar;

    }
}

