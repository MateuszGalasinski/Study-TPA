using System.Windows.Input;

namespace SharedUILogic.Base
{
    public interface ICanExecuteCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}
