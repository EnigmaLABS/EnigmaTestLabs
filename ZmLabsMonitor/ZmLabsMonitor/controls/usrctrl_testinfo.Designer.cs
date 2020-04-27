namespace ZmLabsMonitor.controls
{
    partial class usrctrl_testinfo
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(usrctrl_testinfo));
            this.label1 = new System.Windows.Forms.Label();
            this.txtTest = new System.Windows.Forms.TextBox();
            this.txtClassName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmdPlay = new System.Windows.Forms.Button();
            this.cmdHistorico = new System.Windows.Forms.Button();
            this.cmdInfo = new System.Windows.Forms.Button();
            this.panelDetalle = new System.Windows.Forms.Panel();
            this.picSave = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(21, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Test:";
            // 
            // txtTest
            // 
            this.txtTest.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtTest.Location = new System.Drawing.Point(169, 17);
            this.txtTest.Name = "txtTest";
            this.txtTest.Size = new System.Drawing.Size(573, 31);
            this.txtTest.TabIndex = 3;
            // 
            // txtClassName
            // 
            this.txtClassName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtClassName.Location = new System.Drawing.Point(169, 57);
            this.txtClassName.Multiline = true;
            this.txtClassName.Name = "txtClassName";
            this.txtClassName.Size = new System.Drawing.Size(573, 31);
            this.txtClassName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(21, 60);
            this.label2.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 23);
            this.label2.TabIndex = 6;
            this.label2.Text = "Clase:";
            // 
            // cmdPlay
            // 
            this.cmdPlay.Location = new System.Drawing.Point(629, 101);
            this.cmdPlay.Name = "cmdPlay";
            this.cmdPlay.Size = new System.Drawing.Size(113, 52);
            this.cmdPlay.TabIndex = 9;
            this.cmdPlay.Text = "PLAY";
            this.cmdPlay.UseVisualStyleBackColor = true;
            this.cmdPlay.Click += new System.EventHandler(this.cmdPlay_Click);
            // 
            // cmdHistorico
            // 
            this.cmdHistorico.Location = new System.Drawing.Point(510, 101);
            this.cmdHistorico.Name = "cmdHistorico";
            this.cmdHistorico.Size = new System.Drawing.Size(113, 52);
            this.cmdHistorico.TabIndex = 17;
            this.cmdHistorico.Text = "Histórico";
            this.cmdHistorico.UseVisualStyleBackColor = true;
            this.cmdHistorico.Click += new System.EventHandler(this.cmdHistorico_Click);
            // 
            // cmdInfo
            // 
            this.cmdInfo.BackColor = System.Drawing.Color.Coral;
            this.cmdInfo.Location = new System.Drawing.Point(391, 101);
            this.cmdInfo.Name = "cmdInfo";
            this.cmdInfo.Size = new System.Drawing.Size(113, 52);
            this.cmdInfo.TabIndex = 18;
            this.cmdInfo.Text = "Info.";
            this.cmdInfo.UseVisualStyleBackColor = false;
            this.cmdInfo.Click += new System.EventHandler(this.cmdInfo_Click);
            // 
            // panelDetalle
            // 
            this.panelDetalle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelDetalle.Location = new System.Drawing.Point(25, 166);
            this.panelDetalle.Name = "panelDetalle";
            this.panelDetalle.Size = new System.Drawing.Size(1007, 778);
            this.panelDetalle.TabIndex = 19;
            // 
            // picSave
            // 
            this.picSave.BackColor = System.Drawing.Color.Transparent;
            this.picSave.Image = ((System.Drawing.Image)(resources.GetObject("picSave.Image")));
            this.picSave.Location = new System.Drawing.Point(764, 17);
            this.picSave.Name = "picSave";
            this.picSave.Size = new System.Drawing.Size(55, 55);
            this.picSave.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSave.TabIndex = 0;
            this.picSave.TabStop = false;
            // 
            // usrctrl_testinfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.picSave);
            this.Controls.Add(this.panelDetalle);
            this.Controls.Add(this.cmdInfo);
            this.Controls.Add(this.cmdHistorico);
            this.Controls.Add(this.cmdPlay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClassName);
            this.Controls.Add(this.txtTest);
            this.Controls.Add(this.label1);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "usrctrl_testinfo";
            this.Size = new System.Drawing.Size(1035, 947);
            this.Load += new System.EventHandler(this.usrctrl_testinfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picSave)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTest;
        private System.Windows.Forms.TextBox txtClassName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button cmdPlay;
        private System.Windows.Forms.Button cmdHistorico;
        private System.Windows.Forms.Button cmdInfo;
        private System.Windows.Forms.Panel panelDetalle;
        private System.Windows.Forms.PictureBox picSave;
    }
}
