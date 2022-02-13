namespace hanee.Cad.Tool
{
    partial class FormFilter
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
            this.textBoxNo = new System.Windows.Forms.TextBox();
            this.labelNo = new System.Windows.Forms.Label();
            this.buttonSelectByNo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxNo
            // 
            this.textBoxNo.Location = new System.Drawing.Point(43, 6);
            this.textBoxNo.Name = "textBoxNo";
            this.textBoxNo.Size = new System.Drawing.Size(100, 21);
            this.textBoxNo.TabIndex = 0;
            this.textBoxNo.Text = "432";
            this.textBoxNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // labelNo
            // 
            this.labelNo.AutoSize = true;
            this.labelNo.Location = new System.Drawing.Point(12, 9);
            this.labelNo.Name = "labelNo";
            this.labelNo.Size = new System.Drawing.Size(25, 12);
            this.labelNo.TabIndex = 1;
            this.labelNo.Text = "No.";
            // 
            // buttonSelectByNo
            // 
            this.buttonSelectByNo.Location = new System.Drawing.Point(150, 6);
            this.buttonSelectByNo.Name = "buttonSelectByNo";
            this.buttonSelectByNo.Size = new System.Drawing.Size(75, 23);
            this.buttonSelectByNo.TabIndex = 2;
            this.buttonSelectByNo.Text = "Select";
            this.buttonSelectByNo.UseVisualStyleBackColor = true;
            this.buttonSelectByNo.Click += new System.EventHandler(this.buttonSelectByNo_Click);
            // 
            // FormFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 40);
            this.Controls.Add(this.buttonSelectByNo);
            this.Controls.Add(this.labelNo);
            this.Controls.Add(this.textBoxNo);
            this.Name = "FormFilter";
            this.Text = "Filter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNo;
        private System.Windows.Forms.Label labelNo;
        private System.Windows.Forms.Button buttonSelectByNo;
    }
}