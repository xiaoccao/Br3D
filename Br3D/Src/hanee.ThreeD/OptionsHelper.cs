using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    static public class OptionsHelper
    {
        /// <summary>
        /// option 파일 경로
        /// </summary>
        /// <returns></returns>
        static public string GetOptionsFilePath()
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);

            // 경로가 없으면 만든다
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            return path;
        }

        /// <summary>
        /// option 파일 경로 + 파일명
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        static public string GetOptionsFilePathName(string fileName)
        {
            string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Application.ProductName);

            // 경로가 없으면 만든다
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            return System.IO.Path.Combine(OptionsHelper.GetOptionsFilePath(), fileName);
        }
    }
}
