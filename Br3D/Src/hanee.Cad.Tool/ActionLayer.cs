using devDept.Eyeshot;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionLayer : ActionBase
    {
        static public FormLayer formLayer;
        Form owner;
        public ActionLayer(Workspace environment, Form owner) : base(environment)
        {
            this.owner = owner;
        }

        public override void Run()
        {
            if (formLayer == null)
            {
                formLayer = new FormLayer(GetDesign());
                formLayer.FormClosed += FormLayer_FormClosed;
            }

            formLayer.Show();
            formLayer.Owner = owner;
        }

        private void FormLayer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            formLayer = null;
        }
    }
}
