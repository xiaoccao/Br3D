using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace hanee.ThreeD
{
    // 치수 관련 옵션 함수들
    public class DimensionOptionsHelper
    {
        static private float dimTextHeight = 2.0f;

        static public float DimTextHeight
        {
            get
            {
                return dimTextHeight;
            }
            set
            {
                dimTextHeight = value;
                if (dimTextHeight <= 0)
                    dimTextHeight = 2.0f;
            }
        }

        // 치수언어 옵션에 대한 파일 명
        static string DimensionOptionsFileName()
        {
            return OptionsHelper.GetOptionsFilePathName("dimension_options.xml");
        }

        // 현재 언어 설정을 읽어온다.
        public static void ReadDimensionOptions()
        {
            string fileName = DimensionOptionsFileName();
            if (System.IO.File.Exists(fileName))
            {
                XDocument doc = XDocument.Load(DimensionOptionsFileName());
                if (doc.Root == null)
                    return;

                XElement xLanguage = doc.Root.Element("dim_text_height");
                if (xLanguage != null)
                {
                    DimTextHeight   = float.Parse(xLanguage.Value);
                }
            }
        }

        // 현재 언어 설정을 쓴다.
        public static void WriteDimensionOptions()
        {
            string fileName = DimensionOptionsFileName();
            XDocument doc = new XDocument();
            doc.Add(new XElement("root"));

            XElement xLanguage = new XElement("dim_text_height");
            xLanguage.SetValue(DimTextHeight.ToString());
            doc.Root.Add(xLanguage);

            doc.Save(fileName);
        }
    }
}
