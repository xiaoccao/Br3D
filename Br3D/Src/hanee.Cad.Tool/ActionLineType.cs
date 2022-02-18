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
    public class ActionLineType : ActionBase
    {
        static public FormLineType formLineType;
        Form owner;
        public ActionLineType(Workspace environment, Form owner) : base(environment)
        {
            this.owner = owner;
        }

        public override void Run()
        {
            if (formLineType == null)
            {
                formLineType = new FormLineType(GetDesign());
                formLineType.FormClosed += FormLayer_FormClosed;
            }

            formLineType.Show();
            formLineType.Owner = owner;
        }

        private void FormLayer_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            formLineType = null;
        }
    }
}
