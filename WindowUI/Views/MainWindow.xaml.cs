using System.Windows;
using UILogic.Services;
using UILogic.ViewModel;

namespace WindowUI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = InitializeViewModel();
        }

        private MainViewModel InitializeViewModel()
        {
            return Composer.GetComposedMainViewModel(
                new FileDialog());
        }
    }
}