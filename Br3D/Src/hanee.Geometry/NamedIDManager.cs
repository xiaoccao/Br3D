using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    public class NamedIDManager
    {
        public NamedIDManager()
        {
            items = new BindingList<NamedID>();
            defaultItemCount = 0;
        }
        public BindingList<NamedID> items { get; set; }
        public int defaultItemCount { get; set; }

        public void AddDefaultItems(string[] itemNames)
        {
            if (itemNames == null)
                return;

            defaultItemCount = itemNames.Length;
            foreach(var name in itemNames)
            {
                AddItem(name);
            }
        }
        public string GetFirstId()
        {
            return items.Count > 0 ? items.First().id : "";
        }
        public int FindIdxById(string id)
        {
            for(int i = 0; i < items.Count; ++i)
            {
                if (items[i].id.Equals(id))
                    return i;
            }
            return -1;
        }

        public bool AddItem(long id, string name)
        {
            var stringId = id.ToString();
            if (items.FirstOrDefault(x => x.id == stringId) != null)
                return false;

            NamedID newItem = new NamedID();
            newItem.id = stringId;
            newItem.name = name;
            items.Add(newItem);

            return true;
        }

        public string AddItem(string name)
        {
            var newItem = NamedID.New(name);
            items.Add(newItem);
            return newItem.id;
        }

        public string FindNameById(string id)
        {
            foreach(var item in items)
            {
                if (item.id.Equals(id))
                    return item.name;
            }

            return "";
        }

        public NamedID FindItemById(long id)
        {
            return FindItemById(id.ToString());
        }

        public NamedID FindItemById(string id)
        {
            foreach (var item in items)
            {
                if (item.id.Equals(id))
                    return item;
            }
            return null;
        }

        public string FindIdByName(string name)
        {
            foreach(var item in items)
            {
                if (item.name.Equals(name))
                    return item.id;
            }
            return "";
        }

        public string FindIdByIdx(int idx)
        {
            if (idx < 0 || idx >= items.Count)
                return "";
            return items[idx].id;
        }

        // id를 삭제하고, 대신할 id를 리턴한다.
        public string DelItemById(string id)
        {
            int idx = FindIdxById(id);
            if (idx < 0)
                return "";
            DelItem(idx);

            if (items.Count == 0)
                return "";

            idx--;
            if (idx < 0)
                idx = 0;

            return items[idx].id;
        }

        public void DelItem(int idx)
        {
            if (idx < 0 || idx >= items.Count)
                return;
            items.RemoveAt(idx);
        }

        public void MoveUp(int idx)
        {
            if (idx <= 0)
                return;
            var cur = items[idx];
            items.RemoveAt(idx);
            items.Insert(idx - 1, cur);
        }

        public void MoveDown(int idx)
        {
            if (idx >= items.Count)
                return;

            var cur = items[idx];
            items.RemoveAt(idx);
            items.Insert(idx + 1, cur);
        }

        public string GetFirstName()
        {
            return items.Count > 0 ? items.First().name : "";
        }
    }
}
