using devDept.Eyeshot;
using hanee.ThreeD;
using System;

namespace hanee.Cad.Tool
{
    public partial class FormLayer : DevExpress.XtraEditors.XtraForm
    {
        Design design;
        public FormLayer(Design design)
        {
            InitializeComponent();
            this.design = design;
            layerControl1.SetDesign(design);

            Translate();
        }

        public void RefreshDataSource()
        {
            layerControl1.RefreshDataSource();
        }

        private void Translate()
        {
            simpleButtonClose.Text = LanguageHelper.Tr("Close");
        }
    }
}