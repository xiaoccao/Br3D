
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;

namespace Br3D
{
    public static class ObjectTreeListHelper
    {
        static public void Regen(DevExpress.XtraTreeList.TreeList treeList, devDept.Eyeshot.Model model)
        {
            treeList.Nodes.Clear();
            foreach (var ent in model.Entities)
            {
                List<string> properties = GetTreeNodeProperties(ent);
                AddTreeNode(treeList, properties, ent);
            }
        }

        // node를 하나 추가한다.
        private static void AddTreeNode(DevExpress.XtraTreeList.TreeList treeList, List<string> properties, devDept.Eyeshot.Entities.Entity ent)
        {
            TreeListNodes nodes = treeList.Nodes;
            for (int i = 0; i < properties.Count; i++)
            {
                string prop = properties[i];

                
                // 마지막이 아니면 현재 prop의 이름을가진 노드를 가져온다.(없으며 만든다)
                if (i < properties.Count - 1)
                {
                    TreeListNode node = GetNode(nodes, prop);
                    if (node == null)
                        return;

                    nodes = node.Nodes;
                }
                // 마지막이면 무조건 새로 만든다.
                else
                {
                    NewNode(nodes, prop, ent);
                }
            }
            
        }

        private static TreeListNode NewNode(TreeListNodes nodes, string prop, devDept.Eyeshot.Entities.Entity ent)
        {
            var node = nodes.Add(prop);
            node.Tag = ent;

            return node;
        }

        // nodes에서 prop 이름을 가진 노드를 찾는다.
        private static TreeListNode GetNode(TreeListNodes nodes, string prop)
        {
            var firstColumn = nodes.TreeList.Columns[0];
            foreach (TreeListNode node in nodes)
            {
                if (node.GetDisplayText(firstColumn) == prop)
                    return node;
            }

            return NewNode(nodes, prop, null);
        }

        // 객체의 tree node 속성 리턴
        private static List<string> GetTreeNodeProperties(devDept.Eyeshot.Entities.Entity ent)
        {
            List<string> properties = new List<string>();
            properties.Add("Root");
            properties.Add(ent.GetType().ToString());
            properties.Add(ent.ToString());

            return properties;
        }
    }
}
