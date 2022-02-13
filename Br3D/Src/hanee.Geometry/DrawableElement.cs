using devDept.Eyeshot;
using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text;

namespace hanee.Geometry
{
    [Serializable]
    public class DrawableElement : Element
    {
        public string materialName{ get; set; }
        public string textureFileName { get; set; } 
        public Color materialDiffuse{ get; protected set; }// material의 색
        public Color materialSpecular{ get; set; }
        public float materialEnvironment { get; set; }// 주변환경이 비치는 정도
        public double materialScaleU { get; set; }// texture의 scale
        public double materialScaleV { get; set; }// texture의 scale
        public Point2D mappingLeftBottom { get; set; }  // mapping할때 좌측 하단 기준 좌표
        public bool visible { get; set; }
        public bool entityHasSelfMaterialName { get; set; } // entity들이 material 이름을 직접 가지는지?(terrain의 경우 surface가 각각 material 이름을 가진다.)
        public bool fixedDiffuseAlpha { get; set; }
        public DrawableElement()
        {
            // element의 색상을 변경 하려면 반드시 materialDiffuse등의 값을 변경후 반드시 MaterialHelper.SetByElement 함수를 호출해서
            // 실제 eyeshot의 material에 반영해 줘야 한다.
            materialName = null;// Guid.NewGuid().ToString();
            materialDiffuse = Color.White;
            materialSpecular = Color.White;
            materialEnvironment = 0.0f;
            entityHasSelfMaterialName = false;
            fixedDiffuseAlpha = false;
            materialScaleU = 1;
            materialScaleV = 1;
            mappingLeftBottom = new Point2D(0, 0);

            visible = true;
        }

        public DrawableElement(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, false);
        }

        override public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, true);
        }

        override public void Serialize(SerializationInfo info, bool serialize)
        {
            base.Serialize(info, serialize);

            materialName = SerializeHelper.Serialize<string>(info, "material_name", materialName, serialize);
            textureFileName = SerializeHelper.Serialize<string>(info, "texture_filename", textureFileName, serialize);
            materialDiffuse = SerializeHelper.Serialize<Color>(info, "material_diffuse", materialDiffuse, serialize);
            materialSpecular = SerializeHelper.Serialize<Color>(info, "material_specular", materialSpecular, serialize);
            materialEnvironment = SerializeHelper.Serialize<float>(info, "material_environment", materialEnvironment, serialize);
            materialScaleU = SerializeHelper.Serialize<double>(info, "material_scale_u", materialScaleU, serialize);
            materialScaleV = SerializeHelper.Serialize<double>(info, "material_scale_v", materialScaleV, serialize);
            mappingLeftBottom = SerializeHelper.Serialize<Point2D>(info, "mappingLeftBottom", mappingLeftBottom, serialize);
            entityHasSelfMaterialName = SerializeHelper.Serialize<bool>(info, "entityHasSelfMaterialName", entityHasSelfMaterialName, serialize);
            fixedDiffuseAlpha   = SerializeHelper.Serialize<bool>(info, "fixedDiffuseAlpha", fixedDiffuseAlpha, serialize);
            visible = SerializeHelper.Serialize<bool>(info, "visible", visible, serialize);
            
        }

        public bool enabledMaterial 
        { 
            get
            {
                return !string.IsNullOrEmpty(materialName);
            } 
        }
        public void SetMaterialDiffuse(Color color)
        {
            // alpha가 고정된 경우에는 최대 254까지만 변경이 가능하다.
            if (fixedDiffuseAlpha)
                materialDiffuse = Color.FromArgb(Math.Min((byte)254, color.A), color);
            else
                materialDiffuse = color;
        }

        virtual public List<string> GetAllMaterialNames()
        {
            List<string> names = new List<string>();
            names.Add(materialName);
            return names;
        }
        virtual protected string GetDefaultMaterialName()
        {
            return Guid.NewGuid().ToString();
        }
        // material을 초기화 한다.
        // material이 초기화 되기 전에는 material로 랜더링 되지 않는다.
        virtual public void InitMaterial(Model model, List<string> additionalMaterialNames=null)
        {
            // name 이 없으면 name을 만들어 준다.
            if(!enabledMaterial)
            {
                materialName = GetDefaultMaterialName();
            }

            // material이 등록 되어 있지 않으면 등록 시킨다.
            if (!model.Materials.Contains(materialName))
            {
                Material newMaterial = Material.Gold;
                newMaterial.Name = materialName;
                model.Materials.Add(newMaterial);
            }

            if(additionalMaterialNames != null)
            {
                foreach(var amn in additionalMaterialNames)
                {
                    // material이 등록 되어 있지 않으면 등록 시킨다.
                    if (!model.Materials.Contains(amn))
                    {
                        Material newMaterial = Material.Gold;
                        newMaterial.Name = amn;
                        model.Materials.Add(newMaterial);
                    }
                }
            }
        }
    }
}
