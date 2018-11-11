using System.Collections.Generic;
using System.Windows.Input;
using UILogic.Base;
using UILogic.Model;

namespace UILogic.ViewModel
{
    public interface IMainViewModel
    {
        ICommand GetFilePathCommand { get; }

        IRaiseCanExecuteCommand LoadMetadataCommand { get; }

        List<TreeItem> TreeItems { get; set; }
    }
}
