namespace ZmLabsMonitor.controls
{
    partial class usrctrl_monitorlist
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
            this.components = new System.ComponentModel.Container();
            this.lstMonitor = new System.Windows.Forms.ListView();
            this.timerControl = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lstMonitor
            // 
            this.lstMonitor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstMonitor.BackColor = System.Drawing.Color.WhiteSmoke;
            this.lstMonitor.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstMonitor.HideSelection = false;
            this.lstMonitor.Location = new System.Drawing.Point(6, 5);
            this.lstMonitor.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.lstMonitor.Name = "lstMonitor";
            this.lstMonitor.Size = new System.Drawing.Size(1443, 957);
            this.lstMonitor.TabIndex = 1;
            this.lstMonitor.UseCompatibleStateImageBehavior = false;
            this.lstMonitor.View = System.Windows.Forms.View.List;
            // 
            // timerControl
            // 
            this.timerControl.Interval = 2000;
            this.timerControl.Tick += new System.EventHandler(this.timerControl_Tick);
            // 
            // usrctrl_monitorlist
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstMonitor);
            this.Font = new System.Drawing.Font("Verdana", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "usrctrl_monitorlist";
            this.Size = new System.Drawing.Size(1455, 968);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lstMonitor;
        private System.Windows.Forms.Timer timerControl;
    }
}
