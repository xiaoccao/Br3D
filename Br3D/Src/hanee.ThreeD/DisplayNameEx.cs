using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    public class DisplayNameEx : DisplayNameAttribute
    {
        string text;
        public DisplayNameEx(string text)
        {
            this.text = text;
        }
        public override string DisplayName => LanguageHelper.Tr(text);
    }
}
