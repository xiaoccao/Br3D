using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace hanee.Cad.Tool
{
    public partial class FormInputMessage : DevExpress.XtraEditors.XtraForm
    {
        public FormInputMessage(string title)
        {
            InitializeComponent();
            labelControlTitle.Text = title;
        }

        public FormInputMessage()
        {
            InitializeComponent();
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        public RichTextBox RichTextBox { get { return richTextBox1; } }

        private void FormInputMessage_Load(object sender, EventArgs e)
        {
            CenterToParent();
        }
    }
}