using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    // display mode와 상관없이 wire로 보이게 하는 mesh
    public class WireMesh : Mesh
    {
        public WireMesh()
            : base()
        {

        }
        public WireMesh(Mesh another)
           : base(another)
        {
        }

        protected override void Draw(DrawParams data)
        {
            Pre();
            gl.Color3fv(Utility.ColorToFloatArray(Color));
            base.Draw(data);
            Post();
        }

        protected override void SetShader(DrawParams data)
        {
            if (data != null)
            {
                bool prevLighting = data.ShaderParams.Lighting;
                data.ShaderParams.Lighting = false;
                base.SetShader(data);
                data.ShaderParams.Lighting = prevLighting;
            }
        }

        protected override void DrawForShadow(RenderParams data)
        {
            Pre();
            base.DrawForShadow(data);
            Post();
        }


        protected override void Render(RenderParams data)
        {
            Pre();
            gl.Color3fv(Utility.ColorToFloatArray(Color));
            base.Render(data);
            Post();
        }

        protected override void DrawSelected(DrawParams drawParams)
        {
            Pre();
            gl.Color3f(1, 1, 0); // Set the Selection color 
            base.DrawSelected(drawParams);
            Post();
        }

        protected override void DrawForSelection(GfxDrawForSelectionParams data)
        {
            Pre();
            base.DrawForSelection(data);
            Post();
        }

        private static void Post()
        {
            gl.PopAttrib();
            gl.PopAttrib();
            gl.PopAttrib();
        }

        private void Pre()
        {
            gl.PushAttrib(gl.ENABLE_BIT);
            gl.PushAttrib(gl.POLYGON_BIT);
            gl.PushAttrib(gl.CURRENT_BIT);

            gl.Disable(gl.LIGHTING);
            gl.Disable(gl.CULL_FACE);
            gl.PolygonMode(gl.FRONT_AND_BACK, gl.LINE);
        }
    }
}
