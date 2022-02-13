using devDept.Graphics;
using hanee.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    static public class MaterialHelper
    {
        static public bool IsExistTextureImage(this Material material)
        {
            return material.TextureImage != null ? true : false;
        }

        // element로 material속성을 설정한다.
        // element의 속성(color, texture등..)을 변경하고 나면 반드시 이 함수를 통해서 material로 변경해야 한다.
        static public void SetByElement(this Material material, DrawableElement ele)
        {
            if (ele == null)
            {
                material.ClearTexture();
                return;
            }

            
            material.Diffuse = ele.materialDiffuse;
            material.Environment = ele.materialEnvironment;
            material.Specular = ele.materialSpecular;

            // alpha가 고정된 경우에는 texture를 별도로 처리한다.
            // alpha가 고정된 경우에는 임의로 texture image를 만들어서 넣기 때문에 여기서 지우거나 넣으면 혼란을 야기함
            if (!ele.fixedDiffuseAlpha)
            {
                if (!string.IsNullOrEmpty(ele.textureFileName))
                {
                    material.TextureImage = new Bitmap(ele.textureFileName);
                }
                else
                {
                    material.ClearTexture();
                }
            }
        }
    }
}
