using System.Collections.ObjectModel;

namespace UILogic.Model
{
    public abstract class TreeItem
    {
        private bool _wasBuilt = false;
        private bool _isExpanded;

        public string Name { get; set; }

        public ObservableCollection<TreeItem> Children { get; }

        protected TreeItem(string name)
        {
            Children = new ObservableCollection<TreeItem>() { null };
            Name = name;
        }

        public bool IsExpanded
        {
            get => _isExpanded;

            set
            {
                _isExpanded = value;
                if (_wasBuilt == false)
                {
                    Children.Clear();
                    LoadChildrenItems();
                    _wasBuilt = true;
                }
            }
        }

        protected abstract void LoadChildrenItems();
    }
}
