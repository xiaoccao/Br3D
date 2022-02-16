
namespace hanee.Cad.Tool
{
    partial class FormLayer
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
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.sidePanel1 = new DevExpress.XtraEditors.SidePanel();
            this.layerControl1 = new hanee.Cad.Tool.LayerControl();
            this.sidePanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(636, 5);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(74, 26);
            this.simpleButtonClose.TabIndex = 1;
            this.simpleButtonClose.Text = "Close";
            // 
            // sidePanel1
            // 
            this.sidePanel1.Controls.Add(this.simpleButtonClose);
            this.sidePanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sidePanel1.Location = new System.Drawing.Point(0, 356);
            this.sidePanel1.Name = "sidePanel1";
            this.sidePanel1.Size = new System.Drawing.Size(713, 36);
            this.sidePanel1.TabIndex = 2;
            this.sidePanel1.Text = "sidePanel1";
            // 
            // layerControl1
            // 
            this.layerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layerControl1.Location = new System.Drawing.Point(0, 0);
            this.layerControl1.Name = "layerControl1";
            this.layerControl1.Size = new System.Drawing.Size(713, 356);
            this.layerControl1.TabIndex = 4;
            // 
            // FormLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 392);
            this.Controls.Add(this.layerControl1);
            this.Controls.Add(this.sidePanel1);
            this.Name = "FormLayer";
            this.Text = "FormLayer";
            this.sidePanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
        private DevExpress.XtraEditors.SidePanel sidePanel1;
        private LayerControl layerControl1;
    }
}