namespace ZmLabsMonitor
{
    partial class frmMonitor
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

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMonitor));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.treeCatalogo = new System.Windows.Forms.TreeView();
            this.contextMenuArbol = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.nnuevoTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.contextMenuArbol.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.IsSplitterFixed = true;
            this.splitContainer.Location = new System.Drawing.Point(12, 12);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.treeCatalogo);
            this.splitContainer.Panel1.Controls.Add(this.label1);
            this.splitContainer.Size = new System.Drawing.Size(1442, 900);
            this.splitContainer.SplitterDistance = 400;
            this.splitContainer.TabIndex = 1;
            // 
            // treeCatalogo
            // 
            this.treeCatalogo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.treeCatalogo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.treeCatalogo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.treeCatalogo.ContextMenuStrip = this.contextMenuArbol;
            this.treeCatalogo.Location = new System.Drawing.Point(10, 37);
            this.treeCatalogo.Name = "treeCatalogo";
            this.treeCatalogo.Size = new System.Drawing.Size(387, 727);
            this.treeCatalogo.TabIndex = 2;
            this.treeCatalogo.TabStop = false;
            this.treeCatalogo.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeCatalogo_AfterSelect);
            // 
            // contextMenuArbol
            // 
            this.contextMenuArbol.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nnuevoTestToolStripMenuItem});
            this.contextMenuArbol.Name = "contextMenuArbol";
            this.contextMenuArbol.Size = new System.Drawing.Size(181, 48);
            this.contextMenuArbol.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuArbol_Opening);
            // 
            // nnuevoTestToolStripMenuItem
            // 
            this.nnuevoTestToolStripMenuItem.Name = "nnuevoTestToolStripMenuItem";
            this.nnuevoTestToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.nnuevoTestToolStripMenuItem.Text = "Nuevo test...";
            this.nnuevoTestToolStripMenuItem.Click += new System.EventHandler(this.nnuevoTestToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(6, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "catálogo de pruebas";
            // 
            // frmMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1466, 924);
            this.Controls.Add(this.splitContainer);
            this.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "frmMonitor";
            this.Text = "ZMLabs Monitor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMonitor_Load);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.contextMenuArbol.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView treeCatalogo;
        private System.Windows.Forms.ContextMenuStrip contextMenuArbol;
        private System.Windows.Forms.ToolStripMenuItem nnuevoTestToolStripMenuItem;
    }
}

