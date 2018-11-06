using SharedUILogic.ViewModel;
using System.Windows;

namespace WpfGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            DataContext = new MainViewModel(
                new FileDialog(logger),
                logger,
                new Reflector.Reflector(logger),
                new MetadataItemMapper()
            );
        }
    }
}