using System.Threading.Tasks;
using System.Windows.Input;

namespace SharedUILogic.Base
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}
