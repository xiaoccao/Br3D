
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Br3D
{
    public static class ObjectTreeListHelper
    {
        static public void Regen(DevExpress.XtraTreeList.TreeList treeList, devDept.Eyeshot.Model model)
        {
            SetOptionsAsElementTreeList(treeList);
            treeList.TreeViewFieldName = "Name";
            var options = GenerateDataSource(model);
            treeList.DataSource = options;
            treeList.ForceInitialize();

            foreach (TreeListNode node in treeList.Nodes)
            {
                node.ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                SetChildrenCheckBoxStyle(node);
            }

            // tag 연결
            foreach (var op in options)
            {
                var node = treeList.FindNodeByID(op.ID);
                if (node == null)
                    continue;
                node.Tag = op.Tag;
                node.Checked = op.Checked;
                node.Expanded = op.Expanded;
            }

            treeList.ExpandToLevel(2);
        }


        // element를 tree로 만들기 위한 data source를 만든다.
        static public BindingList<TreeListNodeOption> GenerateDataSource(devDept.Eyeshot.Model model)
        {
            int id = -1;
            TreeData rootData = new TreeData(id++, -1, "root");

            // 모든 entity의 property를 가져옴
            for (int i = 0; i < model.Entities.Count; ++i)
            {
                var ele = model.Entities[i];


                List<string> properties = GetTreeNodeProperties(ele);
                if (properties == null || properties.Count == 0)
                    continue;

                AddTreeNode(rootData, properties, ele, ref id);
            }


            // TreeListNodeOption으로 변환
            // 즉, 처음 정했던 id가 바뀔 수 있다.
            // 변경되는 id 정보를 보관했다가 parent id에 변경 id를 적용해준다.
            var allData = TreeData.GetAllTreeData(rootData);

            BindingList<TreeListNodeOption> options = new BindingList<TreeListNodeOption>();
            int realID = 0;
            var idChangeInfo = new Dictionary<int, int>();
            foreach (TreeData data in allData)
            {
                if (data.ID == -1)
                    continue;

                // 실제로 반영되는 id
                idChangeInfo.Add(data.ID, realID);

                // 속성이 이미 있는지 찾는다.
                TreeListNodeOption option = new TreeListNodeOption();
                option.ParentID = data.ParentID;
                option.ID = realID;
                option.Name = data.Name;
                option.Checked = true;
                option.Tag = data.Tag;
                option.Expanded = data.Expanded;
                options.Add(option);

                realID++;
            }

            // 변경된 id로 원래 parent id를 변경해준다.
            foreach (var option in options)
            {
                if (!idChangeInfo.ContainsKey(option.ParentID))
                    continue;

                option.ParentID = idChangeInfo[option.ParentID];
            }

            return options;
        }

        static private void AddTreeNode(TreeData rootData, List<string> listProperties, object tag, ref int id)
        {
            TreeData curData = rootData;

            for (int i = 0; i < listProperties.Count; i++)
            {
                string prop = listProperties[i];
                if (prop == null)
                    continue;

                TreeData tmp;
                // 있으면 curData로 교체하고 통과
                if (curData.dic.TryGetValue(prop, out tmp))
                {
                    curData = tmp;

                    // 마지막 노드에는 tag를 연결한다.
                    if (i == listProperties.Count - 1)
                    {
                        tmp.Tag = tag;
                    }

                    continue;
                }

                // 없으면 만들어서 추가
                tmp = new TreeData(id++, curData.ID, prop);

                curData.dic[prop] = tmp;

                curData = tmp;

                // 마지막 노드에는 tag를 연결한다.
                if (i == listProperties.Count - 1)
                {
                    tmp.Tag = tag;
                }
            }
        }

        static public void SetChildrenCheckBoxStyle(TreeListNode node)
        {
            if (node == null)
                return;

            foreach (TreeListNode childnode in node.Nodes)
            {
                childnode.ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                if (childnode.Nodes != null)
                    SetChildrenCheckBoxStyle(childnode);
            }
        }

        static public void SetOptionsAsElementTreeList(DevExpress.XtraTreeList.TreeList treeList)
        {
            treeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            treeList.OptionsBehavior.ReadOnly = false;
            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsSelection.EnableAppearanceFocusedRow = true;
            treeList.OptionsSelection.KeepSelectedOnClick = true;
        }

        private static TreeListNode NewNode(TreeListNodes nodes, string prop, devDept.Eyeshot.Entities.Entity ent)
        {
            var node = nodes.Add(prop);
            node.SetValue(nodes.TreeList.Columns[0], prop);
            node.Tag = ent;

            return node;
        }

        // nodes에서 prop 이름을 가진 노드를 찾는다.
        private static TreeListNode GetNode(TreeListNodes nodes, string prop)
        {
            var firstColumn = nodes.TreeList.Columns[0];
            foreach (TreeListNode node in nodes)
            {
                var text = node.GetDisplayText(firstColumn);
                if (text == prop)
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
