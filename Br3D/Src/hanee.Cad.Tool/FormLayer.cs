using devDept.Eyeshot;

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
        }
    }
}