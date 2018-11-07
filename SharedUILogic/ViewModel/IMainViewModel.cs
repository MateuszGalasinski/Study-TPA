using System.Collections.Generic;
using System.Windows.Input;
using SharedUILogic.Model;

namespace SharedUILogic.ViewModel
{
    public interface IMainViewModel
    {
        ICommand GetFilePathCommand { get; }

        ICommand LoadMetadataCommand { get; }

        List<TreeItem> TreeItems { get; set; }
    }
}
