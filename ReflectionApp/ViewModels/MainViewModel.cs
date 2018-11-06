using ReflectionApp.Models;
using ReflectionApp.Services;

namespace ReflectionApp.ViewModels
{
    public class MainViewModel : IMainViewModel
    {
        public string Name { get; set; } = "This is main view model";
        public TreeItem TreeRoot { get; private set; }
        private IDataRepository _dataRepository;

        public MainViewModel(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
            TreeRoot = _dataRepository?.LoadTreeRoot("./TPA.ApplicationArchitecture.dll");
        }
    }
}
