using devDept.Eyeshot;
using DevExpress.XtraEditors;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public partial class FormLineType : DevExpress.XtraEditors.XtraForm
    {
        Design design;
        public FormLineType(devDept.Eyeshot.Design design)
        {
            InitializeComponent();

            this.design = design;
            lineTypeControl1.SetDesign(design);

            Translate();
        }


        public void RefreshDataSource()
        {
            lineTypeControl1.RefreshDataSource();
        }

        private void Translate()
        {
            Text = LanguageHelper.Tr("Line Type");
            simpleButtonClose.Text = LanguageHelper.Tr("Close");
        }

        private void simpleButtonClose_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}