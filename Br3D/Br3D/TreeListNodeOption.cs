using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D
{
    public class TreeListNodeOption
    {
        public int ParentID { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        public bool Checked { get; set; }

        public object Tag { get; set; }

        public bool Expanded { get; set; }
    }

    public class TreeData
    {
        public TreeData(int ID, int parentID, string Name)
        {
            this.ID = ID;
            this.ParentID = parentID;
            this.Name = Name;
        }
        public int ID = 1;
        public int ParentID = 0;
        public string Name = "";
        public object Tag = null;
        public bool Expanded = false;
        public Dictionary<string, TreeData> dic = new Dictionary<string, TreeData>();

        public static List<TreeData> GetAllTreeData(TreeData data)
        {
            List<TreeData> allData = new List<TreeData>();
            allData.Add(data);

            foreach (var d in data.dic.Values)
            {
                var allDataTmp = TreeData.GetAllTreeData(d);
                allData.AddRange(allDataTmp);
            }


            return allData;
        }
    }
}
