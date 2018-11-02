using System.Collections.Generic;

namespace ReflectionApp.Models
{
    public class TreeItem
    {
        public string Id { get; }
        public IEnumerable<TreeItem> Children { get; }
        public bool IsExpanded { get; set; } = false;

        public TreeItem(string id)
        {
            Id = id;
        }
    }
}
