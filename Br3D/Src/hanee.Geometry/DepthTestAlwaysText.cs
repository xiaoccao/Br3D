using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry.Entities;
using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    public class DepthTestAlwaysText : Text
    {
        public DepthTestAlwaysText(Text text) : base(text)
        {
        }

        public DepthTestAlwaysText(Point3D insPoint, string textString, double height) : base(insPoint, textString, height)
        {

        }

        protected override void Draw(DrawParams data)
        {
            data.RenderContext.PushDepthStencilState();
            data.RenderContext.SetState(depthStencilStateType.DepthTestOff);

            base.Draw(data);

            data.RenderContext.PopDepthStencilState();
        }
    }
}
