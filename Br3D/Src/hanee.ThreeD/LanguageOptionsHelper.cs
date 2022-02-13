using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace hanee.ThreeD
{
    // 언어 관련 함수들
    public class LanguageOptionsHelper
    {
        public enum Language
        {
            korean,
            english,
            count
        };


        // 언어를 변경한다.
        public static void ChangeLanguage(Language language)
        {
            string type = "en-US";
            if (language == Language.korean)
                type = "ko-KR";
            CultureInfo culture = new CultureInfo(type);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        // 언어별 식별 코드
        public static string GetLanguageName(Language language)
        {
            return language.ToString();
        }

        // 언어 옵션에 대한 파일 명
        static string LanguageOptionFileName()
        {
            return OptionsHelper.GetOptionsFilePathName("language.xml");
        }

        // 현재 언어 설정을 읽어온다.
        
        public static Language ReadCurrentLanauage()
        {
            string fileName = LanguageOptionFileName();
            if(System.IO.File.Exists(fileName))
            {
                XDocument doc = XDocument.Load(LanguageOptionFileName());
                if (doc.Root == null)
                    return Language.english;

                XElement xLanguage = doc.Root.Element("language");
                if (xLanguage == null)
                    return Language.english;

                return (LanguageOptionsHelper.Language)int.Parse(xLanguage.Value);
            }
            

            return Language.english;
        }

        // 현재 언어 설정을 쓴다.
        public static void WriteCurrentLanguage(Language language)
        {
            string fileName = LanguageOptionFileName();
            XDocument doc = new XDocument();
            doc.Add(new XElement("root"));

            XElement xLanguage = new XElement("language");
            xLanguage.SetValue((int)language);
            doc.Root.Add(xLanguage);

            doc.Save(fileName);
        }
    }
}
