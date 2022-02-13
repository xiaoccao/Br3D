using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Br3D
{
    public partial class FormAbout : DevExpress.XtraEditors.XtraForm
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void labelControlName_Click(object sender, EventArgs e)
        {

        }

        private void hyperlinkLabelControl1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://hileejaeho.cafe24.com/kr-br3d/");
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}