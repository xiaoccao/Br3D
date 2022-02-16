
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
            this.layerControl1 = new hanee.Cad.Tool.LayerControl();
            this.simpleButtonClose = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // layerControl1
            // 
            this.layerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layerControl1.Location = new System.Drawing.Point(0, 0);
            this.layerControl1.Name = "layerControl1";
            this.layerControl1.Size = new System.Drawing.Size(713, 337);
            this.layerControl1.TabIndex = 0;
            // 
            // simpleButtonClose
            // 
            this.simpleButtonClose.Location = new System.Drawing.Point(626, 357);
            this.simpleButtonClose.Name = "simpleButtonClose";
            this.simpleButtonClose.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonClose.TabIndex = 1;
            this.simpleButtonClose.Text = "Close";
            // 
            // FormLayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 392);
            this.Controls.Add(this.simpleButtonClose);
            this.Controls.Add(this.layerControl1);
            this.Name = "FormLayer";
            this.Text = "FormLayer";
            this.ResumeLayout(false);

        }

        #endregion

        private LayerControl layerControl1;
        private DevExpress.XtraEditors.SimpleButton simpleButtonClose;
    }
}