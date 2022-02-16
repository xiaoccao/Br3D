using devDept.Eyeshot;
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

namespace hanee.Cad.Tool
{
    public partial class LayerControl : DevExpress.XtraEditors.XtraUserControl
    {
        public LayerControl()
        {
            InitializeComponent();
        }

        public void SetDesign(Design design)
        {
            gridControl1.DataSource = design.Layers;
        }
    }
}
