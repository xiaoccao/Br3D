using System.ComponentModel;

namespace hanee.ThreeD
{
    public class CategoryEx : CategoryAttribute
    {
        string text;
        public CategoryEx(string text)
        {
            this.text = text;
        }

        protected override string GetLocalizedString(string value)
        {
            return LanguageHelper.Tr(text);
        }
    }
}
