using System.Collections.Generic;
using System.Windows.Input;
using SharedUILogic.Base;
using SharedUILogic.Model;

namespace SharedUILogic.ViewModel
{
    public interface IMainViewModel
    {
        ICommand GetFilePathCommand { get; }

        ICanExecuteCommand LoadMetadataCommand { get; }

        List<TreeItem> TreeItems { get; set; }
    }
}
