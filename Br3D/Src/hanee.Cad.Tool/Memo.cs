using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class Memo : devDept.Eyeshot.Labels.LeaderAndImage
    {
        public string[] textLines;
        public Memo(Point3D pt, Bitmap bitmap, Color leaderColor, Vector2D offset, string[] textLines) : base(pt, bitmap, leaderColor, offset)
        {
            this.textLines = textLines.Clone() as string[];

        }

        public string OneLineText
        {
            get
            {
                if (textLines == null || textLines.Length == 0)
                    return "";
                else
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (var textline in textLines)
                    {
                        sb.AppendLine(textline);
                    }
                    return sb.ToString();
                }
                
            }
        }

        public override void Draw(RenderContextBase renderContext)
        {
            base.Draw(renderContext);
            
        }

        public override void DrawSelected(RenderContextBase renderContext)
        {
            base.DrawSelected(renderContext);
        }
        protected override void DrawForSelection(RenderContextBase context)
        {
            base.DrawForSelection(context);
        }

        public override void DrawGdi(float drawScaleFactor, float lineWeightFactor, Graphics g)
        {
            base.DrawGdi(drawScaleFactor, lineWeightFactor, g);
            
        }
    }
}
