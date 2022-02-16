using devDept.Eyeshot;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionLayer : ActionBase
    {
        static public FormLayer formLayer;
        public ActionLayer(Workspace environment) : base(environment)
        {
        }

        public override void Run()
        {
            if (formLayer == null)
            {
                formLayer = new FormLayer(GetDesign());

                formLayer.Parent = GetDesign();
                formLayer.FormClosed += FormLayer_FormClosed;
                
            }

            formLayer.Show();
        }

        private void FormLayer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            formLayer = null;
        }
    }
}
