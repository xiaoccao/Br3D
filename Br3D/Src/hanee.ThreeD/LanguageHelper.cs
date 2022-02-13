using NGettext;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace hanee.ThreeD
{
    static public class LanguageHelper
    {
        static private Dictionary<string, ICatalog> loadedCataloges = new Dictionary<string, ICatalog>();

        // 마지막으로 load된 catalog
        static private ICatalog lastCatalog = null;



        public static string Tr(string text)
        {
            if (lastCatalog == null)
                return text;

            return lastCatalog.GetString(text);
        }

        // ko-KR, en-US
        static public void Load(string code)
        {
            if (code == Options.defaultLanguage)
            {
                lastCatalog = null;
                return;
            }

            if (loadedCataloges.ContainsKey(code))
            {
                lastCatalog = loadedCataloges[code];
                return;
            }

            var i18nPath = Path.Combine(hanee.ThreeD.Util.GetExePath(), "i18n");
            ICatalog catalog = new Catalog("Br3D", i18nPath, new CultureInfo(code));
            loadedCataloges.Add(code, catalog);
            lastCatalog = catalog;
        }
    }
}
