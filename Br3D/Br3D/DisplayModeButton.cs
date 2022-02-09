using devDept.Eyeshot;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D
{
    class DisplayModeButton : DefaultToolBarButton
    {
        public DisplayModeButton(string toolTipText, styleType style, bool visible, bool enabled) : base(toolTipText, style, visible, enabled)
        {

        }

        public DisplayModeButton(Image buttonImage, string name, string toolTipText, styleType style, bool visible, bool enabled) : base(buttonImage, name, toolTipText, style, visible, enabled)
        {
        }
    }
}
