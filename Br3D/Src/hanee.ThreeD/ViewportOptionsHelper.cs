using devDept.Eyeshot;
using System.Drawing;
using System.Xml.Linq;

namespace hanee.ThreeD
{
    public class ViewportOptionsHelper
    {
        static public Color GetBackColor(Model model, bool top)
        {
            return top ? model.ActiveViewport.Background.TopColor : model.ActiveViewport.Background.BottomColor;
        }
        

        // 치수언어 옵션에 대한 파일 명
        static string ViewportOptionsFileName()
        {
            return OptionsHelper.GetOptionsFilePathName("viewport_options.xml");
        }


        // 현재 viewport 설정을 읽어온다.
        public static void ReadViewportOptions(Model model)
        {
            string fileName = ViewportOptionsFileName();
            if (System.IO.File.Exists(fileName))
            {
                XDocument doc = XDocument.Load(fileName);
                if (doc.Root == null)
                    return;

                // 배경색
                XElement xTopBackColorR = doc.Root.Element("top_back_color_r");
                XElement xTopBackColorG = doc.Root.Element("top_back_color_g");
                XElement xTopBackColorB = doc.Root.Element("top_back_color_b");

                XElement xBottomBackColorR = doc.Root.Element("bottom_back_color_r");
                XElement xBottomBackColorG = doc.Root.Element("bottom_back_color_g");
                XElement xBottomBackColorB = doc.Root.Element("bottom_back_color_b");
                if (xTopBackColorR != null && xTopBackColorG != null && xTopBackColorB != null &&
                    xBottomBackColorR != null && xBottomBackColorG != null && xBottomBackColorB != null)
                {
                    Color topBackColor = Color.FromArgb(int.Parse(xTopBackColorR.Value), int.Parse(xTopBackColorG.Value), int.Parse(xTopBackColorB.Value));
                    Color bottomBackColor = Color.FromArgb(int.Parse(xBottomBackColorR.Value), int.Parse(xBottomBackColorG.Value), int.Parse(xBottomBackColorB.Value));

                    foreach (Viewport v in model.Viewports)
                    {
                        v.Background.TopColor = topBackColor;
                        v.Background.BottomColor = bottomBackColor;
                    }
                }
            }
        }

        // 현재 viewport 설정을 쓴다.
        public static void WriteViewportOptions(Model model)
        {
            string fileName = ViewportOptionsFileName();

            XDocument doc = new XDocument();
            XElement xRoot = new XElement("root");
            doc.Add(xRoot);

            XElement xTopBackColorR = new XElement("top_back_color_r");
            XElement xTopBackColorG = new XElement("top_back_color_g");
            XElement xTopBackColorB = new XElement("top_back_color_b");

            XElement xBottomBackColorR = new XElement("bottom_back_color_r");
            XElement xBottomBackColorG = new XElement("bottom_back_color_g");
            XElement xBottomBackColorB = new XElement("bottom_back_color_b");

            Color topBackColor = GetBackColor(model, true);
            Color bottomBackColor = GetBackColor(model, false);
            xTopBackColorR.SetValue(topBackColor.R.ToString());
            xTopBackColorG.SetValue(topBackColor.G.ToString());
            xTopBackColorB.SetValue(topBackColor.B.ToString());
            xBottomBackColorR.SetValue(bottomBackColor.R.ToString());
            xBottomBackColorG.SetValue(bottomBackColor.G.ToString());
            xBottomBackColorB.SetValue(bottomBackColor.B.ToString());


            doc.Root.Add(xTopBackColorR);
            doc.Root.Add(xTopBackColorG);
            doc.Root.Add(xTopBackColorB);
            doc.Root.Add(xBottomBackColorR);
            doc.Root.Add(xBottomBackColorG);
            doc.Root.Add(xBottomBackColorB);

            doc.Save(fileName);
        }
    }
}
