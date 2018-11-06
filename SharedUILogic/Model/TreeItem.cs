using System.Collections.Generic;

namespace SharedUILogic.Model
{
    public class TreeItem
    {
        public TreeItem(string name, bool hasChildren)
        {
            Name = name;
            Children = new List<TreeItem>();
            IsExpendable = hasChildren;
        }

        public string Name { get; set; }

        public List<TreeItem> Children { get; }

        public bool IsExpendable { get; set; }
    }
}
