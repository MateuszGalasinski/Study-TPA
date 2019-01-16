using Core.Components;
using Logging;
using System.Windows;
using UILogic.ViewModel;
using XmlSerialization;
using DBMaster;

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
            ILogger logger = new Logger();
            return new MainViewModel(new FileDialog(logger), logger, new XmlMetadataSerializer(), new DBLogging());
        }
    }
}