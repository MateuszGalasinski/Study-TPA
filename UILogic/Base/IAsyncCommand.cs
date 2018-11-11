using System.Threading.Tasks;
using System.Windows.Input;

namespace UILogic.Base
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}
